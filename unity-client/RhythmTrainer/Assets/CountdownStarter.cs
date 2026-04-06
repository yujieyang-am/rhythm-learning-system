using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownStarter : MonoBehaviour
{
    public RhythmConductor conductor;
    public TextMeshProUGUI countdownText;

    public Vector3 normalScale = Vector3.one;
    public Vector3 popScale = new Vector3(1.5f, 1.5f, 1f);

    void Start()
    {
        StartCoroutine(BeginCountdown());
    }

    IEnumerator BeginCountdown()
    {
        countdownText.gameObject.SetActive(true);

        yield return StartCoroutine(ShowCountdownStep("3", 1f));
        yield return StartCoroutine(ShowCountdownStep("2", 1f));
        yield return StartCoroutine(ShowCountdownStep("1", 1f));

        countdownText.text = "Go!";
        countdownText.transform.localScale = popScale;
        countdownText.alpha = 1f;

        conductor.StartRhythm();

        float t = 0f;
        float duration = 0.6f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            countdownText.transform.localScale = Vector3.Lerp(popScale, normalScale, progress);
            countdownText.alpha = Mathf.Lerp(1f, 0f, progress);

            yield return null;
        }

        countdownText.gameObject.SetActive(false);
    }

    IEnumerator ShowCountdownStep(string text, float duration)
    {
        countdownText.text = text;
        countdownText.transform.localScale = popScale;
        countdownText.alpha = 1f;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            countdownText.transform.localScale = Vector3.Lerp(popScale, normalScale, progress);

            if (progress > 0.7f)
            {
                float fadeProgress = (progress - 0.7f) / 0.3f;
                countdownText.alpha = Mathf.Lerp(1f, 0f, fadeProgress);
            }

            yield return null;
        }
    }
}