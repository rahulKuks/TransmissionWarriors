using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour 
{
	PlayerControl player;
	Animator anim;
	public float rotationalYOffset;
	public float TimeToLowerGun = 0.5f;
	float timer;

	// Use this for initialization
	void Start () 
	{
		player = GetComponentInParent<PlayerControl> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		var locVel = transform.InverseTransformDirection (player.rb.velocity);
		anim.SetFloat ("ForwardMovement", locVel.z);
		//anim.SetBool ("isAiming", player.isAiming());
		if (player.isAiming ()) {
			transform.localEulerAngles = (new Vector3 (0, rotationalYOffset, 0));
		} 
		else 
		{
			transform.localRotation = Quaternion.identity;
		}
		if ( (Input.GetKey(KeyCode.F) || Input.GetButton("Fire1") ))
		{
			//anim.SetTrigger ("Shoot");	
			anim.SetBool ("isAiming", true);
			timer = 0;
		} 
		else if (timer >= TimeToLowerGun)
		{
			anim.SetBool ("isAiming", false);
		}
		if (false) 
		{
			anim.SetTrigger ("SwordSwing");			
		}
	}
}
