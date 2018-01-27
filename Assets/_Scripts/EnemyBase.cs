using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private float walkingSpeed;

    [SerializeField]
    private float attackRadius = 5;

    [SerializeField]
    private int health = 1000;

    public Transform target { set; get; }

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if(target != null)
        {
            Vector3 targetVector = (target.position - transform.position);
            if(targetVector.magnitude < attackRadius)
            {
                transform.position += (target.position - transform.position).normalized * walkingSpeed * Time.deltaTime;
            }
        }
    }

    public void GetHit(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Trigger VFX
        //Tell EnemySpawner to transfer us?
    }
}
