using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Dead
    }

    public enum PlayerTag { Player1, Player2};
    public PlayerTag PlayerID = PlayerTag.Player1;
    public enum Direction { North, East, South, West,NE,NW,SE,SW };
    public Direction facing = Direction.South;
    public float speed = 0.5f;
    public float jumpPower = 0.3f;
    public Rigidbody rb;
    private float onAirTime = 0.0f;
    public float jumpCD = 0.5f;//the time the player can hold the jump button and it's still effective, holding it after this time will just fall down
    public float fireCD = 0.15f;
	[HideInInspector]
    public float currentFireCD = 0f;
    public Gun gun;
    public int maxHP = 50;
    public int currentHP;
    public float immuneTime = 2f;
    private float remainingImmuneTime = 0f;
    public float bounceDistance = 0.3f;
    public float meleeCD = 3f;
	public float currentMeleeCd = 0f;
    public int meleeDamage = 100;
    public float meleeBounceStrength = 3.0f; //how much the melee attack will bounce the enemy away
	public bool isAttacking;

    //audio
    public AudioSource MeleeSFX;
    public AudioSource RangeSFX;

    public HpBar hpBar;

    [SerializeField]
    float meleeRange = 2f;

    [SerializeField]
    float meleeWidth = 1.5f;


    [SerializeField]
    Camera cam;


    public PlayerState CurrentState { private set; get; }

    [SerializeField]
    private PlayerWorld currentPlayerWorld;

                              // Use this for initialization
    void Start()
    {
        currentHP = maxHP;
        CurrentState = PlayerState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        currentMeleeCd -= Time.deltaTime;
        if (PlayerID == PlayerTag.Player1)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //pressing direction key(s)
            {
                setDirection();
                if (!isAiming())//not aiming means: moving
                {
                    //  movement 
                    float x = Input.GetAxis("Horizontal");
                    float z = Input.GetAxis("Vertical");

                    rb.velocity += getCameraMovement(x, z).normalized * speed;
                }
            }
            
            if (isAiming())
            {
                rb.velocity = new Vector3(0,0,0);
            }


            // jump

            if (Input.GetKey(KeyCode.V) || Input.GetButtonDown("Melee")) //Melee input
            {

                /*onAirTime += Time.deltaTime;
                //check is jump input in valid (can keep jumping (as strength of jump) for a short while, after that character falls down, this time counter reset when the player hits the ground)
                if (onAirTime < jumpCD)
                {
                    rb.velocity += Vector3.up * jumpPower;
                }*/
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, meleeWidth, transform.forward, meleeRange);

                if ((currentMeleeCd < 0))
                {
                    foreach (RaycastHit hit in hits)
                        if ((hit.transform.tag == "Enemy"))
                        {
                            Melee(hit.transform.gameObject.GetComponent<EnemyBase>());
						}
					currentMeleeCd = meleeCD;

                }


            };

            // aim, then fire
            currentFireCD -= Time.deltaTime;
           // if (isAiming())
            //{
                if ( (Input.GetKey(KeyCode.F) || Input.GetButtonDown("Fire1") ) && currentFireCD <= 0)
                {
                    gun.Fire(currentPlayerWorld.CurrentPlayerLayer);
                RangeSFX.Play();
                    currentFireCD = fireCD;
                }
           // }


            //  Debug.Log("OnAirTime" + onAirTime);
            remainingImmuneTime -= Time.deltaTime;
        }
        else if (PlayerID == PlayerTag.Player2)
        {
            if (Input.GetAxis("Horizontal2") != 0 || Input.GetAxis("Vertical2") != 0) //pressing direction key(s)
            {
                setDirection();
                if (!isAiming())//not aiming means: moving
                {
                    //  movement 
                    float x = Input.GetAxis("Horizontal2");
                    float z = Input.GetAxis("Vertical2");

                    rb.velocity += getCameraMovement( x, z).normalized * speed;
                }
            }
            
            if (isAiming())
            {
                rb.velocity = new Vector3(0, 0, 0);
            }


            // jump

            if (Input.GetKey(KeyCode.J) || Input.GetButtonDown("Melee2")) //melee input
            {

                /* onAirTime += Time.deltaTime;
                 //check is jump input in valid (can keep jumping (as strength of jump) for a short while, after that character falls down, this time counter reset when the player hits the ground)
                 if (onAirTime < jumpCD)
                 {
                     rb.velocity += Vector3.up * jumpPower;
                 }*/

                RaycastHit[] hits = Physics.SphereCastAll(transform.position, meleeWidth, transform.forward, meleeRange);

                if ((currentMeleeCd < 0))
                {
                    foreach (RaycastHit hit in hits)
                        if ((hit.transform.tag == "Enemy"))
                        {
                            Melee(hit.transform.gameObject.GetComponent<EnemyBase>());
                        }

					currentMeleeCd = meleeCD;
                }

            };

            // aim, then fire
            currentFireCD -= Time.deltaTime;
           // if (isAiming())
           // {
                if ((Input.GetKey(KeyCode.L) || Input.GetButtonDown("Fire2")) && currentFireCD <= 0)
                {
                    gun.Fire(currentPlayerWorld.CurrentPlayerLayer);
                RangeSFX.Play();
                currentFireCD = fireCD;
                }
          //  }


            //  Debug.Log("OnAirTime" + onAirTime);
            remainingImmuneTime -= Time.deltaTime;
        }

        if (cam != null)
        {
            hpBar.transform.LookAt(hpBar.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        }
    }

    void TakeDamage(int monsterAttack)
    {
        hpBar.setHP((float)currentHP / (float)maxHP);
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
    public bool isAiming()
    {
        if (PlayerID == PlayerTag.Player1)
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Aim1");
        }
        else //if (tag == PlayerTag.Player2)
        {
            return Input.GetKey(KeyCode.K) || Input.GetButton("Aim2");
        }
        
    }

    void Melee(EnemyBase enemy)
    {
        enemy.GetHit(meleeDamage, this.gameObject.transform, meleeBounceStrength);
        MeleeSFX.Play();
    }
    void Die()
    {
        CurrentState = PlayerState.Dead;
        gameObject.SetActive(false);
    }

    void setDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
   
        //discard the previous lines, for player2, check on player2 input sets
        if (PlayerID == PlayerTag.Player2)
        {
            x = Input.GetAxisRaw("Horizontal2");
            z = Input.GetAxisRaw("Vertical2");
        }
        if (x != 0) //has x componet in world coodinate
        {
            if (x > 0)//somewhere east
            {
                if (z < 0) // somewhere south
                {
                    facing = Direction.SE;
                    Vector3 direction = ((cam.transform.right) + (cam.transform.forward * -1)).normalized;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                    // transform.rotation= Quaternion.LookRotation(new Vector3(1, 0, -1));

                }
                else if (z > 0) // somewhere north
                {
                    facing = Direction.NE;
                    Vector3 direction = ((cam.transform.right) + (cam.transform.forward)).normalized;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                    //transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 1));
                }
                else
                {
                    facing = Direction.East;
                    Vector3 direction = cam.transform.right;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                    //transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                }
            }
            else if (x < 0) // somewhere west
            {
                if (z < 0) // somwhere south
                {
                    facing = Direction.SW;
                    Vector3 direction = ((cam.transform.right * -1) + (cam.transform.forward * -1)).normalized;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                    //transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, -1));
                }
                else if (z > 0)//somewhere north
                {
                    facing = Direction.NW;
                    Vector3 direction = ((cam.transform.right * -1) + (cam.transform.forward)).normalized;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                    //transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 1));
                }
                else
                {
                    facing = Direction.West;
                    Vector3 direction = cam.transform.right * -1;
                    direction.y = 0;
                    transform.rotation = Quaternion.LookRotation(direction);
                    //transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
                }
            }

        }
        else // there is no x component in world coordinate
        {
            if (z > 0) //south
            {
                facing = Direction.North;
                //transform.rotation = Quaternion.LookRotation(new Vector3(.5f, 0, 1));
                Vector3 direction = cam.transform.forward;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);

            }
            else if (z < 0)
            {
                facing = Direction.South;
                Vector3 direction = cam.transform.forward * -1;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
                //   transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));


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

    private void OnTriggerStay(Collider other)//trigger collider for melee attack
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            if (remainingImmuneTime <= 0)
            {
                Vector3 bounceDirection = (enemy.transform.position - transform.position).normalized;
                bounceDirection *= bounceDistance;
                transform.Translate(new Vector3(bounceDirection.x, 0, bounceDirection.z));

                TakeDamage(enemy.Damage);
                if (currentHP <= 0)
                {
                    Die();
                }
            }
        }
    }

    Vector3 getCameraMovement(float x, float z)
    {
        if (z > 0)
        {
            if (x == 0)
            {
                Vector3 direction = cam.transform.forward;
                return new Vector3(direction.x, 0, direction.z);

            }
            else
            if (x > 0)
            {
                Vector3 direction = (cam.transform.forward + cam.transform.right).normalized;
                return new Vector3(direction.x, 0, direction.z);
            }
            else
            {
                Vector3 direction = (cam.transform.forward + cam.transform.right * -1).normalized;
                return new Vector3(direction.x, 0, direction.z);
            }
        }
        else
        if (z < 0)
        {
            if (x == 0)
            {
                Vector3 direction = cam.transform.forward * -1;
                return new Vector3(direction.x, 0, direction.z);

            }
            else
                if (x > 0)
            {
                Vector3 direction = ((cam.transform.forward * -1) + (cam.transform.right));
                return new Vector3(direction.x, 0, direction.z);
            }
            else
                if (x < 0)
            {
                Vector3 direction = ((cam.transform.forward * -1) + (cam.transform.right * -1));
                return new Vector3(direction.x, 0, direction.z);
            }

        }
        else
        if (z == 0)
        {

            if (x > 0)
            {
                Vector3 direction = cam.transform.right;
                return new Vector3(direction.x, 0, direction.z);
            }
            if (x < 0)
            {
                Vector3 direction = cam.transform.right * -1;
                return new Vector3(direction.x, 0, direction.z);
            }
        }

        return new Vector3(x, 0, z);
    }

}



