using UnityEngine;

public static class AudioManager 
{
    public static GameObject instance = null;

    /* Creates a game object and makes it play music
     * @param clip music track to play
     */
    public static void PlaySongHelper(AudioClip clip)
    {
        if (instance == null)
        {
            instance = new GameObject("Music");
            instance.AddComponent<AudioSource>();
            instance.GetComponent<AudioSource>().clip = clip;
            instance.GetComponent<AudioSource>().loop = true;
            instance.GetComponent<AudioSource>().playOnAwake = false;
            MonoBehaviour.DontDestroyOnLoad(instance);
            instance.GetComponent<AudioSource>().Play();
        }
    }
}
