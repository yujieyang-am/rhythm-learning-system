using UnityEngine;

public class PlayheadController : MonoBehaviour
{
    public RhythmConductor conductor;
    public RectTransform playhead;

    public float beat1X = -300f;
    public float beat2X = -100f;
    public float beat3X = 100f;
    public float beat4X = 300f;

    void Update()
    {
        if (conductor == null || !conductor.IsReady()) return;

        double currentTime = AudioSettings.dspTime;
        double elapsed = currentTime - conductor.startDspTime;

        if (elapsed < 0) return;

        int beatIndex = conductor.GetCurrentBeatIndex(currentTime) % 4;
        if (beatIndex < 0) beatIndex += 4;

        float x = beat1X;

        switch (beatIndex)
        {
            case 0: x = beat1X; break;
            case 1: x = beat2X; break;
            case 2: x = beat3X; break;
            case 3: x = beat4X; break;
        }

        Vector2 pos = playhead.anchoredPosition;
        pos.x = x;
        playhead.anchoredPosition = pos;
    }
}