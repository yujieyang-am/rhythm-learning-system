using UnityEngine;

public class Metronome : MonoBehaviour
{
    public RhythmConductor conductor;
    public AudioSource audioSource;

    private int nextBeatIndex = 0;

    void Update()
    {
        if (conductor == null || !conductor.IsReady()) return;

        double currentTime = AudioSettings.dspTime;
        double nextTickTime = conductor.GetBeatTime(nextBeatIndex);

        if (currentTime >= nextTickTime)
        {
            audioSource.Play();
            nextBeatIndex++;
        }
    }
}