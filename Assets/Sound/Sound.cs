using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    //Variables 
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    //this will be the source from the sound to me emitted 
    [HideInInspector]
    public AudioSource source;

    public bool loop;

    //type of play
    public enum typesOfPlay { play, playOneShoot };
    public typesOfPlay myTypeOfPLay;
}
