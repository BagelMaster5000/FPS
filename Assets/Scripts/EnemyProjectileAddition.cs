using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyProjectileAddition : MonoBehaviour
{
    Transform target;
    public GameObject prefabBullet;
    public Transform launchLoc;

    const float SHOT_INTERVAL = 4;
    const float ATTACK_DAMAGE = 40;

    public VoidEvent OnFired;

    private void Awake() { GetComponent<EnemyGeneral>().OnActivated += Activated; }

    void Activated()
    {
        target = GetComponent<EnemyGeneral>().target;
        StartCoroutine(ProjectileAttackLoop());
    }

    IEnumerator ProjectileAttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(SHOT_INTERVAL);
            // Firing bullet
            OnFired.Invoke();
            Instantiate(prefabBullet, launchLoc.position, Quaternion.identity).
                GetComponent<Bullet>().Setup(target.position - launchLoc.position, ATTACK_DAMAGE);
        }
    }
}
