using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float speed = 5;
    public void Launch(Vector3 targetPos){
        //receives target pos, gen projectile, rotates in direction of target pos, moves in direction of target pos
        GameObject newProjectile = Instantiate(projectilePrefab,transform.position,Quaternion.identity);
        
        newProjectile.transform.position = new Vector3(newProjectile.transform.position.x, newProjectile.transform.position.y, -5f);

        newProjectile.transform.rotation = Quaternion.LookRotation(transform.forward,targetPos - transform.position);
        newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.up * speed;

        //projectile dies after 15 seconds
        Destroy(newProjectile,15);
    }
}