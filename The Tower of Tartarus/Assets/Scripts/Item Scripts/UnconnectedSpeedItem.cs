using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollForceItem : MonoBehaviour
{
    Player player;
    ItemManager itemManager;
    [SerializeField] float increaseAmount;
    void Awake(){
        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        itemManager = playerObj.GetComponent<ItemManager>();

    }

    //increases unconnected speed
    //coin of hermes
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            itemManager.UpdateItemUI(0);
            player.unconnectedSpeed += increaseAmount;
            Destroy(this.gameObject);
        }
    }
}
