using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public float damageAmt = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSGeneral>() == null) return;

        other.GetComponent<FPSGeneral>().TakeDamage(damageAmt);
    }
}
