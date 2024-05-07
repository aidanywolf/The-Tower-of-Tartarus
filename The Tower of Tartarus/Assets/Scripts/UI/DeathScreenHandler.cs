using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class DeathScreenHandler : MonoBehaviour
{
    [SerializeField] DeathFader deathFader;
    public void Quit(){
        Application.Quit();
    }

    public void Restart(){
        StartCoroutine(Transition());
    }

    IEnumerator Transition(){
        deathFader.FadeToClear();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

    public void Options(){
        
    }
}
