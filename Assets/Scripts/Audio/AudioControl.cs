﻿using System.Collections;
using System.Collections.Generic;
using EazyTools.SoundManager;
using UnityEngine;
 
public class AudioControl : MonoBehaviour
{
    public ObjectDictionary SoundClips;
    public ObjectDictionary MusicClips;
 
    public string DefaultMusic;
    public bool IgnoreDuplicateMusic = true;
 
    private static int _currentMusicId;
    private static string _currentMusic;
 
    void OnEnable()
    {
        //This is buggy, see https://github.com/JackM36/Eazy-Sound-Manager/issues/7, use own tracking
        //var unusedGO = SoundManager.gameobject;
        //Debug.Log(unusedGO.name); // Simply for not letting unusedGO being optimized away
        //SoundManager.ignoreDuplicateMusic = true;
    }

    void Start()
    {
        Hub.Register(this);
    }
 
    // Simple functions for demo usage with button OnClick()
    public void PlayDefaultMusic()
    {
        PlayDefaultMusic(SoundManager.globalMusicVolume);
    }
 
    public void PlayMusic(string key)
    {
        PlayMusic(key, SoundManager.globalMusicVolume);
    }
 
    public void PlaySound(string key)
    {
        PlaySound(key, SoundManager.globalSoundsVolume);
    }
 
    public void PlayMusic(AudioClip clip)
    {
        PlayMusic(clip, SoundManager.globalMusicVolume);
    }
 
    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, SoundManager.globalSoundsVolume);
    }
 
    // Default theme helper
    public void PlayDefaultMusic(float volume = 0, bool loop = true, bool persist = true)
    {
        PlayMusic(DefaultMusic, volume, loop, persist);
    }
 
    // Basically wrappers for EazySoundManager's method, which fetch the AudioClip from the Dictionary
    public int PlayMusic(string key, float volume = 0, bool loop = true, bool persist = true)
    {
        if (IgnoreDuplicateMusic && _currentMusic == key)
            return _currentMusicId;
 
        Object clip;
        var found = MusicClips.TryGetValue(key, out clip);
        if (found)
        {
            _currentMusic = key;
            _currentMusicId = PlayMusic((AudioClip)clip, volume, loop, persist);
            return _currentMusicId;
        }
 
        return -1;
    }
 
    public int PlayMusic(AudioClip clip, float volume = 0, bool loop = true, bool persist = true)
    {
        return SoundManager.PlayMusic(clip, volume, loop, persist);
    }
 
    public int PlaySound(string key, float volume = 0, Transform sourceTransform = null)
    {
        Object clip;
        var found = SoundClips.TryGetValue(key, out clip);
        if (found)
        {
            return PlaySound((AudioClip)clip, volume, sourceTransform);
        }
 
        return -1;
    }
 
    public int PlaySound(AudioClip clip, float volume = 0, Transform sourceTransform = null)
    {
        return SoundManager.PlaySound(clip, volume, false, sourceTransform);
    }
}