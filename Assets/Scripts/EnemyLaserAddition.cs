using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyLaserAddition : MonoBehaviour
{
    Transform target;
    public Transform laser1LaunchPoint;
    public Transform laser2LaunchPoint;
    public LineRenderer laser1;
    public LineRenderer laser2;
    
    static Vector3 LASER_HIT_DELTA = new Vector3(0, -0.25f, 0);
    const float TRACKING_LENGTH = 2;
    const float WAITING_LENGTH = 0.75f;
    const float AFTERSHOT_LENGTH = 0.2f;
    const float DELAY_BETWEEN_SHOTS = 4.5f;
    public Material trackingLaser;
    public Material shotLaser;
    const float ATTACK_DAMAGE = 40;

    public VoidEvent OnLaserBeginCharging;
    public VoidEvent OnLaserFired;

    private void Awake() { GetComponent<EnemyGeneral>().OnActivated += Activated; }

    void Activated()
    {
        target = GetComponent<EnemyGeneral>().target;
        StartCoroutine(LaserAttackLoop());
    }

    IEnumerator LaserAttackLoop()
    {
        while (true)
        {
            StartCoroutine(LaserAttack());
            yield return new WaitForSeconds(TRACKING_LENGTH + WAITING_LENGTH + DELAY_BETWEEN_SHOTS);
        }
    }

    IEnumerator LaserAttack()
    {
        // Laser starts charging
        OnLaserBeginCharging.Invoke();
        float timeLimit = Time.time + TRACKING_LENGTH;
        Vector3 aimDirection;
        RaycastHit hit;
        laser1.material = laser2.material = trackingLaser;
        laser1.enabled = laser2.enabled = true;
        while (timeLimit > Time.time)
        {
            laser1.SetPosition(0, laser1LaunchPoint.position);
            laser2.SetPosition(0, laser2LaunchPoint.position);
            aimDirection = target.position - (laser1LaunchPoint.position + laser2LaunchPoint.position) / 2.0f + Vector3.down / 2.0f;
            if (Physics.Raycast(transform.position, aimDirection, out hit) && !hit.collider.CompareTag("Player"))
            {
                laser1.SetPosition(1, hit.point + LASER_HIT_DELTA);
                laser2.SetPosition(1, hit.point + LASER_HIT_DELTA);
            }
            else
            {
                laser1.SetPosition(1, transform.position + aimDirection * 99);
                laser2.SetPosition(1, transform.position + aimDirection * 99);
            }
            yield return new WaitForEndOfFrame();
        }
        timeLimit = Time.time + WAITING_LENGTH;
        while (timeLimit > Time.time)
        {
            laser1.SetPosition(0, laser1LaunchPoint.position);
            laser2.SetPosition(0, laser2LaunchPoint.position);
            yield return null;
        }

        // Laser is shot
        OnLaserFired.Invoke();
        laser1.material = laser2.material = shotLaser;
        if (Physics.Raycast(transform.position, laser1.GetPosition(1) - laser1.GetPosition(0), out hit) && hit.collider.GetComponent<FPSGeneral>() != null)
            hit.collider.GetComponent<FPSGeneral>().TakeDamage(ATTACK_DAMAGE);
        timeLimit = Time.time + AFTERSHOT_LENGTH;
        while (timeLimit > Time.time)
        {
            laser1.SetPosition(0, laser1LaunchPoint.position);
            laser2.SetPosition(0, laser2LaunchPoint.position);
            yield return null;
        }
        laser1.enabled = laser2.enabled = false;
    }
}
