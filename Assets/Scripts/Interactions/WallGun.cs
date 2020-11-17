using UnityEngine;

public class WallGun : MonoBehaviour
{
    public GunProperties wallGunProperties;

    public float GetGunPrice() { return wallGunProperties.gunPrice; }
    public FPSGeneral.GunType GetGunType() { return wallGunProperties.gunType; }
}
