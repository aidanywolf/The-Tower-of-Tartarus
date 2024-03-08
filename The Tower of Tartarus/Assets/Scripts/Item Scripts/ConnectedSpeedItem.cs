using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnconnectedSpeedItem : MonoBehaviour
{
    Player player;
    ItemManager itemManager;
    [SerializeField] float increaseAmount;
    void Awake(){
        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        itemManager = playerObj.GetComponent<ItemManager>();
    }

    //increases connected move speed
    //herculian toe nail
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player"))
        {
            itemManager.UpdateItemUI(2);
            player.connectedSpeed += increaseAmount;
            Destroy(this.gameObject);
        }
    }
}
