using UnityEngine;

public class PlayheadController : MonoBehaviour
{
    public RectTransform playhead;
    public float bpm = 60f;

    public float beat1X = -300f;
    public float beat2X = -100f;
    public float beat3X = 100f;
    public float beat4X = 300f;

    private double startTime;
    private double interval;

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

        int beatIndex = Mathf.FloorToInt((float)(elapsed / interval)) % 4;

        float x = beat1X;

        switch (beatIndex)
        {
            case 0:
                x = beat1X;
                break;
            case 1:
                x = beat2X;
                break;
            case 2:
                x = beat3X;
                break;
            case 3:
                x = beat4X;
                break;
        }

        Vector2 pos = playhead.anchoredPosition;
        pos.x = x;
        playhead.anchoredPosition = pos;
    }
}