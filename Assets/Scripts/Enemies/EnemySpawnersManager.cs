using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

#if UNITY_EDITOR
using UnityEngine;
#endif

[ExecuteAlways]
public class EnemySpawnersManager : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        enemySpawners = FindObjectsOfType<EnemySpawner>().ToList();

        Vector3 offset;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            offset = Vector3.up * (transform.position.y - spawner.transform.position.y) / 2.0f;

            Handles.DrawBezier(
                transform.position,
                spawner.transform.position,
                transform.position - offset,
                spawner.transform.position + offset,
                Color.red,
                EditorGUIUtility.whiteTexture,
                2.0f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawner.transform.position, 1);
        }
    }
#endif
}
