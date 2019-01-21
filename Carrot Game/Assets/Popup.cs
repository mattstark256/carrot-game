using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private Text continueText;

    private float lockedDuration = 1.5f;
    private float fadeDuration = 1f;
    private float timer = 0;

    private bool canBeClosed = false;
    public bool CanBeClosed() { return canBeClosed; }

    // Start is called before the first frame update
    void Start()
    {
        continueText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        if (timer > lockedDuration)
        {
            continueText.enabled = true;
            canBeClosed = true;
            StartCoroutine(FadeInText());
        }
    }

    IEnumerator FadeInText()
    {
        float f = 0;
        while (f < 1)
        {
            f += Time.unscaledDeltaTime / fadeDuration;
            f = Mathf.Clamp01(f);
            float smoothedF = Mathf.SmoothStep(0, 1, f);
            continueText.color = new Color(1, 1, 1, smoothedF);

            yield return null;
        }
    }
}
