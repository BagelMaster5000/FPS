using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundDetachAndDestroy : MonoBehaviour
{
    AudioSource sound;

    private void Awake() { sound = GetComponent<AudioSource>(); }

    public void Activate()
    {
        this.transform.parent = null;
        sound.Play();
        StartCoroutine(DestroyWhenSilent());
    }

    IEnumerator DestroyWhenSilent()
    {
        while (sound.isPlaying)
            yield return null;
        Destroy(this.gameObject);
    }
}
