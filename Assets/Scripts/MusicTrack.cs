using System;
using UnityEngine;

[Serializable]
public class MusicTrack
{
    [HideInInspector] public string Name = "Music Track";
    public AudioClip track;
    [Range(0, 1)]
    public float volume;
}
