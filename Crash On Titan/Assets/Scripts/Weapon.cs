using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    public Transform firePoint; // location to intantiate the bullet from

    public GameObject bulletPrefab; // The object to spawn

    // Update is called once per frame
    void Update()
    {
        if(CrossPlatformInputManager.GetButtonDown("Fire1")) // if the on screen button is recognised as the Fire1 button
        {
            Shoot(); // Shoot a bullet
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Spawn the bullet at the position and rotation of the declared location
    }
}
