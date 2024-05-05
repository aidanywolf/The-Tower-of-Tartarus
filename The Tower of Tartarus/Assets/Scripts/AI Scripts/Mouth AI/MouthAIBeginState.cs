using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthAIBeginState : MouthAIState
{

    //this state is included so that the states can reset their own state. idk why it wouldnt work without this
    public MouthAIBeginState(MouthAI mouthAI) : base(mouthAI){}

    public override void BeginState()
    {
        UpdateState();
    }
    Vector3 moveVec;
    public override void UpdateState()
    {
        mouthAI.ChangeState(mouthAI.patrolState);
    }
}
