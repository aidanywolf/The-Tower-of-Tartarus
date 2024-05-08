using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [Header("Config")]
    public int seed = 0;
    public int roomCount = 10;
    public List<GameObject> genRooms;

    public List<GameObject> placedRooms;
    GameObject exit;
    GameObject newRoom;

    void Start(){
        NewFloor();
    }

    public void NewFloor(){
        Generate();
    }
    public void Reset(){
        
        foreach(GameObject g in placedRooms){
            Destroy(g);
        }
        Debug.Log("LevelDestroyed");
    }

    void Generate(){
        Random.InitState(System.DateTime.Now.Millisecond);

        placedRooms = new List<GameObject>();


        //place starter room
        GameObject starter = genRooms[0];

        GameObject starterRoom = Instantiate(starter, new Vector3(0, 0, 0), Quaternion.identity);
        starterRoom.gameObject.name = "Starter";
        Transform exitTransform = starterRoom.transform.Find("Exit");
        placedRooms.Add(starterRoom);
        
        StartCoroutine(GenerateSlicedRoutine());
        IEnumerator GenerateSlicedRoutine(){
            for(int i = 2; i < roomCount; i++){

                newRoom = Instantiate(genRooms[Random.Range(2,genRooms.Count)], exitTransform.position, Quaternion.identity);
                newRoom.gameObject.name = "Room " + i;
                exitTransform = newRoom.transform.Find("Exit");
                placedRooms.Add(newRoom);
                yield return null;
            }
            GameObject ender = genRooms[1];
            GameObject endingRoom = Instantiate(ender, exitTransform.position, Quaternion.identity);
            endingRoom.gameObject.name = "Ender";
            placedRooms.Add(endingRoom);
        }
    }
}
