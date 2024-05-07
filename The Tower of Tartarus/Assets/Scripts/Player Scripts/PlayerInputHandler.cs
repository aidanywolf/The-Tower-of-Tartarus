using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField] Player player;
    BoulderRoller boulderRoller;
    public bool pauseActive = false;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject beginMenuUI;
    [SerializeField] GameObject deathMenuUI;


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
        if(Input.GetKeyDown(KeyCode.Mouse0) && pauseMenuUI.activeSelf == false){
            boulderRoller.Roll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }


        if (Input.GetKeyDown(KeyCode.Escape) && beginMenuUI.activeSelf == false && deathMenuUI.activeSelf == false)
        {
            if(pauseActive == false){
                Time.timeScale = 0;
                pauseActive = true;
                pauseMenuUI.SetActive(true);
            }
            else{
                Time.timeScale = 1;
                pauseActive = false;
                pauseMenuUI.SetActive(false);
            }
        }

        //send input to movecreature
        player.MovePlayer(input);


    }
}

