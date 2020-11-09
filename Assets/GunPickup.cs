using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GunPickup : MonoBehaviour
{
    [SerializeField] FPSGeneral.GunType gunType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FPSGeneral>().AddGun(gunType);
            Destroy(this.gameObject);
        }
    }
}
