using UnityEngine;

public class RhythmConductor : MonoBehaviour
{
    public float bpm = 60f;
    public double startDelay = 0.8;   // 倒數結束後，再等 0.8 秒進第一拍

    [HideInInspector] public double startDspTime;
    [HideInInspector] public double interval;

    private bool initialized = false;

    void Awake()
    {
        if (PlayerPrefs.HasKey("SelectedBPM"))
        {
            bpm = PlayerPrefs.GetInt("SelectedBPM");
        }

        interval = 60.0 / bpm;
        Debug.Log("目前 BPM: " + bpm);
    }

    public void StartRhythm()
    {
        startDspTime = AudioSettings.dspTime + startDelay;
        initialized = true;

        Debug.Log("Rhythm will start at dspTime = " + startDspTime);
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