using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAIBeginState : GhostAIState
{

    //this state is included so that the states can reset their own state. idk why it wouldnt work without this
    public GhostAIBeginState(GhostAI ghostAI) : base(ghostAI){}

    public override void BeginState()
    {
        UpdateState();
    }
    Vector3 moveVec;
    public override void UpdateState()
    {
        ghostAI.ChangeState(ghostAI.patrolState);
    }
}
