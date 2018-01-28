using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepad : MonoBehaviour {

    public Telepad target;
    public float teleportCD = 5f;
    private float currentTeleportCD =0; //note that this cold down is for arrival, when you teleported here, you don't get teleport back right away
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentTeleportCD -= Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (currentTeleportCD <= 0)
        {
            if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Enemy"))
            {
                other.gameObject.transform.position = target.transform.position+new Vector3(0,2,0);//drop a bit when teleported to the new location
                target.Arrive();
            }
        }
        
    }

    public void Arrive()
    {
        currentTeleportCD = teleportCD;
    }
}
