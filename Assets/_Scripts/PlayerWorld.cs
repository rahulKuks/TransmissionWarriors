
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorld : MonoBehaviour
{
    private enum PlayerLayer
    {
        Player1World,
        Player2World
    }

    [SerializeField]
    private PlayerLayer playerLayer;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private EnemySpawner enemySpawner;

    public int CurrentPlayerLayer { get { return LayerMask.NameToLayer( playerLayer.ToString() );} }
    public GameObject CurrentPlayerGameObject { get { return playerGameObject; } }

    [HideInInspector]
    public List<EnemyBase> enemiesToTransfer = new List<EnemyBase>();

    void Start ()
    {
		
	}

    private void Update()
    {
    }

    public void RecieveEnemy(GameObject transferredEnemy)
    {
        enemySpawner.SpawnTransferredEnemy(transferredEnemy);
    }

}
