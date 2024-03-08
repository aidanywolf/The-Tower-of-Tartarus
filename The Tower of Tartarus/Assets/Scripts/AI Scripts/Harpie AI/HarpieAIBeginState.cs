using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpieAIBeginState : HarpieAIState
{

    //this state is included so that the states can reset their own state. idk why it wouldnt work without this
    public HarpieAIBeginState(HarpieAI harpieAI) : base(harpieAI){}

    public override void BeginState()
    {
        UpdateState();
    }
    Vector3 moveVec;
    public override void UpdateState()
    {
        harpieAI.ChangeState(harpieAI.patrolState);
    }
}
