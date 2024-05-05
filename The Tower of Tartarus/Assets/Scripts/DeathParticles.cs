using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] float duration;
    void Awake()
    {
        this.GetComponent<ParticleSystem>().Play();
        Invoke("StopParticles", duration);
    }

    void StopParticles(){
        this.GetComponent<ParticleSystem>().Stop();
    }
}
