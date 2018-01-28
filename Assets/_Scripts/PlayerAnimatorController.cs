using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour 
{
	PlayerControl player;
	Animator anim;
	public float rotationalYOffset;

	// Use this for initialization
	void Start () 
	{
		player = GetComponentInParent<PlayerControl> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		var locVel = transform.InverseTransformDirection (player.rb.velocity);
		anim.SetFloat ("ForwardMovement", locVel.z);
		anim.SetBool ("isAiming", player.isAiming());
		if (player.isAiming ()) {
			transform.localEulerAngles = (new Vector3 (0, rotationalYOffset, 0));
		} 
		else 
		{
			transform.localRotation = Quaternion.identity;
		}
		if ( (Input.GetKey(KeyCode.F) || Input.GetButtonDown("Fire1") ) && player.currentFireCD <= 0)
		{
			anim.SetTrigger ("Shoot");	
		}
		if (false) 
		{
			anim.SetTrigger ("SwordSwing");			
		}
	}
}
