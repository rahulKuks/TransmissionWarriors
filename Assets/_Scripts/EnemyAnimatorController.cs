using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour {

	EnemyBase enemy;
	Animator anim;

	// Use this for initialization
	void Start () 
	{
		enemy = GetComponentInParent<EnemyBase> ();
		anim = GetComponent<Animator> ();
		anim.SetFloat ("Move", 1);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
