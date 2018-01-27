using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private BoxCollider[] spawnPlanes;

    [SerializeField]
    private PlayerWorld playerWorld;

    [SerializeField]
    private float safeZonePercent = 0.05f;

	void Start ()
    {
        SpawnEnemy(playerWorld.CurrentPlayerLayer);
        SpawnEnemy(playerWorld.CurrentPlayerLayer);
        SpawnEnemy(playerWorld.CurrentPlayerLayer);
        SpawnEnemy(playerWorld.CurrentPlayerLayer);
        SpawnEnemy(playerWorld.CurrentPlayerLayer);
    }

    private void SpawnEnemy(int layerMask)
    {
        BoxCollider randomSpawnPlane = spawnPlanes[Random.Range(0, spawnPlanes.Length - 1)];
        
        GameObject newEnemy = null;
        newEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, transform);

        BoxCollider newEnemyBox = newEnemy.GetComponent<BoxCollider>();
        if(newEnemyBox != null)
        {
            float xPlaneExtent = (1-safeZonePercent) * randomSpawnPlane.bounds.extents.x;
            float zPlaneExtent = (1-safeZonePercent) * randomSpawnPlane.bounds.extents.z;
            float RandomXinPlane = Random.Range(-xPlaneExtent, xPlaneExtent);
            float RandomZinPlane = Random.Range(-zPlaneExtent, zPlaneExtent);
            Vector3 spawnPosition = new Vector3(RandomXinPlane, newEnemyBox.size.y/2, RandomZinPlane) + randomSpawnPlane.transform.localPosition;
            newEnemy.transform.localPosition = spawnPosition;
            newEnemy.layer = layerMask;
        }
    }
}
