using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthProjectileThrower : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    public void Launch(Vector3 targetPos){

        GameObject newProjectile = Instantiate(projectilePrefab,transform.position,Quaternion.identity);

        Destroy(newProjectile,30);
    }
}
