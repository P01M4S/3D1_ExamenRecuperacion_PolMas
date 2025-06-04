using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public Transform _bullet;
    public GameObject bulletPrefab;

    public bool canShoot = false;

   
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    void shoot()
    {
        Instantiate(bulletPrefab, _bullet.position, _bullet.rotation);
    }

}
