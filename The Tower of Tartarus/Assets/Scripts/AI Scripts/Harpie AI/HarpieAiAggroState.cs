using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpieAIAggroState : HarpieAIState
{
    List<Vector2> path;
    public HarpieAIAggroState(HarpieAI harpieAI) : base(harpieAI){}

    public override void BeginState()
    {
        if(path == null){
            path = new List<Vector2>();
        }
        harpieAI.GetTargetMoveCommand(ref path);
    }
    public override void UpdateState()
    {

        //if we finished walking the path, or couldn't find one, start patrolling randomly
        if(path.Count == 0){
            harpieAI.ChangeState(harpieAI.patrolState);
            return;
        }

        //every second make new path to player
        if(timer > 1f){
            timer = 0;

            harpieAI.ChangeState(harpieAI.aggroState);
        }

        //draw lines in scene view to see where we're going
        Debug.DrawLine(harpieAI.myHarpie.transform.position,path[0]);
        for(int i = 0; i < path.Count-1; i++){
            Debug.DrawLine(path[i],path[i+1]);
        }

        harpieAI.myHarpie.MoveHarpieToward(path[0]); //move to the next stop on the path
        if(Vector3.Distance(harpieAI.myHarpie.transform.position,path[0]) < harpieAI.myHarpie.speed * Time.fixedDeltaTime){
            harpieAI.myHarpie.transform.position = path[0]; //teleport to path point so we don't overshoot
            path.RemoveAt(0); //remove element
        }
    }
}
