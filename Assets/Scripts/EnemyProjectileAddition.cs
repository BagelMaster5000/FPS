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

    private void Start()
    {
        target = GetComponent<EnemyGeneral>().target;
        GetComponent<EnemyGeneral>().OnActivated += Activated;
    }

    void Activated() { StartCoroutine(ProjectileAttackLoop());}

    IEnumerator ProjectileAttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(SHOT_INTERVAL);
            Instantiate(prefabBullet, launchLoc.position, Quaternion.identity).
                GetComponent<Bullet>().Setup(target.position - launchLoc.position, ATTACK_DAMAGE);
        }
    }
}
