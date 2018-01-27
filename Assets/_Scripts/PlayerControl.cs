using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool facingRight = true;
    public float speed = 0.5f;
    public float jumpPower = 0.3f;
    public Rigidbody rb;
    private float onAirTime = 0.0f;
    public float jumpCD = 0.5f;//the time the player can hold the jump button and it's still effective, holding it after this time will just fall down

    public AimingLine aimingLine;
    //note that max and min angles are how many degrees up or down, regardless on the direction the character is facing
    public float maxAimAngle = 80f;
    public float minAimAngle = -60f;
    public float aimingSpeed = 15f;//how fast is the aiming angle rotating
    public float aimingAngle = 0;//note that this angle is the absolute angle goes all 360 degrees, it matters for witch way the charater is facing. It is public just for debug inspection.
                                 // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // horizontal movement 
        float x = Input.GetAxis("Horizontal") * speed;
        //check which way the character is facing
        if (x > 0)
        {
            if(facingRight == false)
            {
                Flip();
            }
        
        }
        else if(x<0)
        {
            if (facingRight == true)
            {
                Flip();
            }
        }

        rb.velocity += new Vector3(x, 0, 0);
        // vertical jump

        if (Input.GetAxis("Vertical") > 0) //jump input
        {

            onAirTime += Time.deltaTime;
            //check is jump input in valid (can keep jumping (as strength of jump) for a short while, after that character falls down, this time counter reset when the player hits the ground)
            if (onAirTime < jumpCD)
            {
                rb.velocity += Vector3.up * jumpPower;
            }

        };

        // aiming
        if (Input.GetAxis("AimAngle") != 0)
        {

            if (facingRight)
            {
                if(aimingAngle +Time.deltaTime* aimingSpeed*Input.GetAxis("AimAngle") > maxAimAngle)
                {
                    Debug.Log("A");
                    //do nothing when it is reaching the top limit
                }
                else if (aimingAngle + Time.deltaTime * aimingSpeed * Input.GetAxis("AimAngle") < minAimAngle)
                {
                    //do nothing when it is reaching the buttom limit
                    Debug.Log("B");
                }
                else
                {
                    aimingLine.transform.Rotate(Vector3.forward, Time.deltaTime * aimingSpeed * Input.GetAxis("AimAngle"));
                    aimingAngle += Time.deltaTime * aimingSpeed * Input.GetAxis("AimAngle");
                }
                
            }
            else //facing left
            {
                if (aimingAngle - Time.deltaTime * aimingSpeed * Input.GetAxis("AimAngle") > maxAimAngle)
                {
                    //do nothing when it is reaching the top limit
                }
                else if (aimingAngle - Time.deltaTime * aimingSpeed * Input.GetAxis("AimAngle") < minAimAngle)
                {
                    //do nothing when it is reaching the buttom limit
                }
                aimingLine.transform.Rotate(Vector3.back, Time.deltaTime * aimingSpeed* Input.GetAxis("AimAngle"));
                aimingAngle += Time.deltaTime * aimingSpeed * Input.GetAxis("AimAngle");
            }
        }
        //  Debug.Log("OnAirTime" + onAirTime);

       // Debug.Log("AimingAngle" +aimingAngle);

    }
    void Flip()
    {
        
        if (facingRight)
        {
            facingRight = false;

        }
        else
        {
            facingRight = true;
        }
        transform.Rotate(Vector3.up, 180);
        aimingAngle = 180 - aimingAngle;
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

}
