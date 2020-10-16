using System.Collections;
using UnityEngine;

public class GunEffects : MonoBehaviour
{
    public SpriteRenderer muzzleFlash;
    public AudioSource gunShot;
    public AudioSource reloadSound;

    public void Flash() { StartCoroutine(MuzzleFlash()); }

    public void Shot() { gunShot.Play(); }

    public void Reload() { reloadSound.Play(); }

    IEnumerator MuzzleFlash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.enabled = false;
    }
}
