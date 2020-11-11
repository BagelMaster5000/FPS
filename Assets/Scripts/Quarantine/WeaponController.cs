/*
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GunType[] allGuns;
    public CallOfDutyGameplay player;

    private GameObject currentGun;

    private void Start()
    {
        Equip(0);
    }

    void Equip(int gunNum)
    {
        if (currentGun != null)
            Destroy(currentGun);

        currentGun = Instantiate(allGuns[gunNum].prefab, transform.position, transform.rotation, transform) as GameObject;
        currentGun.transform.localPosition = Vector3.zero;
        currentGun.transform.localRotation = Quaternion.Euler(Vector3.right * 90);
        player.SetGunAnimator(currentGun.GetComponent<Animator>());
    }
}
*/
