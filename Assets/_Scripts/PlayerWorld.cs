
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

    public int CurrentPlayerLayer { get { return LayerMask.NameToLayer( playerLayer.ToString() );} }
    public GameObject CurrentPlayerGameObject { get { return playerGameObject; } }

    void Start ()
    {
		
	}
	
}
