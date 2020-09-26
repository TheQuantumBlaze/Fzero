using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip motorEngineSound;
    public AudioClip motorExteriorSound;
    public AudioClip explosionSound;
    public AudioClip background;

    AudioSource audioSourceEngine;
    AudioSource audioSourceExt;
    AudioSource backgroundAudio;

    PlayerController player;

    // Start is called before the first frame update
    private void Start()
    {
    	player = GetComponent<PlayerController>();

        AudioSource[] srcs = GetComponents<AudioSource>();
        audioSourceEngine = srcs[0];
        audioSourceExt = srcs[1];
        backgroundAudio = srcs[2];
        
        SetupAudioSourcesForDrive();
        backgroundAudio.clip = background;
        backgroundAudio.loop = true;
        backgroundAudio.volume = 0.027f;
        backgroundAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsAlive)
        {
            float pitchAdjusted = player.acceleration * 8;

            if(player.IsHighGear)
                audioSourceEngine.pitch = pitchAdjusted;
            else
                audioSourceEngine.pitch = 2*pitchAdjusted;

            if(audioSourceExt)
                audioSourceExt.pitch = pitchAdjusted;
        }
    }

    public void OnCrash()
    {
    	SetupAudioSourcesForExplosion();
    }

    public void OnRespawn()
    {
    	SetupAudioSourcesForDrive();
    }

    void SetupAudioSourcesForDrive()
    {
        audioSourceEngine.clip = motorEngineSound;
        audioSourceEngine.loop = true;
        audioSourceEngine.pitch = 0;
        audioSourceEngine.Play();

        if(audioSourceExt)
        {
            audioSourceExt.clip = motorExteriorSound;
            audioSourceExt.loop = true;
            audioSourceExt.pitch = 0;
            audioSourceExt.Play();
        }
    }

    void SetupAudioSourcesForExplosion()
    {
        audioSourceEngine.clip = explosionSound;
        audioSourceEngine.loop = false;
        audioSourceEngine.pitch = 1;
        audioSourceEngine.Play();

        if(audioSourceExt)
            audioSourceExt.Stop();
    }
}
