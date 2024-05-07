using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{

   [SerializeField] private Image fadeImage;
   [SerializeField] private Color fadeColor = Color.black;
   [SerializeField] private float fadeTime = 1;
   [SerializeField] private bool fadeInOnStart = true;

    void Start(){
        if(fadeInOnStart){
            FadeToClear();
        }
    }

    public void FadeToClear(){
        fadeImage.color = fadeColor;
        StartCoroutine(FadeToClearRoutine());
    }
    IEnumerator FadeToClearRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                fadeImage.color = new Color(fadeColor.r,fadeColor.g,fadeColor.b, 1f - (timer/fadeTime));
            }
            fadeImage.color = Color.clear;
    }

    public void FadeToColor(){
        fadeImage.color = new Color(fadeColor.r,fadeColor.g,fadeColor.b,0);
        StartCoroutine(FadeToColorRoutine());
    }
    IEnumerator FadeToColorRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                fadeImage.color = new Color(fadeColor.r,fadeColor.g,fadeColor.b, (timer/fadeTime));

            }
            fadeImage.color = fadeColor;
    }
}
