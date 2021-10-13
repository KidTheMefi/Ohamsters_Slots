using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource winningSound;
    public AudioSource backgroundMusic;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;
        }

        backgroundMusic.Play();
    }



    public void AudioPause()
    {
        winningSound.Pause();
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
    }

    public void AudioResume()
    {
        winningSound.UnPause();
        foreach (Sound s in sounds)
        {
            s.source.UnPause();
        }
    }

    public void PlayLose()
    {
        Play("Nothing");
    }

    public void PlaySpin()
    {
        Play("Spinning");
        Play("Pull");
    }

    public void StopSpinning()
    {
        Stop("Spinning");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Play(AudioClip audio)
    {
        winningSound.clip = audio;
        winningSound.Play();
        StartCoroutine(winSoundPlaying());
    }

    private IEnumerator winSoundPlaying()
    {
        yield return new WaitForSeconds(0.1f);
        backgroundMusic.mute = true;
        while (winningSound.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }
        backgroundMusic.mute = false;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound " + name + " not found!");
            return;
        }

        Debug.Log("Sound " + name + "  found!");
        s.source.Stop();
        s.source.Pause();
    }
}
