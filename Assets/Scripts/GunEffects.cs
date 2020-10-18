using System.Collections;
using UnityEngine;

public class GunEffects : MonoBehaviour
{
    public SpriteRenderer muzzleFlash;
    public Sound gunShot;
    public Sound[] reloadSounds;

    private void Start()
    {
        gunShot.audioSource = gameObject.AddComponent<AudioSource>();
        gunShot.audioSource.clip = gunShot.audioClip;
        gunShot.audioSource.volume = gunShot.volume;
        foreach (Sound reloadSound in reloadSounds)
        {
            reloadSound.audioSource = gameObject.AddComponent<AudioSource>();
            reloadSound.audioSource.clip = reloadSound.audioClip;
            reloadSound.audioSource.volume = reloadSound.volume;
        }
    }

    public void Flash() { StartCoroutine(MuzzleFlash()); }

    public void Shot() { gunShot.audioSource.Play(); }

    public void Reload(int index)
    { 
        if (index < reloadSounds.Length && index >= 0)
            reloadSounds[index].audioSource.Play();
    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.enabled = false;
    }
}
