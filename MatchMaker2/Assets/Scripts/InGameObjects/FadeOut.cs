using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {

    [SerializeField]
    private float fadeSpeed = 0.01f;
    [SerializeField]
    private bool looping = false;
    [SerializeField]
    private SpriteRenderer fadeSpriteRender;
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private Text fadeText;
    private Color temp;
    [SerializeField]
    private float maxAlpha = 1;
    [SerializeField]
    private float minAlpha = 0;
    void Start()
    {
        if (fadeSpriteRender != null)
            temp = fadeSpriteRender.color;
        if (fadeImage != null)
            temp = fadeImage.color;
        if (fadeText != null)
            temp = fadeText.color;
        if (looping)
        {
            StartFade();
        }
    }
    void Update()
    {
        if (fadeImage != null)
            fadeImage.color = temp;
        if (fadeSpriteRender != null)
            fadeSpriteRender.color = temp;
        if (fadeText != null)
            fadeText.color = temp;
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
        while (temp.a < maxAlpha)
        {
            temp.a += fadeSpeed;
            yield return new WaitForFixedUpdate();
        }
        if(looping)
        {
            EndFade();
        }
    }
    IEnumerator FadeOver()
    {
        while (temp.a > minAlpha)
        {
            temp.a -= fadeSpeed;
            yield return new WaitForFixedUpdate();
        }
        if(looping)
        {
            StartCoroutine(FadeIn());
        }
    }
}
