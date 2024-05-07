using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthItem : MonoBehaviour
{
    HealthManager healthManager;
    ItemManager itemManager;
    AudioManager audioManager;
    void Awake(){
        GameObject playerObj = GameObject.Find("Player");
        healthManager = playerObj.GetComponent<HealthManager>();
        itemManager = playerObj.GetComponent<ItemManager>();
        GameObject audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();
    }

    //adds one maxhealth and currhealth
    // splinter of aegis
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            itemManager.UpdateItemUI(3);
            healthManager.maxHealth += 1;
            healthManager.currentHealth += 1;
            healthManager.UpdateHealthUI();
            audioManager.PlaySFX(audioManager.itemPickup);
            Destroy(this.gameObject);
        }
    }
}
