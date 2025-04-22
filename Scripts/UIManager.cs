using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject startWaveButton;
    public GameObject startToolTip;
    public GameObject gameWonToolTip;
    public GameObject gameLostToolTip;
    public TextMeshProUGUI waveCounter;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI moneyCounter;
    public TextMeshProUGUI healthCounter;
    public LevelManager levelManager;

    private void Update()
    {
        UpdateWave();
        UpdateScore();
        UpdateMoney();
        UpdateHealth(); 
    }
    private void UpdateWave()
    {
        waveCounter.text = levelManager.wave.ToString();
    }
    private void UpdateScore()
    {
        scoreCounter.text = levelManager.score.ToString();
    }
    private void UpdateMoney()
    {
        moneyCounter.text = levelManager.money.ToString();
    }
    private void UpdateHealth()
    {
        healthCounter.text = levelManager.towerHealth.ToString();
    }
    public void ShowStartWaveButton()
    {
        startWaveButton.SetActive(true);
    }

    public void ShowStartToolTip()
    {
        startToolTip.SetActive(true);   
    }

    public void ShowGameWonToolTip()
    {
        gameWonToolTip.SetActive(true);
    }

    public void ShowGameLostToolTip()
    {
        gameLostToolTip.SetActive(true);
    }
}
