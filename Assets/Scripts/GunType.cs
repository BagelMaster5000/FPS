using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunType : ScriptableObject
{
    public string gunName;
    public int ammoMagazineSize;
    public int ammoStarting;
    public int ammoMax;
    public int ammoPickupAmt;
    public float damage;
    public float reloadLength;
    public float scopeAmount;
}
