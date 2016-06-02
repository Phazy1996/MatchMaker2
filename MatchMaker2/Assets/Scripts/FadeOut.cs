using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    [SerializeField]
    private float fadeSpeed = 0.01f;
    [SerializeField]
    private bool looping = false;
    [SerializeField] private SpriteRenderer fadeImage;
    private Color temp;

    void Start()
    {
        if(looping)
        {
            StartFade();
        }
    }
    public void StartFade()
    {
        StartCoroutine(FadeIn());
    }
    public void EndFade()
    {
        StartCoroutine(FadeOver());
    }
    IEnumerator FadeIn()
    {
        while (fadeImage.color.a < 1)
        {
            temp = fadeImage.color;
            temp.a += fadeSpeed;
            fadeImage.color = temp;
            yield return new WaitForFixedUpdate();
        }
        if(looping)
        {
            EndFade();
        }
    }
    IEnumerator FadeOver()
    {
        while (fadeImage.color.a > 0)
        {
            temp = fadeImage.color;
            temp.a -= fadeSpeed;
            fadeImage.color = temp;
            yield return new WaitForFixedUpdate();
        }
        if(looping)
        {
            StartCoroutine(FadeIn());
        }
    }
}
