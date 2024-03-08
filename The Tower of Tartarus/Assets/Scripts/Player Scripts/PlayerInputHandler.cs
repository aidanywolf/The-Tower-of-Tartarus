using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Player player;
    BoulderRoller boulderRoller;

    void Start()
    {
        boulderRoller = player.GetComponent<BoulderRoller>();
    }

    void Update()
    {
        //get player input
        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            input.x += -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.y += -1;
        }

        //if m1 is pressed, call boulder roll
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            boulderRoller.Roll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        //send input to movecreature
        player.MoveCreature(input);


    }
}

