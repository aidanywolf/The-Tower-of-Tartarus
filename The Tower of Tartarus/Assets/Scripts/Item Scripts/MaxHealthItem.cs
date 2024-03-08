using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthItem : MonoBehaviour
{
    HealthManager healthManager;
    ItemManager itemManager;
    void Awake(){
        GameObject playerObj = GameObject.Find("Player");
        healthManager = playerObj.GetComponent<HealthManager>();
        itemManager = playerObj.GetComponent<ItemManager>();

    }

    //adds one maxhealth and currhealth
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            itemManager.UpdateItemUI(3);
            healthManager.maxHealth += 1;
            healthManager.currentHealth += 1;
            healthManager.UpdateHealthUI();
            Destroy(this.gameObject);
        }
    }
}
