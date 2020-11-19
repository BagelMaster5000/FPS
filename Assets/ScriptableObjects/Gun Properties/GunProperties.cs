using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunProperties : ScriptableObject
{
    public FPSGeneral.GunType gunType;
    public int gunPrice;
    public int ammoMagazineSize;
    public int ammoStarting;
    public int ammoMax;
    public int ammoPickupAmt;
    public float damage;
    public float reloadLength;
    public float scopeAmount;

    [Space(10)]
    public float onFiredShakeAmt;
    public float[] onReloadShakeAmts;
}
