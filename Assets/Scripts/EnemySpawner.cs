using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform playerToTrack;
    const float SPAWN_INTERVAL = 9;
    const float SPAWN_INTERVAL_RANGE = 7;
    const float SPAWN_THRESHOLD = -0.5f; // Any value less than this from the dot product will cause the enemy not to spawn

    [Header("Enemies")] // FIXME Currently uses instantiation and destruction, but should use pooling
    public GameObject laserEnemyPrefab;
    public GameObject projectileEnemyPrefab;
    Vector3 spawnOffset = new Vector3(0, 0.45f, 0);

    void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        while (true)
        {
            transform.LookAt(playerToTrack);
            if (Vector3.Dot(transform.TransformDirection(Vector3.forward), playerToTrack.TransformDirection(Vector3.forward)) > -0.5f)
            {
                GameObject tempEnemy = (Random.value > 0.5f) ?
                    Instantiate(laserEnemyPrefab, transform.position + spawnOffset, transform.rotation) :
                    Instantiate(projectileEnemyPrefab, transform.position + spawnOffset, transform.rotation);
                tempEnemy.GetComponent<EnemyGeneral>().target = playerToTrack;
                tempEnemy.GetComponent<EnemyGeneral>().Activate();
            }
            yield return new WaitForSeconds(Random.Range(SPAWN_INTERVAL - SPAWN_INTERVAL_RANGE / 2.0f, SPAWN_INTERVAL + SPAWN_INTERVAL_RANGE / 2.0f));
        }
    }
}
