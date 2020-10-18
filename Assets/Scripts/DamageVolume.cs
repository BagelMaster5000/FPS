using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    public float damageAmt = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FPSGeneral>().TakeDamage(damageAmt);
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
