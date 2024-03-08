using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HarpieAIState
{

    protected HarpieAI harpieAI;
    protected float timer = 0;
    public HarpieAIState(HarpieAI newAI){
        harpieAI = newAI;
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

