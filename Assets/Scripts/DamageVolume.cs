using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<CallOfDutyGameplay>().TakeDamage(0.2f);
    }
}
