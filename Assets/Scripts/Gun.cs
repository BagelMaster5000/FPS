using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public AudioSource gunShot;
    public AudioSource reloadSound;

    public void Flash() { muzzleFlash.Play(); }

    public void Shot() { gunShot.Play(); }

    public void Reload() { reloadSound.Play(); }
}
