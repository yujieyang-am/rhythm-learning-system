using UnityEngine;

public class PlayheadController : MonoBehaviour
{
    public RectTransform playhead;
    public float bpm = 60f;

    private double startTime;
    private double interval;
    private float[] beatPositions = { -300f, -100f, 100f, 300f };

    void Start()
    {
        interval = 60.0 / bpm;
        startTime = AudioSettings.dspTime + 1.0;
    }

    void Update()
    {
        double currentTime = AudioSettings.dspTime;
        double elapsed = currentTime - startTime;

        if (elapsed < 0) return;

        double beatProgress = elapsed / interval;
        int wholeBeat = Mathf.FloorToInt((float)beatProgress);
        int currentBeat = wholeBeat % 4;

        float t = (float)(beatProgress - Mathf.Floor((float)beatProgress));

        float x;

        if (currentBeat < 3)
        {
            x = Mathf.Lerp(beatPositions[currentBeat], beatPositions[currentBeat + 1], t);
        }
        else
        {
            x = beatPositions[3];
        }

        Vector2 pos = playhead.anchoredPosition;
        pos.x = x;
        playhead.anchoredPosition = pos;
    }
}