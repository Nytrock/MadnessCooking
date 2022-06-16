using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class SaveSound : MonoBehaviour
{
    public VolumeSet volumeSet;
    public Animator animator;
    public AudioSource Press;
    [System.Serializable]
    public class Volume {
        public float AllSounds;
        public float MusicSounds;
        public float EffectSounds;
    }

    public void Start()
    {
        if (File.Exists(Application.dataPath + "/save/MadnessCookingSaveSound.txt"))
            LoadSound();
    }

    public void SaveAllSound()
    {
        Volume volume = new Volume();
        volume.AllSounds = volumeSet.AllSounds;
        volume.MusicSounds = volumeSet.MusicSounds;
        volume.EffectSounds = volumeSet.EffectSounds;

        if (Directory.Exists(Application.dataPath + "/save")) {
            FileStream stream = new FileStream(Application.dataPath + "/save/MadnessCookingSaveSound.txt", FileMode.Create);
            BinaryFormatter form = new BinaryFormatter();
            form.Serialize(stream, volume);
            stream.Close();
        }
    }

    public void LoadSound()
    {
        if (File.Exists(Application.dataPath + "/save/MadnessCookingSaveSound.txt")) {
            FileStream stream = new FileStream(Application.dataPath + "/save/MadnessCookingSaveSound.txt", FileMode.Open);
            BinaryFormatter form = new BinaryFormatter();
            try {
                Volume volume = (Volume)form.Deserialize(stream);
                volumeSet.AllSounds = volume.AllSounds;
                volumeSet.MusicSounds = volume.MusicSounds;
                volumeSet.EffectSounds = volume.EffectSounds;
                volumeSet.SetVolume();
            } finally {
                stream.Close();
            }
        }
    }

    public void Active()
    {
        animator.SetBool("Active", !animator.GetBool("Active"));
        Press.Play();
        if (!animator.GetBool("Active")) { 
            SaveAllSound();
        } 
    }
}
