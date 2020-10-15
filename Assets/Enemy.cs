using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health = 100;

    public void TakeDamage(float damageAmt)
    {
        health = Mathf.Clamp(health - damageAmt, 0, 100);
        if (health <= 0)
            Destroy(this.gameObject);
    }
}
