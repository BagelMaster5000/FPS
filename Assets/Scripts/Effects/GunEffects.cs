using System.Collections;
using UnityEngine;

public class GunEffects : MonoBehaviour
{
    [SerializeField] FPSGeneral player;

    [Space(10)]
    [SerializeField] SpriteRenderer muzzleFlash;
    [SerializeField] Sound gunShot;
    [SerializeField] Sound[] reloadSounds;

    public FloatEvent OnCameraShake;

    private void Start()
    {
        gunShot.audioSource = gameObject.AddComponent<AudioSource>();
        gunShot.audioSource.clip = gunShot.audioClip;
        gunShot.audioSource.volume = gunShot.volume;
        gunShot.audioSource.playOnAwake = false;
        foreach (Sound reloadSound in reloadSounds)
        {
            reloadSound.audioSource = gameObject.AddComponent<AudioSource>();
            reloadSound.audioSource.clip = reloadSound.audioClip;
            reloadSound.audioSource.volume = reloadSound.volume;
            reloadSound.audioSource.playOnAwake = false;
        }
    }

    public void Flash() { StartCoroutine(MuzzleFlash()); }

    public void Shot()
    {
        gunShot.audioSource.Play();
        OnCameraShake.Invoke(player.GetGunOnFiredShakeAmt());
    }

    public void Reload(int index)
    {
        if (index < reloadSounds.Length && index >= 0)
        {
            reloadSounds[index].audioSource.Play();
            OnCameraShake.Invoke(player.GetGunOnReloadShakeAmt(index));
        }
    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.enabled = false;
    }
}
