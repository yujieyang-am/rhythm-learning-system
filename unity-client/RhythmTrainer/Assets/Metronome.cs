using UnityEngine;

public class Metronome : MonoBehaviour
{
    public RhythmConductor conductor;
    public AudioSource audioSource;

    private int nextBeatIndex = 0;
    private bool hasStarted = false;

    void OnEnable()
    {
        nextBeatIndex = 0;
        hasStarted = false;
    }

    void Update()
    {
        if (conductor == null || !conductor.IsReady()) return;

        double currentTime = AudioSettings.dspTime;

        if (!hasStarted)
        {
            if (currentTime < conductor.startDspTime) return;

            hasStarted = true;
            nextBeatIndex = 0;
        }

        double nextTickTime = conductor.GetBeatTime(nextBeatIndex);

        if (currentTime >= nextTickTime)
        {
            audioSource.Play();
            nextBeatIndex++;
        }
    }
}