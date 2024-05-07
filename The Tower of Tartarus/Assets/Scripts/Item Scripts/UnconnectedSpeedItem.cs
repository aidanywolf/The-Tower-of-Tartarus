using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollForceItem : MonoBehaviour
{
    Player player;
    ItemManager itemManager;
    AudioManager audioManager;
    [SerializeField] float increaseAmount;
    void Awake(){
        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        itemManager = playerObj.GetComponent<ItemManager>();
        GameObject audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();
    }

    //increases unconnected speed
    //coin of hermes
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            itemManager.UpdateItemUI(0);
            player.unconnectedSpeed += increaseAmount;
            audioManager.PlaySFX(audioManager.itemPickup);
            Destroy(this.gameObject);
        }
    }
}
