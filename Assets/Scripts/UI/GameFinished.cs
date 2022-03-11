using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class GameFinished : MonoBehaviour
{
    private CanvasGroup canvasGroupComponent;
    private Image imageComponent;

    [SerializeField]
    private float startAfter = 2f;

    [SerializeField]
    private float fadeInDuration = 1f;

    [SerializeField]
    private float maxBlur = 10f;

    private void Awake() {
        canvasGroupComponent = GetComponent<CanvasGroup>();
        imageComponent = GetComponent<Image>();
    }

    public void fadeIn()
    {
        StartCoroutine(fadeInCoroutine());
    }

    private void setOpacity(float opacity)
    {
        canvasGroupComponent.alpha = opacity;
        imageComponent.material.SetFloat("_Radius", Mathf.Lerp(0f, maxBlur, opacity));
        // gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Radius", Mathf.Lerp(0f, maxBlur, opacity));
    }

    public IEnumerator fadeInCoroutine()
    {
        yield return new WaitForSeconds(startAfter);

        float startTime = Time.time;
        while(Time.time - startTime < fadeInDuration )
        {
            setOpacity((Time.time - startTime) / fadeInDuration);
            yield return null;
        }
    }
}
