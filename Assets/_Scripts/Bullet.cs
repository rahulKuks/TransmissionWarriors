using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage = 1000;

    public static readonly string BULLET_TAG = "Bullet";

    private float LifeTime = 10f; //10s is long enough for the bullet to travel anywhere

    public int Damage { get { return damage; } private set { damage = value; } }

	void Update ()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0) { Destroy(this.gameObject); }
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
