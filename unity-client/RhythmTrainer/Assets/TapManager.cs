using TMPro;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    public float bpm = 60f;

    private double startTime;
    private double interval;
    public TextMeshProUGUI judgementText;

    void Start()
    {
        interval = 60.0 / bpm;
        startTime = AudioSettings.dspTime + 1.0;
    }

    void Update()
    {
        double currentTime = AudioSettings.dspTime;
        double beatTime = GetNearestBeatTime(currentTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            double tapTime = AudioSettings.dspTime;
            double delta = tapTime - beatTime;

            string result;

            double absDelta = Mathf.Abs((float)delta);

            if (absDelta < 0.02)
                result = "Perfect";
            else if (absDelta < 0.06)
                result = delta < 0 ? "Early" : "Late";
            else
                result = "Miss";

            judgementText.text = result;

            Debug.Log(result + " | Δt: " + delta);
        }
    }

    double GetNearestBeatTime(double currentTime)
    {
        int beatIndex = Mathf.RoundToInt((float)((currentTime - startTime) / interval));
        return startTime + beatIndex * interval;
    }
}