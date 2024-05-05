using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject destroyedObstacle;
    [SerializeField] ParticleSystem deathParticles;

    public void DestroyObstacle(){

        GameObject obstacleDebris = Instantiate(destroyedObstacle,transform.position,Quaternion.identity);
        obstacleDebris.transform.parent = this.transform.parent;
        deathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}