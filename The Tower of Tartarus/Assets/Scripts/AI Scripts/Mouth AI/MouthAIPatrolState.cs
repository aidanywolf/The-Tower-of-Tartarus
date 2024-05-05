using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthAIPatrolState : MouthAIState
{
    List<Vector2> path;
    public MouthAIPatrolState(MouthAI mouthAI) : base(mouthAI){}

    public override void BeginState()
    {
        path = new List<Vector2>();
    
        mouthAI.GetRandomMoveCommand(ref path);
    }
    public override void UpdateState()
    {

        if(mouthAI.GetTarget() == null){
            mouthAI.myMouth.aggroed = false;
        }else{
            mouthAI.myMouth.aggroed = true;
        }
        //if we finished walking the path, or couldn't find one, start patrolling randomly
        if(path.Count == 0){
            mouthAI.myMouth.Stop();
            mouthAI.ChangeState(mouthAI.patrolState);
            return;
        }

        //draw lines in scene view to see where we're going
        Debug.DrawLine(mouthAI.myMouth.transform.position,path[0]);
        for(int i = 0; i < path.Count-1; i++){
            Debug.DrawLine(path[i],path[i+1]);
        }

        //if we see the target, make new path
        if(mouthAI.GetTarget() != null && timer > 2f){
            mouthAI.myMouth.Stop();
            mouthAI.ChangeState(mouthAI.patrolState);
            return;
        }

        mouthAI.myMouth.MoveMouthToward(path[0]); //move to the next stop on the path
        if(Vector3.Distance(mouthAI.myMouth.transform.position,path[0]) < mouthAI.myMouth.speed * Time.fixedDeltaTime){
            mouthAI.myMouth.transform.position = path[0]; //teleport to path point so we don't overshoot
            path.RemoveAt(0); //remove element
        }
    }
}

