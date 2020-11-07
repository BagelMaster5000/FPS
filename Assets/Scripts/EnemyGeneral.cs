using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyGeneral : MonoBehaviour
{
    float health = 100;
    public Transform target;
    NavMeshAgent agentComponent;

    const float ACTIVATION_RANGE = 25;
    bool activated;

    public event Action OnActivated;
    public UnityEvent OnHit;
    public UnityEvent OnKilled;

    const float DESTINATION_REFRESH_INTERVAL = 0.15f;

    private void Awake()
    {
        agentComponent = GetComponent<NavMeshAgent>();
        agentComponent.enabled = false;
        StartCoroutine(CheckForActivation());
    }

    // Returns true if hit killed
    public bool TakeDamage(float damageAmt)
    {
        health = Mathf.Clamp(health - damageAmt, 0, 100);
        if (health <= 0)
        {
            StopAllCoroutines();
            OnKilled.Invoke();
            Destroy(this.gameObject);
            return true;
        }
        else
            OnHit.Invoke();
        if (!activated)
            Activate();
        return false;
    }

    public void Activate()
    {
        agentComponent.enabled = true;
        StopAllCoroutines();
        StartCoroutine(SettingDestination());
        OnActivated?.Invoke();
        activated = true;
    }

    // Loops until player walks into activation range
    IEnumerator CheckForActivation()
    {
        while (!activated)
        {
            yield return new WaitForSeconds(0.2f);
            if ((target.position - transform.position).magnitude < ACTIVATION_RANGE)
                Activate();
        }
    }

    IEnumerator SettingDestination()
    {
        while (true)
        {
            agentComponent.SetDestination(target.position);
            yield return new WaitForSeconds(DESTINATION_REFRESH_INTERVAL);
        }
    }
}
