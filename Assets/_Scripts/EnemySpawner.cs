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

    public List<GameObject> Enemies { set; get; }
    private List<GameObject> EnemiesToRemove = new List<GameObject>();

	private void Start ()
    {
        Enemies = new List<GameObject>();
        Enemies.Add(SpawnEnemy());
        Enemies.Add(SpawnEnemy());
        Enemies.Add(SpawnEnemy());
        Enemies.Add(SpawnEnemy());
        Enemies.Add(SpawnEnemy());
    }

    private void Update()
    {
        foreach(GameObject currentEnemy in Enemies)
        {
            EnemyBase enemyBase = currentEnemy.GetComponent<EnemyBase>();
            if (enemyBase.CurrentState == EnemyBase.EnemyState.Dead)
            {
                EnemiesToRemove.Add(currentEnemy);
                playerWorld.enemiesToTransfer.Add(enemyBase);
            }
        }

        if(EnemiesToRemove.Count > 0)
        {
            foreach (GameObject currentEnemy in EnemiesToRemove)
            {
                Enemies.Remove(currentEnemy);
            }
            EnemiesToRemove.Clear();
        }
    }

    private GameObject SpawnEnemy()
    {
        GameObject newEnemy = null;
        newEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, transform);
        
        SetupNewEnemy(newEnemy);

        return newEnemy;
    }

    public void SpawnTransferredEnemy(GameObject newEnemy)
    {
        Enemies.Add(newEnemy);
        SetupNewEnemy(newEnemy);
    }

    private void SetupNewEnemy(GameObject newEnemy)
    {
        if(newEnemy != null)
        {
            if(newEnemy.transform.parent != transform)
            {
                newEnemy.transform.SetParent(transform);
            }

            newEnemy.layer = playerWorld.CurrentPlayerLayer;
            BoxCollider randomSpawnPlane = spawnPlanes[Random.Range(0, spawnPlanes.Length - 1)];
            BoxCollider newEnemyBox = newEnemy.GetComponent<BoxCollider>();
            if (newEnemyBox != null)
            {
                float xPlaneExtent = (1 - safeZonePercent) * randomSpawnPlane.bounds.extents.x;
                float zPlaneExtent = (1 - safeZonePercent) * randomSpawnPlane.bounds.extents.z;
                float RandomXinPlane = Random.Range(-xPlaneExtent, xPlaneExtent);
                float RandomZinPlane = Random.Range(-zPlaneExtent, zPlaneExtent);
                Vector3 spawnPosition = new Vector3(RandomXinPlane, newEnemyBox.size.y / 2, RandomZinPlane) + randomSpawnPlane.transform.localPosition;
                newEnemy.transform.localPosition = spawnPosition;

                EnemyBase enemyAI = newEnemy.GetComponent<EnemyBase>();
                enemyAI.target = playerWorld.CurrentPlayerGameObject.transform;
                enemyAI.Initialize();
            }
        }
    }


}
