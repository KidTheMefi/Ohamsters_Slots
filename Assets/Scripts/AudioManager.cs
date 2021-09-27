using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
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
    }


    public void AudioPause()
    {
        foreach (Sound s in sounds)
        {
            s.source.Pause();
        }
    }

    public void AudioResume()
    {
        foreach (Sound s in sounds)
        {
            s.source.UnPause();
        }
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