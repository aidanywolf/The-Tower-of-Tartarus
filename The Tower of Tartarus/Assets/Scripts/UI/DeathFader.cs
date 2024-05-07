using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathFader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI death;
    [SerializeField] private Image restart;
    [SerializeField] private Image screen;
    [SerializeField] private Image quit;
    [SerializeField] private Color[] fadeColors;
    [SerializeField] private float fadeTime = 1;

    public void FadeToClear(){
        StartCoroutine(FadeToClearRoutine());
        IEnumerator FadeToClearRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                death.color = new Color(death.color.r,death.color.g,death.color.b, 1f - (timer/fadeTime));
                restart.color = new Color(restart.color.r,restart.color.g,restart.color.b, 1f - (timer/fadeTime));
                quit.color = new Color(quit.color.r,quit.color.g,quit.color.b, 1f - (timer/fadeTime));
            }
            death.color = Color.clear;
            restart.color = Color.clear;
            quit.color = Color.clear;
        }
    }

    public void FadeToColor(){
        this.gameObject.SetActive(true);
        death.color = Color.clear;
        restart.color = Color.clear;
        quit.color = Color.clear;
        StartCoroutine(FadeToColorRoutine());
        IEnumerator FadeToColorRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                death.color = new Color(fadeColors[0].r,fadeColors[0].g,fadeColors[0].b, (timer/fadeTime));
                restart.color = new Color(fadeColors[2].r,fadeColors[2].g,fadeColors[2].b, (timer/fadeTime));
                screen.color = new Color(fadeColors[3].r,fadeColors[3].g,fadeColors[3].b, (timer/fadeTime));
                quit.color = new Color(fadeColors[4].r,fadeColors[4].g,fadeColors[4].b, (timer/fadeTime));

            }
            death.color = fadeColors[0];
            restart.color = fadeColors[2];
            screen.color = fadeColors[3];
            quit.color = fadeColors[4];
        }
    }
}