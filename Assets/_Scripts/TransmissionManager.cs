using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionManager : MonoBehaviour
{
    [SerializeField]
    PlayerWorld[] playerWorlds;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        for (int i = 0; i < playerWorlds.Length; i++)
        {
            if(playerWorlds[i].enemiesToTransfer.Count > 0)
            {
                foreach(EnemyBase enemy in playerWorlds[i].enemiesToTransfer)
                {
                    int nextPlayerWorld = (i + 1) % playerWorlds.Length;
                    playerWorlds[nextPlayerWorld].RecieveEnemy(enemy.gameObject);
                }
            }

            playerWorlds[i].enemiesToTransfer.Clear();
        }
	}
}
