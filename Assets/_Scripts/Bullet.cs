using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int Damage = 5;
    private float LifeTime = 10f; //10s is long enough for the bullet to travel anywhere
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0) { Destroy(this.gameObject); }
	}
}
