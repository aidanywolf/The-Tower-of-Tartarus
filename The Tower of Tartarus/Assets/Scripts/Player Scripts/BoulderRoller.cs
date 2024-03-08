using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRoller : MonoBehaviour
{
    BoulderController boulderController;
    [SerializeField] GameObject boulder;
    [SerializeField] public float force = 5;
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float checkRadius;
    bool inWall = false;


    void Start()
    {
        boulderController = boulder.GetComponent<BoulderController>();
    }
    public void Roll(Vector3 targetPos){
        //boulder must be connected to roll
        //check if inwall using overlap circle all
        Vector2 checkPosition = boulder.transform.position;

        // Check for colliders within the specified circle
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPosition, checkRadius, wallLayer);

        // Check if there are colliders (i.e., if the object is in a wall)
        if (colliders.Length > 0)
        {
            // Object is in a wall
            inWall = true;
        }
        else
        {
            // Object is not in a wall
            inWall = false;
        }

        if(boulderController.connected && !inWall){
            boulderController.connected = false;
            
            //Calculate the force direction based on player's movement dir and target direction
            targetPos.z = 0;
            Vector3 forceDirection = ((targetPos - transform.position).normalized)+ ((new Vector3(rbPlayer.velocity.x, rbPlayer.velocity.y, 0f).normalized)/2);

            // Apply force to the boulder
            Rigidbody2D boulderRb = boulder.GetComponent<Rigidbody2D>();
            boulderRb.AddForce(forceDirection * force, ForceMode2D.Impulse);
        }
    }
}
