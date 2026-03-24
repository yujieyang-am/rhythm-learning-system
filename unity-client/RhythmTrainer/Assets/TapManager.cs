using TMPro;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    public float bpm = 60f;

    private double startTime;
    private double interval;
    public TextMeshProUGUI judgementText;
    public float judgementDisplayTime = 0.5f;
    private float judgementTimer = 0f;

    void Start()
    {
        interval = 60.0 / bpm;
        startTime = AudioSettings.dspTime + 1.0;
    }

    void Update()
    {
        if (judgementTimer > 0)
        {
            judgementTimer -= Time.deltaTime;

            if (judgementTimer <= 0)
            {
                judgementText.text = "";
            }
        }

        double currentTime = AudioSettings.dspTime;
        double beatTime = GetNearestBeatTime(currentTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            double tapTime = AudioSettings.dspTime;
            double delta = tapTime - beatTime;

            string result;

            double absDelta = Mathf.Abs((float)delta);

            if (absDelta < 0.02)
            {
                result = "Perfect";
                judgementText.color = Color.green;
            }
            else if (absDelta < 0.06)
            {
                if (delta < 0)
                {
                    result = "Early";
                    judgementText.color = Color.yellow;
                }
                else
                {
                    result = "Late";
                    judgementText.color = new Color(1f, 0.5f, 0f); // 橘色
                }
            }
            else
            {
                result = "Miss";
                judgementText.color = Color.red;
            }

            string deltaText = delta >= 0 
                ? "(+" + delta.ToString("F3") + "s)" 
                : "(" + delta.ToString("F3") + "s)";

            judgementText.text = result + " " + deltaText;
            judgementTimer = judgementDisplayTime;

            Debug.Log(result + " | Δt: " + delta);
        }
    }

    double GetNearestBeatTime(double currentTime)
    {
        int beatIndex = Mathf.RoundToInt((float)((currentTime - startTime) / interval));
        return startTime + beatIndex * interval;
    }
}