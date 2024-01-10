using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSound : MonoBehaviour
{
    public string savePath;

    public VolumeSet volumeSet;
    public LocalizationManager localizationManager;
    public Animator animator;
    public AudioSource Press;
    [System.Serializable]
    public class Settings {
        public float UISounds;
        public float musicSounds;
        public float effectSounds;
        public int langId;
    }

    public void Start()
    {
        if (File.Exists(Application.dataPath + savePath))
            LoadSound();
    }

    public void SaveAllSound()
    {
        Settings settings = new Settings();
        settings.musicSounds = volumeSet.musicSounds;
        settings.effectSounds = volumeSet.effectSounds;
        settings.UISounds = volumeSet.UISounds;
        settings.langId = LocalizationManager.SelectedLanguage;

        if (Directory.Exists(Application.dataPath + "/save")) {
            FileStream stream = new FileStream(Application.dataPath + savePath, FileMode.Create);
            BinaryFormatter form = new BinaryFormatter();
            form.Serialize(stream, settings);
            stream.Close();
        }
    }

    public void LoadSound()
    {
        if (File.Exists(Application.dataPath + savePath)) {
            FileStream stream = new FileStream(Application.dataPath + savePath, FileMode.Open);
            BinaryFormatter form = new BinaryFormatter();
            try {
                Settings settings = (Settings)form.Deserialize(stream);
                volumeSet.UISounds = settings.UISounds;
                volumeSet.musicSounds = settings.musicSounds;
                volumeSet.effectSounds = settings.effectSounds;
                localizationManager.SetLanguage(settings.langId);
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
