using UnityEngine;
using UnityEngine.Audio;
using System.Collections;


public class MainMenuAudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public AudioClip[] backingTracks;

    public bool clipChangedRecent = true;
    public int currentSong = 0;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackingTrack();
    }
    private void Update()
    {
        if (clipChangedRecent)
        {
            StartCoroutine(ChangeSongs());
        }
    }
    private void PlayBackingTrack()
    {
        audioSource.clip = backingTracks[currentSong];
        audioSource.Play();


    }
    IEnumerator ChangeSongs()
    {
        clipChangedRecent = false;
        int x = Random.Range(0, backingTracks.Length);
        yield return new WaitForSeconds(180);
        currentSong = x;
        audioSource.clip = backingTracks[currentSong];
        audioSource.Play();
        clipChangedRecent = true;
    }
}
