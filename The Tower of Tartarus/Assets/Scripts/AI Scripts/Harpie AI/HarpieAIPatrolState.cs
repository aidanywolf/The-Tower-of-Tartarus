using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpieAIPatrolState : HarpieAIState
{
    List<Vector2> path;
    public HarpieAIPatrolState(HarpieAI harpieAI) : base(harpieAI){}

    public override void BeginState()
    {
        path = new List<Vector2>();
    
        harpieAI.GetRandomMoveCommand(ref path);
    }
    public override void UpdateState()
    {

        //if we finished walking the path, or couldn't find one, start patrolling randomly
        if(path.Count == 0){
            harpieAI.myHarpie.Stop();
            harpieAI.ChangeState(harpieAI.patrolState);
            return;
        }

        //draw lines in scene view to see where we're going
        Debug.DrawLine(harpieAI.myHarpie.transform.position,path[0]);
        for(int i = 0; i < path.Count-1; i++){
            Debug.DrawLine(path[i],path[i+1]);
        }

        //if we see the target, make new path
        if(harpieAI.GetTarget() != null && timer > 1f){
            harpieAI.myHarpie.Stop();
            harpieAI.ChangeState(harpieAI.aggroState);
            return;
        }

        harpieAI.myHarpie.MoveHarpieToward(path[0]); //move to the next stop on the path
        if(Vector3.Distance(harpieAI.myHarpie.transform.position,path[0]) < harpieAI.myHarpie.speed * Time.fixedDeltaTime){
            harpieAI.myHarpie.transform.position = path[0]; //teleport to path point so we don't overshoot
            path.RemoveAt(0); //remove element
        }
    }
}

