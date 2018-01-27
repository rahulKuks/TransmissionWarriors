
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

    public int CurrentPlayerLayer { get { return LayerMask.NameToLayer( playerLayer.ToString() );} }

    private List<EnemyBase> enemiesToTransfer = new List<EnemyBase>();

    void Start ()
    {
		
	}
	
}
