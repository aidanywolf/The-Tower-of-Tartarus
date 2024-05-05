using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] ScreenFader screenFader;
    [SerializeField] GameObject mainCamera;

    void Awake(){
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        screenFader = GameObject.Find("ScreenFader").GetComponent<ScreenFader>();
        mainCamera = GameObject.Find("Main Camera");
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.gameObject.tag == "Player"){
            Player player = other.gameObject.GetComponent<Player>();
            StartCoroutine(NewLevel(player));
        }
    }

    IEnumerator NewLevel(Player player){
        screenFader.FadeToColor();
        yield return new WaitForSeconds(1.5f);
        player.transform.position = new Vector3(0,0,0);
        mainCamera.transform.position = new Vector3(0,0,-10);
        levelGenerator.Reset();
        levelGenerator.NewFloor();

       screenFader.FadeToClear();
    }
    IEnumerator Clear(){
        yield return new WaitForSeconds(1);
        screenFader.FadeToClear();
    }
}
