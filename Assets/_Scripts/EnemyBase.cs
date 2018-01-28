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

    [SerializeField]
    private int damage = 100;

    [SerializeField]
    private float bounceDistance = 2f;

    public Transform target { set; get; }
    public EnemyState CurrentState { private set { currentState = value; } get { return currentState;  } }
    public int Damage { private set { damage = value; } get { return damage; } }

    private EnemyState currentState;
    private int currentHealth;

	void Start ()
    {
        Initialize();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Bullet.BULLET_TAG))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            GetHit(bullet.Damage, other.transform);
        }
    }

    public void Initialize()
    {
        currentHealth = maxHealth;
        currentState = EnemyState.Idle;
        gameObject.SetActive(true);
    }

    public void GetHit(int damage, Transform attacker, float bounceMultiplier = 1f)
    {
        Vector3 bounceDirection = (transform.position - attacker.position).normalized;
        bounceDirection *= bounceDistance * bounceMultiplier;
        transform.Translate(new Vector3(bounceDirection.x, 0, bounceDirection.z));

        currentHealth -= damage;

        if(currentState != EnemyState.Dead && currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        currentState = EnemyState.Dead;
        gameObject.SetActive(false);
        //Trigger VFX
    }
}
