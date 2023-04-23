using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject bomb;
    public float damage = 10f;
    public float range = 100f;
    public Transform firePoint;
    
    Quaternion bullet;

    public void Fire()
    {
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 4f);
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        bullet = Random.rotation;
        Quaternion rot = Quaternion.RotateTowards(firePoint.transform.rotation, bullet,0);
        Instantiate(projectilePrefab, firePoint.transform.position, rot);
    }

    public void Throw()
    {
        Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.yellow, 4f);
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        bullet = Random.rotation;
        Quaternion rot = Quaternion.RotateTowards(firePoint.transform.rotation, bullet, 0);
        Instantiate(bomb, firePoint.transform.position, rot);
        //GameObject bomb = Instantiate(Bomb, firePoint.transform.position, Quaternion.identity);
        //bomb.name = "Bomb";
        //Vector3 direction = bomb.transform.forward - bomb.transform.position;
        //bomb.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
    
}
