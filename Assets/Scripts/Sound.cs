using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0,1)]
    public float volume;
    public AudioMixerGroup audioMixer;

    [HideInInspector]
    public AudioSource source;
}
