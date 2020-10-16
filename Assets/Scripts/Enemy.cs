using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    float health = 100;
    public Transform target;
    NavMeshAgent agentComponent;
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

    const float ACTIVATION_RANGE = 10;
    const float ATTACK_DAMAGE = 40;

    private void Start()
    {
        agentComponent = GetComponent<NavMeshAgent>();
        agentComponent.enabled = false;
        StartCoroutine(CheckForActivation());
    }

    public void TakeDamage(float damageAmt)
    {
        Debug.Log("Took damage");
        health = Mathf.Clamp(health - damageAmt, 0, 100);
        if (health <= 0)
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }

    // Loops until player walks into activation range
    IEnumerator CheckForActivation()
    {
        while (true)
        {
            if ((target.position - transform.position).magnitude < ACTIVATION_RANGE)
            {
                agentComponent.enabled = true;
                StartCoroutine(SettingDestination());
                StartCoroutine(LaserAttackLoop());
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator SettingDestination()
    {
        while (true)
        {
            agentComponent.SetDestination(target.position);
            yield return null;
        }
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
        float timeLimit = Time.time + TRACKING_LENGTH;
        Vector3 aimDirection;
        RaycastHit hit;
        laser1.material = laser2.material = trackingLaser;
        laser1.enabled = laser2.enabled = true;
        while (timeLimit > Time.time)
        {
            laser1.SetPosition(0, laser1LaunchPoint.position);
            laser2.SetPosition(0, laser2LaunchPoint.position);
            aimDirection = target.position - transform.position;
            if (Physics.Raycast(transform.position, aimDirection, out hit))
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
        laser1.material = laser2.material = shotLaser;
        if (Physics.Raycast(transform.position, laser1.GetPosition(1) - laser1.GetPosition(0), out hit) && hit.collider.GetComponent<FPSGeneral>() != null)
        {
            hit.collider.GetComponent<FPSGeneral>().TakeDamage(ATTACK_DAMAGE);
        }
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
