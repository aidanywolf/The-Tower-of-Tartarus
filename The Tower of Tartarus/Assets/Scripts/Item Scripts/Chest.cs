using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject destroyedChest;

    public void DestroyChest(Vector2 boulderVelocity){
        //itemIndex = generate random num 0 size of items +1
        int itemIndex = Random.Range(0, items.Length);

        //instantiate random num
        GameObject item = Instantiate(items[itemIndex],transform.position,Quaternion.identity);
        //apply 1/2 boulder force to item so that it moves when chest destroyed
        item.GetComponent<Rigidbody2D>().velocity = boulderVelocity / 2;
        item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 1f);

        // instantiate destroyed chest
        GameObject chestPieces = Instantiate(destroyedChest,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
