using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private EnemySpawner spawner;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]    
    private TowerManager towerManager;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public AudioClip[] backingTracks;

    [Header("Counters")]
    public int level;
    public int wave = 1;
    public int score = 0;
    public int money = 0;
    public int currentEnemies = 0;
    public int requiredScore = 100;
    public int towerHealth;

    [Header("Game Controls")]
    private int enemyCount;
    public bool waveStarted = false;
    private bool waveEnded = false;
    public bool playerDead = false;
    public bool startedSpawning = false;
    public bool waveRunning = false;
    public bool clipChangedRecent = true;
    public int currentSong = 0;
    public bool changeSong = false;

    public void Start()
    {
        LevelStartToolTip();
        towerManager = FindFirstObjectByType<TowerManager>();  
        audioSource = GetComponent<AudioSource>();  
        PlayBackingTrack();
    }
    public void Update()
    {
        towerHealth = towerManager.health;
        if (clipChangedRecent)
        {
            StartCoroutine(ChangeSongs());
        }
        CheckIfWaveRunning();
        CheckForWaveEnd();
        CheckIfPlayerWon();
        ChangeSongFromBool();   
        if (playerDead)
        {
            GameOver();
        }
    }
    public void ChangeSongFromBool()
    {
        if (changeSong)
        {
            
            
            int x = Random.Range(0, backingTracks.Length);
            
            currentSong = x;
            audioSource.clip = backingTracks[currentSong];
            audioSource.Play();

            changeSong = false;
        }
    }
    public void StartWave()
    {
        waveStarted = true;
        GetEnemyCount();
        SpawnEnemies();
    }
    public void UpdateScore(in int addScore)
    {
        score += addScore;
    }
    public void UpdateMoney(in int addMoney)
    {
        money += addMoney;
    }

    private void PlayBackingTrack()
    {
        audioSource.clip = backingTracks[currentSong];
        audioSource.Play();

        
    }
    private void LevelStartToolTip()
    {
        uiManager.ShowStartToolTip();
    }

    private void CheckIfWaveRunning()
    {
        if (waveStarted && currentEnemies > 0)
        {
            waveRunning = true;
            waveEnded = false;  
        }
    }
    private void CheckForWaveEnd()
    {
        if (startedSpawning && currentEnemies <= 0 && !waveStarted)
        {
            waveStarted = false;
            startedSpawning = false;
            waveEnded = true;
            enemyCount = 0;
            WaveEnd();       
        }
    }

   
    private void SpawnEnemies()
    {
        waveEnded = false;
        waveStarted = true;
        startedSpawning = true;
        spawner.SpawnEnemies(enemyCount);
    }
    private void GetEnemyCount()
    {
        enemyCount = wave * wave;
    }
    private void CheckIfPlayerWon()
    {
        if (waveEnded && score >= requiredScore)
        {
            uiManager.startWaveButton.SetActive(false);
            uiManager.ShowGameWonToolTip();
            StartCoroutine(NextLevel());
        }
    }
    private void WaveEnd()
    {  
        enemyCount = 0;
        wave++;
        waveEnded = true;
        uiManager.ShowStartWaveButton();
    }

    private void GameOver()
    {
        uiManager.startWaveButton.SetActive(false);
        uiManager.ShowGameLostToolTip();
        StartCoroutine(ResetGame());
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(10);
        if (level != 2)
            SceneManager.LoadScene(level + 1);
        else if (level == 2)
            uiManager.ShowGameWonToolTip();
    }
    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator ChangeSongs()
    {
        clipChangedRecent = false;
        int x = Random.Range(0, backingTracks.Length);
        yield return new WaitForSeconds(120);
        if (x != currentSong)
        {
            currentSong = x;
            
        }
        else if (x == currentSong)
        {
            if (x == 0)
                x++;
            else
            {
                x--;
                currentSong = x;
            }
        }
        audioSource.clip = backingTracks[x];
        audioSource.Play();

        clipChangedRecent = true;
    }
    IEnumerator ChangeSongsRightNow()
    {
        clipChangedRecent = false;
        int x = Random.Range(0, backingTracks.Length);
        yield return new WaitForSeconds(0);
        currentSong = x;
        audioSource.clip = backingTracks[currentSong];
        audioSource.Play();

        clipChangedRecent = true;
    }
}
