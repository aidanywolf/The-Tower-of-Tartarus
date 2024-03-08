using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float connectedSpeed = 0;
    [SerializeField] public float unconnectedSpeed = 0;
    [SerializeField] GameObject boulder;
    [SerializeField] private List<AnimationStateChanger> animationStateChangers;
    [SerializeField] public float invulnerabilityDuration = 1.5f;

    BoulderController boulderController;    
    HealthManager healthManager;
    bool isInvulnerable = false;
    float invulnerabilityTimer = 0f;
    float speed = 0;
    Rigidbody2D rb;

     void Awake(){
        rb = GetComponent<Rigidbody2D>();
        boulderController = boulder.GetComponent<BoulderController>();
        healthManager = GetComponent<HealthManager>();
    }

    public void MoveCreature(Vector3 direction)
    {
        //move player
        rb.velocity = (direction * speed);
    }

    void Update(){

        //gives player invulnerability for some time
        if(isInvulnerable){
            invulnerabilityTimer -= Time.deltaTime;

            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position);
        direction.z = 0;

        //angle from direction to 0,1,0 (0-180 degrees)
        float angle = Vector3.Angle(direction, Vector3.up);
        //cross product of direction and 0,1,0 (direction->right = cross.z is pos. direction->left = cross.z is negative)
        Vector3 cross = Vector3.Cross(direction, Vector3.up);
        float moving = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);

         //changes speed and anim based on if player is connected to boulder
        if(boulderController.connected == true){
            speed = connectedSpeed;
            playerAnimConnected(moving, angle, cross.z);
        }else{
            speed = unconnectedSpeed;
            playerAnimDisconnected(moving, angle, cross.z);
        }

    }

    public void LoseHealth(){
        //when lose health is called, player has to be vulnerable,
        //player is then given invulnerability for some time
        if (!isInvulnerable){
            healthManager.currentHealth -= 1;
            healthManager.UpdateHealthUI();
            isInvulnerable = true;
            invulnerabilityTimer = invulnerabilityDuration;
        }
    }

    void playerAnimConnected(float moving, float angle, float crossZ){
        //change animation based on which direction mouse is
        if(angle > 0 && angle < 45){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosUp", moving, speed);
            }
        }else if(angle > 135){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosDown", moving, speed);
            }
        }else if(angle > 45 && angle < 135 && crossZ < 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosLeft",moving, speed);
            }
        }else{
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosRight", moving, speed);
            }
        }
    }

    void playerAnimDisconnected(float moving, float angle, float crossZ){
        //change animation based on which direction mouse is
        if(angle > 0 && angle < 45){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosSoloUp", moving, speed);
            }
        }else if(angle > 135){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosDown", moving, speed);
            }
        }else if(angle > 45 && angle < 135 && crossZ < 0){
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosSoloLeft",moving, speed);
            }
        }else{
            foreach(AnimationStateChanger asc in animationStateChangers){
                asc.ChangeAnimationState("SisyphosSoloRight", moving, speed);
            }
        }
    }
}
