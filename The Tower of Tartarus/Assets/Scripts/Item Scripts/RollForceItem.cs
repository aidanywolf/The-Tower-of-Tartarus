using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnconnectedMoveSpeedItem : MonoBehaviour
{
    BoulderRoller boulderRoller;
    [SerializeField] float increaseAmount;
    ItemManager itemManager;
    AudioManager audioManager;
    void Awake(){
        GameObject playerObj = GameObject.Find("Player");
        boulderRoller = playerObj.GetComponent<BoulderRoller>();
        itemManager = playerObj.GetComponent<ItemManager>();
        GameObject audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();
    }

    //increases boulder roll force
    //hair of ares
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            itemManager.UpdateItemUI(1);
            boulderRoller.force += increaseAmount;
            audioManager.PlaySFX(audioManager.itemPickup);
            Destroy(this.gameObject);
        }
    }
}