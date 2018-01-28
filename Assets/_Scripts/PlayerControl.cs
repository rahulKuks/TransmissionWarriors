using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
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
    public float bonceDistance = 0.3f;

                              // Use this for initialization
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //pressing direction key(s)
        {
            setDirection();
            Debug.Log("Direction:" + facing);
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
            if (Input.GetKey(KeyCode.F) && currentFireCD<=0)
            {
                gun.Fire();
                currentFireCD = fireCD;
            }
        }


        //  Debug.Log("OnAirTime" + onAirTime);
        remainingImmuneTime -= Time.deltaTime;

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
        return Input.GetKey(KeyCode.LeftShift);
    }

    void Die()
    {
        //TODO
    }

    void setDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (Input.GetAxis("Horizontal") != 0) //has x componet in world coodinate
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
        else if (collision.collider.tag.Equals("Enemy"))
        {
            
            EnemyBase enemy = collision.collider.gameObject.GetComponent<EnemyBase>();
            if (remainingImmuneTime <= 0)
            {
                Vector3 bonceDirection = (transform.position - enemy.transform.position).normalized;
                transform.Translate(bonceDirection * bonceDistance);
                //TODO
                //TakeDamage(enemy.Attack);
                if (currentHP <= 0)
                {
                    Die();
                }
            }
           
        }

    }

}
