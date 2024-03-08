using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;

    void Awake(){
        GameObject player = GameObject.Find("Player");
        healthManager = player.GetComponent<HealthManager>();
    }

    //adds one health if curr health is less than max health
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && healthManager.currentHealth < healthManager.maxHealth)
        {
            healthManager.currentHealth += 1;
            healthManager.UpdateHealthUI();
            Destroy(this.gameObject);
        }
    }
}
