using System;
using UnityEngine;

[Serializable]
public class MusicInfo {
    [SerializeField] private AudioClip _music;
    [SerializeField, Range(0, 1)] private float _volume = 0.5f;

    public AudioClip Music => _music;
    public float Volume => _volume;
}
