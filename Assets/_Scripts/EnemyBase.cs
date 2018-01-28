using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Attacking,
        Dead
    }

    [SerializeField]
    private float walkingSpeed;

    [SerializeField]
    private float attackRadius = 5;

    [SerializeField]
    private int maxHealth = 1000;

    public Transform target { set; get; }
    public EnemyState CurrentState { private set { currentState = value; } get { return currentState;  } }

    private EnemyState currentState;
    private int currentHealth;

	void Start ()
    {
        currentState = EnemyState.Idle;
    }
	
	void Update ()
    {
        if(target != null && currentState != EnemyState.Dead)
        {
            Vector3 targetVector = (target.position - transform.position);
            if(targetVector.magnitude < attackRadius)
            {
                transform.position += (target.position - transform.position).normalized * walkingSpeed * Time.deltaTime;
                currentState = EnemyState.Attacking;
            }

            currentState = EnemyState.Idle;
        }
    }

    public void Initialize()
    {
        currentHealth = maxHealth;
        currentState = EnemyState.Idle;
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;

        if(currentState != EnemyState.Dead && currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        currentState = EnemyState.Dead;
        //Trigger VFX
    }
}
