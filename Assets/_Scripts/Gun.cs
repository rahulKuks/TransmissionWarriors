using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Bullet bulletPrefeb;
    public float bulletVelocity = 15f;

	// Use this for initialization
	void Start () {
		
	}

    public void Fire()
    {
       
        Bullet bullet = Instantiate(bulletPrefeb,this.transform.position+ transform.up*2f, this.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletVelocity;

     
    }
}
