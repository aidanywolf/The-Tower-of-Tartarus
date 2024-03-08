using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAIAggroState : GhostAIState
{
    List<Vector2> path;
    public GhostAIAggroState(GhostAI ghostAI) : base(ghostAI){}

    public override void BeginState()
    {
        if(path == null){
            path = new List<Vector2>();
        }
        ghostAI.GetTargetMoveCommand(ref path);
        ghostAI.myGhost.aggroed = true;
    }
    public override void UpdateState()
    {
        //if we finished walking the path, or couldn't find one, start patrolling randomly
        if(path.Count == 0){
            ghostAI.ChangeState(ghostAI.patrolState);
            return;
        }

        //every second make new path to player
        if(timer > 1f){
            timer = 0;

            ghostAI.ChangeState(ghostAI.aggroState);
        }

        //draw lines in scene view to see where we're going
        Debug.DrawLine(ghostAI.myGhost.transform.position,path[0]);
        for(int i = 0; i < path.Count-1; i++){
            Debug.DrawLine(path[i],path[i+1]);
        }

        ghostAI.myGhost.MoveGhostToward(path[0]); //move to the next stop on the path
        if(Vector3.Distance(ghostAI.myGhost.transform.position,path[0]) < ghostAI.myGhost.speed * Time.fixedDeltaTime){
            ghostAI.myGhost.transform.position = path[0]; //teleport to path point so we don't overshoot
            path.RemoveAt(0); //remove element
        }
    }
}
