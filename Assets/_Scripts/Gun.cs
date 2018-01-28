using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefeb;

    [SerializeField]
    private float bulletVelocity = 15f;

    public void Fire(int layer)
    {
        Bullet bullet = Instantiate(bulletPrefeb, this.transform.position+ transform.up * 2f, this.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletVelocity;
        bullet.gameObject.layer = layer;
    }
}
