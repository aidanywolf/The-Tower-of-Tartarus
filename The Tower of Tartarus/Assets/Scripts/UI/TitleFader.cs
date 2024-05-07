using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleFader : MonoBehaviour
{
    [SerializeField] private Image title;
    [SerializeField] private Image quit;
    [SerializeField] private Color[] fadeColors;
    [SerializeField] private float fadeTime = 1;
    [SerializeField] private bool fadeInOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if(fadeInOnStart){
            FadeToColor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
        {
            FadeToClear();
        }
    }
    void FadeToClear(){
        StartCoroutine(FadeToClearRoutine());
        IEnumerator FadeToClearRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                title.color = new Color(title.color.r,title.color.g,title.color.b, 1f - (timer/fadeTime));
                quit.color = new Color(quit.color.r,quit.color.g,quit.color.b, 1f - (timer/fadeTime));
            }
            title.color = Color.clear;
            quit.color = Color.clear;
            this.gameObject.SetActive(false);
        }
    }

    void FadeToColor(){
        title.color = Color.clear;
        quit.color = Color.clear;
        StartCoroutine(FadeToColorRoutine());
        IEnumerator FadeToColorRoutine(){
            float timer = 0;
            while(timer < fadeTime){
                yield return null;
                timer+=Time.deltaTime;
                title.color = new Color(fadeColors[0].r,fadeColors[0].g,fadeColors[0].b, (timer/fadeTime));
                quit.color = new Color(fadeColors[2].r,fadeColors[2].g,fadeColors[2].b, (timer/fadeTime));

            }
            title.color = fadeColors[0];
            quit.color = fadeColors[2];
        }
    }
}
