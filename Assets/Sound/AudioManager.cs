using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //index
    //0 -> Airport
    //1 -> CP
    //2 -> Intel
    //3 -> prisao
    //4 -> Main Menu
    //8 -> HQ

    //general mixer
    public AudioMixer theMixer;

    //ambientmixer group
    public AudioMixerGroup audioMixerGroupAmbient;

    //sfx mixer group
    public AudioMixerGroup audioMixerGroupSFX;

    //sounds 
    public Sound[] sounds;
    bool inGame = true;
    public Sound[] ambientMusicByIndexScene;

    private void Awake()
    {
        //BuildAudioManagerBySceneManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            theMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            theMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            theMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        }

    }

    public void PlaySoundByName(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            if (s.myTypeOfPLay == Sound.typesOfPlay.play)
            {
                s.source.Play();
            }
            else if (s.myTypeOfPLay == Sound.typesOfPlay.playOneShoot)
            {
                s.source.PlayOneShot(s.clip);
            }
            
        }
        else
        {
            if (inGame) Debug.Log("Error Finding the sound with the title ->" + name);
        }
    }

    /*void BuildAudioManagerBySceneManager()
    {
        //airport ambient music
        if (SceneManager.GetActiveScene().buildIndex == 0 ||
            SceneManager.GetActiveScene().buildIndex == 1 ||
            SceneManager.GetActiveScene().buildIndex == 2 ||
            SceneManager.GetActiveScene().buildIndex == 3)
        {
            ambientMusicByIndexScene[0].source = gameObject.AddComponent<AudioSource>();
            ambientMusicByIndexScene[0].source.clip = ambientMusicByIndexScene[0].clip;
            ambientMusicByIndexScene[0].source.volume = ambientMusicByIndexScene[0].volume;
            ambientMusicByIndexScene[0].source.pitch = ambientMusicByIndexScene[0].pitch;
            ambientMusicByIndexScene[0].source.loop = ambientMusicByIndexScene[0].loop;
            ambientMusicByIndexScene[0].source.outputAudioMixerGroup = audioMixerGroupAmbient;

            PlaySoundByNameAmbient(ambientMusicByIndexScene[0].name);

            BuildAudioSourcesForTanks();
        }

        if (SceneManager.GetActiveScene().buildIndex == 8 ||
            SceneManager.GetActiveScene().buildIndex == 4)
        {
            ambientMusicByIndexScene[1].source = gameObject.AddComponent<AudioSource>();
            ambientMusicByIndexScene[1].source.clip = ambientMusicByIndexScene[1].clip;
            ambientMusicByIndexScene[1].source.volume = ambientMusicByIndexScene[1].volume;
            ambientMusicByIndexScene[1].source.pitch = ambientMusicByIndexScene[1].pitch;
            ambientMusicByIndexScene[1].source.loop = ambientMusicByIndexScene[1].loop;
            ambientMusicByIndexScene[1].source.outputAudioMixerGroup = audioMixerGroupAmbient;

            PlaySoundByNameAmbient(ambientMusicByIndexScene[1].name);
        }
    }*/

    void BuildAudioSourcesForTanks()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixerGroupSFX;
        }
    }

    //play ambient Music 
    public void PlaySoundByNameAmbient(string name)
    {
        Sound s = Array.Find(ambientMusicByIndexScene, sound => sound.name == name);
        if (s != null)
        {
            if (s.myTypeOfPLay == Sound.typesOfPlay.play)
            {
                s.source.Play();
            }
            else if (s.myTypeOfPLay == Sound.typesOfPlay.playOneShoot)
            {
                s.source.PlayOneShot(s.clip);
            }

        }
        else
        {
            if (inGame) Debug.Log("Error Finding the sound with the title ->" + name);
        }
    }

    public void PausePlaySoundByName(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Pause();

        }
        else
        {
            if (inGame) Debug.Log("Error stoping audio name the sound with the title ->" + name);
        }
    }
}
