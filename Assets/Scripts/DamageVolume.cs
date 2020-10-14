using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSGeneral>() == null) return;

        other.GetComponent<FPSGeneral>().TakeDamage(23);
    }
}
