using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Player playerController;
    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] float offset = 1f; 
    [SerializeField] public bool connected = true;
    [SerializeField] float bounciness = 0.5f;
    [SerializeField] List<AnimationStateChanger> animationStateChangers;
    Vector3 zLayer = new Vector3(0f, 0f, 0f);
    Rigidbody2D rb, playerrb;
    [SerializeField] CircleCollider2D collider;
    [SerializeField] GameObject damagingCollider;
    float moving;
    float playerSpeed;
    float playerMoving;

    void Awake(){
        playerrb = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<Player>();
        playerInputHandler = player.GetComponentInChildren<PlayerInputHandler>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(!connected){
            if(other.CompareTag("Player"))
            {
                //on pick up, set tag to boulder so it doesnt damage
                damagingCollider.tag = "Boulder";
                connected = true;
                //collider turns off is trigger so that most enemies/ wall will be collidable when player is holding boulder
                collider.isTrigger = false;
                rb.velocity = new Vector3(0,0,0);
            }
            //if colliding with wall while not connected. boulder bounces off of wall
            else if(other.CompareTag("LengthWall")){
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y) * bounciness;
            }
            else if(other.CompareTag("WidthWall")){
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y) * bounciness;
            }
            //boulder breaks obstacles when not connected
            else if(other.CompareTag("Obstacle")){
                Obstacle obstacle = other.GetComponent<Obstacle>();
                obstacle.DestroyObstacle();
                rb.velocity *= 0.7f;
            }
            else if(other.CompareTag("Chest")){
                Chest chest = other.GetComponent<Chest>();
                chest.DestroyChest(rb.velocity);
            }
        }
    }

    void Update()
    {
        if(playerInputHandler.pauseActive == false){
            moving = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);

            if(moving > 1 && !connected){
                //set tag to damaging so it can damage enemies
                //turn on is trigger for main collider so that it can go through enemies
                damagingCollider.tag = "DamagingBoulder";
                collider.isTrigger = true;
            }
            else if(!connected){
                //if its not connected and not moving set tag to boulder so it cant damage
                //set is trigger to false so that most enemies can no longer walk through it
                damagingCollider.tag = "Boulder";
                collider.isTrigger = false;
            }


            //boulder must be connected for this transform code to run
            if(connected){
                Vector3 playerPosition = player.transform.position;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 boulderPosition = transform.position;
                mousePosition.z = 0;

                //direction to rotate boulder
                Vector3 direction = (mousePosition - playerPosition);

                //rotate in direction 
                Quaternion targetRotation = Quaternion.LookRotation(Camera.main.transform.forward, direction);
                transform.rotation = targetRotation;
            

                Vector3 desiredPosition = playerPosition + offset * direction.normalized; 
                transform.position = desiredPosition;

        
                //changes the boulders layer depending on where player is looking
                //if boulder is pointing up, boulder should be below player z
                //if boulder is pointing down, boulder should be above player z
                float verticalThreshold = -0.4f;
                if((boulderPosition.y - playerPosition.y) > verticalThreshold ){
                    zLayer = new Vector3(0f, 0f, 1f);
                }else if((boulderPosition.y - playerPosition.y) < verticalThreshold ){
                    zLayer = new Vector3(0f, 0f, -1f);
                }
                transform.position += zLayer;
                //animation function while connected
                ConnectedRollAnim();
            }else{
                //animation function while disconnected
                RollAnim();
            }
        }
    }

    // animate based on boulder move dir and speed
    void RollAnim(){
        if(rb.velocity.x == 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("BoulderRollRight", moving, moving);
            }
        }else if(rb.velocity.x > 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("BoulderRollRight", moving, moving);
            }
        }else if(rb.velocity.x < 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("BoulderRollLeft", moving, moving);
            }
        }
    }
    
    //animate based on player move dir and speed
    void ConnectedRollAnim(){
        playerMoving = Mathf.Abs(playerrb.velocity.x) + Mathf.Abs(playerrb.velocity.y);
        playerSpeed = playerController.connectedSpeed;
        if(playerrb.velocity.x == 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("BoulderRollRight", playerMoving, playerSpeed);
            }
        }else if(playerrb.velocity.x < 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("BoulderRollLeft", playerMoving, playerSpeed);
            }
        }else if(playerrb.velocity.x > 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("BoulderRollRight", playerMoving, playerSpeed);
            }
        }
    }
}
