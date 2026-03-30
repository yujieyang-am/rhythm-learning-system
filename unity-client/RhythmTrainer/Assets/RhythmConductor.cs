using UnityEngine;

public class RhythmConductor : MonoBehaviour
{
    public float bpm = 60f;
    public double startDelay = 1.0;

    [HideInInspector] public double startDspTime;
    [HideInInspector] public double interval;

    private bool initialized = false;

    void Awake()
    {
        interval = 60.0 / bpm;
        startDspTime = AudioSettings.dspTime + startDelay;
        initialized = true;
    }

    public double GetBeatTime(int beatIndex)
    {
        return startDspTime + beatIndex * interval;
    }

    public int GetNearestBeatIndex(double dspTime)
    {
        return Mathf.RoundToInt((float)((dspTime - startDspTime) / interval));
    }

    public int GetCurrentBeatIndex(double dspTime)
    {
        return Mathf.FloorToInt((float)((dspTime - startDspTime) / interval));
    }

    public bool IsReady()
    {
        return initialized;
    }
}