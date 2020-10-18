using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float SPEED = 2;
    const float LIFETIME = 8;
    float timeToDestroy;
    Vector3 moveDirection;
    float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<FPSGeneral>()?.TakeDamage(damage); // FIX null propagation operator doesn't work here
        Destroy(this.gameObject);
    }

    private void Start() { timeToDestroy = Time.time + LIFETIME; }

    // Sets move direction and damage amount
    public void Setup(Vector3 setMoveDirection, float setDamage)
    {
        moveDirection = setMoveDirection;
        damage = setDamage;
    }

    private void Update()
    {
        transform.position += moveDirection * SPEED * Time.deltaTime;
        if (Time.time > timeToDestroy) Destroy(this.gameObject);
    }
}
