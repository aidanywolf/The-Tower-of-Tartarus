using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAIPatrolState : GhostAIState
{
    List<Vector2> path;
    public GhostAIPatrolState(GhostAI ghostAI) : base(ghostAI){}

    public override void BeginState()
    {
        path = new List<Vector2>();
    
        ghostAI.GetRandomMoveCommand(ref path);
    }
    public override void UpdateState()
    {

        //if we finished walking the path, or couldn't find one, start patrolling randomly
        if(path.Count == 0){
            ghostAI.myGhost.Stop();
            ghostAI.ChangeState(ghostAI.patrolState);
            return;
        }

        //draw lines in scene view to see where we're going
        Debug.DrawLine(ghostAI.myGhost.transform.position,path[0]);
        for(int i = 0; i < path.Count-1; i++){
            Debug.DrawLine(path[i],path[i+1]);
        }

        //if we see the target, make new path
        if(ghostAI.GetTarget() != null && timer > 1f){
            ghostAI.myGhost.Stop();
            ghostAI.ChangeState(ghostAI.aggroState);
            return;
        }

        ghostAI.myGhost.MoveGhostToward(path[0]); //move to the next stop on the path
        if(Vector3.Distance(ghostAI.myGhost.transform.position,path[0]) < ghostAI.myGhost.speed * Time.fixedDeltaTime){
            ghostAI.myGhost.transform.position = path[0]; //teleport to path point so we don't overshoot
            path.RemoveAt(0); //remove element
        }
    }
}
