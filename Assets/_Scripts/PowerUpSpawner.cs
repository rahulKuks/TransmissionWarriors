using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUpsPrefabs;

    [SerializeField]
    private BoxCollider spawnPlane;

    [SerializeField]
    private PlayerWorld playerWorld;

    [SerializeField]
    private float safeZonePercent = 0.05f;

    [SerializeField]
    private float spawnInterval;

    private float timer = 0;


    private void SpawnPowerup()
    {
        if(spawnPlane != null)
        {
            int randomIndex = Random.Range(0, powerUpsPrefabs.Length);
            GameObject newPowerUp = Instantiate(powerUpsPrefabs[randomIndex], transform);
            newPowerUp.layer = playerWorld.CurrentPlayerLayer;

            BoxCollider newPowerUpBox = newPowerUp.GetComponent<BoxCollider>();
            if (newPowerUpBox != null)
            {
                float xPlaneExtent = (1 - safeZonePercent) * spawnPlane.bounds.extents.x;
                float zPlaneExtent = (1 - safeZonePercent) * spawnPlane.bounds.extents.z;
                float RandomXinPlane = Random.Range(-xPlaneExtent, xPlaneExtent);
                float RandomZinPlane = Random.Range(-zPlaneExtent, zPlaneExtent);
                Vector3 spawnPosition = new Vector3(RandomXinPlane, newPowerUpBox.size.y / 2, RandomZinPlane) + spawnPlane.transform.localPosition;
                newPowerUp.transform.localPosition = spawnPosition;
            }
        }
    }
	
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            timer = spawnInterval - timer;
            SpawnPowerup();
        }
    }
}
