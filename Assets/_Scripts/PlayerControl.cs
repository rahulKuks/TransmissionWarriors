using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum PlayerTag { Player1, Player2};
    public PlayerTag tag = PlayerTag.Player1;
    public enum Direction { North, East, South, West,NE,NW,SE,SW };
    public Direction facing = Direction.South;
    public float speed = 0.5f;
    public float jumpPower = 0.3f;
    public Rigidbody rb;
    private float onAirTime = 0.0f;
    public float jumpCD = 0.5f;//the time the player can hold the jump button and it's still effective, holding it after this time will just fall down
    public float fireCD = 0.15f;
    private float currentFireCD = 0f;
    public Gun gun;
    public int maxHP = 50;
    public int currentHP;
    public float immuneTime = 2f;
    private float remainingImmuneTime = 0f;
<<<<<<< HEAD
    public float bonceDistance = 0.3f;
    public float meleeCD = 3f;
    private float currentMeleeCd = 0f;
    public int meleeDamage = 100;
=======
    public float bounceDistance = 0.3f;
>>>>>>> a6d299d4cbbf12bd2404dc0c87305c8f77270c01

    [SerializeField]
    private PlayerWorld currentPlayerWorld;

                              // Use this for initialization
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentMeleeCd -= Time.deltaTime;
        if (tag == PlayerTag.Player1)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //pressing direction key(s)
            {
                setDirection();
                if (!isAiming())//not aiming means: moving
                {
                    //  movement 
                    float x = Input.GetAxis("Horizontal");
                    float z = Input.GetAxis("Vertical");

                    rb.velocity += new Vector3(x, 0, z).normalized * speed;
                }
            }


            // jump

            if (Input.GetKey(KeyCode.C)) //jump input
            {

                onAirTime += Time.deltaTime;
                //check is jump input in valid (can keep jumping (as strength of jump) for a short while, after that character falls down, this time counter reset when the player hits the ground)
                if (onAirTime < jumpCD)
                {
                    rb.velocity += Vector3.up * jumpPower;
                }

            };

            // aim, then fire
            currentFireCD -= Time.deltaTime;
            if (isAiming())
            {
                if ( (Input.GetKey(KeyCode.F) || Input.GetButtonDown("Fire1") ) && currentFireCD <= 0)
                {
                    gun.Fire(currentPlayerWorld.CurrentPlayerLayer);
                    currentFireCD = fireCD;
                }
            }


            //  Debug.Log("OnAirTime" + onAirTime);
            remainingImmuneTime -= Time.deltaTime;
        }
        else if (tag == PlayerTag.Player2)
        {
            if (Input.GetAxis("Horizontal2") != 0 || Input.GetAxis("Vertical2") != 0) //pressing direction key(s)
            {
                setDirection();
                if (!isAiming())//not aiming means: moving
                {
                    //  movement 
                    float x = Input.GetAxis("Horizontal2");
                    float z = Input.GetAxis("Vertical2");

                    rb.velocity += new Vector3(x, 0, z).normalized * speed;
                }
            }


            // jump

            if (Input.GetKey(KeyCode.J)) //jump input
            {

                onAirTime += Time.deltaTime;
                //check is jump input in valid (can keep jumping (as strength of jump) for a short while, after that character falls down, this time counter reset when the player hits the ground)
                if (onAirTime < jumpCD)
                {
                    rb.velocity += Vector3.up * jumpPower;
                }

            };

            // aim, then fire
            currentFireCD -= Time.deltaTime;
            if (isAiming())
            {
                if ((Input.GetKey(KeyCode.L) || Input.GetButtonDown("Fire2")) && currentFireCD <= 0)
                {
                    gun.Fire(currentPlayerWorld.CurrentPlayerLayer);
                    currentFireCD = fireCD;
                }
            }


            //  Debug.Log("OnAirTime" + onAirTime);
            remainingImmuneTime -= Time.deltaTime;
        }
    }

    void TakeDamage(int monsterAttack)
    {
        if (currentHP > monsterAttack)
        {
            currentHP -= monsterAttack;
            remainingImmuneTime = immuneTime;
        }
        else
        {
            currentHP = 0;
        }
    }
    //helper functions
    bool isAiming()
    {
        if (tag== PlayerTag.Player1)
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Aim1");
        }
        else //if (tag == PlayerTag.Player2)
        {
            return Input.GetKey(KeyCode.K) || Input.GetButton("Aim2");
        }
        
    }

    void Melee()
    {
        //TODO
    }
    void Die()
    {
        Destroy(gameObject);
        //TODO
    }

    void setDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //discard the previous lines, for player2, check on player2 input sets
        if (tag == PlayerTag.Player2)
        {
            x = Input.GetAxis("Horizontal2");
            z = Input.GetAxis("Vertical2");
        }
        if (x != 0) //has x componet in world coodinate
        {
            if (x > 0)//somewhere east
            {
                if(z < 0) // somewhere south
                {
                    facing = Direction.SE;
                    transform.rotation= Quaternion.LookRotation(new Vector3(1, 0, -1));

                }
                else if (z > 0) // somewhere north
                {
                    facing = Direction.NE;
                    transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
                }
                else
                {
                    facing = Direction.East;
                    transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                }
            }
            else if (x < 0) // somewhere west
            {
                if (z < 0) // somwhere south
                {
                    facing = Direction.SW;
                    transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, -1));
                }
                else if (z > 0)//somewhere north
                {
                    facing = Direction.NW;
                    transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 1));
                }
                else
                {
                    facing = Direction.West;
                    transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
                }
            }
            
        }
        else // there is no x component in world coordinate
        {
            if (z > 0) //south
            {
                facing = Direction.South;
                transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
            }
            else if (z < 0)
            {
                facing = Direction.North;
                transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
            }
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Floor"))//check collision with floor...
        {
            if (collision.contacts.Length > 0)//with your legs, not head...
            {
                ContactPoint contact = collision.contacts[0];
                if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
                {
                    onAirTime = 0f;//reset on air time
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            if (remainingImmuneTime <= 0)
            {
                Vector3 bounceDirection = (transform.position - enemy.transform.position).normalized;
                bounceDirection *= bounceDistance;
                transform.Translate( new Vector3(bounceDirection.x, 0, bounceDirection.z) );

                TakeDamage(enemy.Damage);
                if (currentHP <= 0)
                {
                    Die();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)//trigger collider for melee attack
    {
        if (currentMeleeCd > 0)
        {
            return;
        }
        if(Vector3.Dot((other.transform.position-transform.position)/*vector from player to enemy*/, transform.forward) > 0)
        {
            if (tag == PlayerTag.Player1)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    Melee();
                }
            }
            else if (tag == PlayerTag.Player2)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    Melee();
                }
            }
        }
        
    }
}
