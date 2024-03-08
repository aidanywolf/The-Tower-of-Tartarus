using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostAIState
{

    protected GhostAI ghostAI;
    protected float timer = 0;
    public GhostAIState(GhostAI newAI){
        ghostAI = newAI;
    }

    public void UpdateStateBase(){
        timer+=Time.fixedDeltaTime;
        UpdateState();
    }

    public void BeginStateBase(){
        timer = 0;
        BeginState();
    }


    //When we start the state up
    public abstract void UpdateState();
    //Tick Method
    public abstract void BeginState();
}
