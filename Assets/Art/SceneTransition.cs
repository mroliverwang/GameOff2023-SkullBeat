using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image transitionImage;
    public float transitionTime = 1.0f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0;
        while (timer <= transitionTime)
        {
            float alpha = Mathf.Lerp(1, 0, timer / transitionTime);
            transitionImage.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(transitionImage.gameObject);
    }
}
