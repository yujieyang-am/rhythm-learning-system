using UnityEngine;

public class Metronome : MonoBehaviour
{
    public AudioSource audioSource;
    public float bpm = 60f;

    private double nextTickTime;
    private double interval;

    void Start()
    {
        interval = 60.0 / bpm;
        nextTickTime = AudioSettings.dspTime + 1.0; // 延遲1秒開始
    }

    void Update()
    {
        double currentTime = AudioSettings.dspTime;

        if (currentTime >= nextTickTime)
        {
            audioSource.Play();
            nextTickTime += interval;
        }
    }
}