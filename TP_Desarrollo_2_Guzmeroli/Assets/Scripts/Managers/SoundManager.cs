using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Music,
    SFX
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] SoundSO[] soundList;
    [SerializeField] AudioSource musicMenu;
    [SerializeField] AudioSource inGameMusic;
    [SerializeField] List<AudioSource> audioSourcesList;

    [SerializeField][Range(0, 1)] float sfxVolume;
    [SerializeField][Range(0, 1)] float musicVolume;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }



    public void PlayMusicMenu() => musicMenu.Play();
    public void StopMusicMenu() => musicMenu.Stop();

    public void PlayMusicGame() => inGameMusic.Play();
    public void StopMusicGame() => inGameMusic.Stop();



    public void SetMusicMenu(string key)
    {
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key && soundList[i].type == SoundType.Music)
            {
                musicMenu.clip = soundList[i].clip;
                return;
            }
        }

        Debug.LogError($"No se encontro {key} en la lista o no pertenece a Tipo Musica");
    }

    public void SetMusicInGame(string key)
    {
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key && soundList[i].type == SoundType.Music)
            {
                inGameMusic.clip = soundList[i].clip;
                return;
            }
        }

        Debug.LogError($"No se encontro {key} en la lista o no pertenece a Tipo Musica");
    }

    public void PlaySound(string key)
    {
        SoundSO sound = null;

        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                sound = soundList[i];
                break;
            }
        }

        audioSourcesList[0].PlayOneShot(sound.clip, sound.volume * sfxVolume);

        if (sound == null) Debug.LogError($"[{gameObject.name}.PlaySound]Error: \"{key}\" could not be found!");
    }

    public void StopSound(string key)
    {
        SoundSO sound = null;
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                sound = soundList[i];
                break;
            }
        }

        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            if (audioSourcesList[i].clip == sound.clip && audioSourcesList[i].isPlaying)
            {
                audioSourcesList[i].Stop();
                return;
            }
        }

        if (sound == null) Debug.LogError($"[{gameObject.name}.StopSound]Error: \"{key}\" could not be found!");
    }

    public void SetSound(string key, AudioSource _as)
    {
        SoundSO sound = null;
        if (_as == null)
        {
            Debug.LogError($"[{_as.gameObject.name}.SetSound]Error: Se detecto instancia null: {_as}");
            return;
        }

        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                sound = soundList[i];

                _as.clip = sound.clip;
                _as.spatialBlend = sound.spatialBlend;
                _as.volume = sound.volume;
                _as.pitch = sound.pitch;
                _as.priority = sound.priority;

                _as.minDistance = sound.minDistance;
                _as.maxDistance = sound.maxDistance;

                _as.loop = sound.loop;

                if (sound.type == SoundType.SFX && !(audioSourcesList.Contains(_as)))
                {
                    audioSourcesList.Add(_as);
                    Debug.Log("Sound Adds Succes");
                }

                return;
            }
        }

        if (sound == null) Debug.LogError($"[{_as.gameObject.name}.SetSound]Error: \"{key}\" could not be found!");
    }

    public void ChangeVolumeSFX(float newVol)
    {
        var volume = newVol / 100.0f;
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].type == SoundType.SFX) soundList[i].volume = volume;
        }

        for (int i = 0; i < audioSourcesList.Count; i++)
        {
            audioSourcesList[i].volume = volume;
        }

        sfxVolume = volume;
    }

    public void ChangeMusicVolume(float newVol)
    {
        var volume = newVol / 100.0f;

        if (musicMenu)
            musicMenu.volume = volume;
        else
            Debug.LogError("No se asigno una musica para el menu");

        if (inGameMusic)
            inGameMusic.volume = volume;
        else
            Debug.LogError("No se asigno una musica para el gameplay");

        musicVolume = volume;
    }

    public void ChangeGeneralVolume(float newVol)
    {
        ChangeVolumeSFX(newVol);
        sfxVolume = newVol;

        ChangeMusicVolume(newVol);
        musicVolume = newVol;
    }

    public AudioClip GetClip(string key)
    {
        for (int i = 0; i < soundList.Length; i++)
        {
            if (soundList[i].keyCode == key)
            {
                return soundList[i].clip;
            }
        }

        Debug.LogError($"No se encontro {key} en la lista.");
        return null;
    }

    public float GetActualSFXVolume()
    {
        return sfxVolume;
    }

    public float GetActualMusicVolume()
    {
        return musicVolume;
    }
}