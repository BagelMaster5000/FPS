using UnityEngine;

public static class AudioManager 
{
    public static GameObject instance = null;

    /* Creates a game object and makes it play music
     * @param clip music track to play
     */
    public static void PlaySongHelper(MusicTrack musicTrack)
    {
        if (instance == null)
        {
            instance = new GameObject("Music");
            instance.AddComponent<AudioSource>();
            AudioSource tempAudioSource = instance.GetComponent<AudioSource>();
            tempAudioSource.volume = musicTrack.volume;
            tempAudioSource.clip = musicTrack.track;
            tempAudioSource.loop = true;
            tempAudioSource.playOnAwake = false;
            MonoBehaviour.DontDestroyOnLoad(instance);
            tempAudioSource.Play();
        }
    }
}
