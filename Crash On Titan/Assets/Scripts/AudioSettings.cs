using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{

    public AudioSource audioSource; // The audio source
    public bool isPlaying; // Bool to determine toggle


    // Start is called before the first frame update
    private void Start()
    {
        isPlaying = true; // set is playing to true, as the audio source plays on wake
    }

    public void Toggle() // Toggle mute and unmute
    {
        if(isPlaying) // If the audiosource is playing
        {
            isPlaying = !isPlaying; // set is playing to false
            Mute(); // Mute
        }
        else
        {
            isPlaying = true; // Set is playing to true
            UnMute(); // unmute
        }
    }
     
    public void Mute() // Mute the audiosource
    {
        audioSource.Pause();
    }

    public void UnMute() // unmute the audiosource
    {
        audioSource.UnPause();
    }

}
