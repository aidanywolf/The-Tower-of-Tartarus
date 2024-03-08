using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject destroyedObstacle;

    public void DestroyObstacle(){

        GameObject obstacleDebris = Instantiate(destroyedObstacle,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}