using TMPro;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    public RhythmConductor conductor;
    public PracticeStats stats;

    [Header("UI")]
    public TextMeshProUGUI judgementText;
    public TextMeshProUGUI summaryText;

    public BeatVisual[] beats; // 4個Beat

    public float judgementDisplayTime = 0.5f;
    private float judgementTimer = 0f;

    void Start()
    {
        if (stats != null)
            stats.ResetStats();

        if (summaryText != null)
            summaryText.text = stats.GetSummaryText();
    }

    void Update()
    {
        if (conductor == null || !conductor.IsReady()) return;

        HandleJudgementTextTimer();
        HandleTapInput();
        HandleSummaryToggle();
        HandleReset();
    }

    void HandleJudgementTextTimer()
    {
        if (judgementTimer > 0)
        {
            judgementTimer -= Time.deltaTime;

            if (judgementTimer <= 0)
            {
                judgementText.text = "";
            }
        }
    }

    void HandleTapInput()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        double tapTime = AudioSettings.dspTime;
        double beatTime = GetNearestBeatTime(tapTime);
        double delta = tapTime - beatTime;

        int beatIndex = conductor.GetNearestBeatIndex(tapTime) % 4;
        if (beatIndex < 0) beatIndex += 4;

        Debug.Log("Tap detected, beatIndex = " + beatIndex);

        string result;
        double absDelta = Mathf.Abs((float)delta);

        if (absDelta < 0.02)
        {
            result = "Perfect";
            judgementText.color = Color.green;

            Debug.Log("Call ShowPerfect");
            if (beats != null && beats.Length > beatIndex && beats[beatIndex] != null)
                beats[beatIndex].ShowPerfect();
        }
        else if (absDelta < 0.06)
        {
            if (delta < 0)
            {
                result = "Early";
                judgementText.color = Color.yellow;

                Debug.Log("Call ShowEarly");
                if (beats != null && beats.Length > beatIndex && beats[beatIndex] != null)
                    beats[beatIndex].ShowEarly();
            }
            else
            {
                result = "Late";
                judgementText.color = new Color(1f, 0.5f, 0f);

                Debug.Log("Call ShowLate");
                if (beats != null && beats.Length > beatIndex && beats[beatIndex] != null)
                    beats[beatIndex].ShowLate();
            }
        }
        else
        {
            result = "Miss";
            judgementText.color = Color.red;

            Debug.Log("Call ShowMiss");
            if (beats != null && beats.Length > beatIndex && beats[beatIndex] != null)
                beats[beatIndex].ShowMiss();
        }

        string deltaText = delta >= 0
            ? "(+" + delta.ToString("F3") + "s)"
            : "(" + delta.ToString("F3") + "s)";

        judgementText.text = result + " " + deltaText;
        judgementTimer = judgementDisplayTime;

        if (stats != null)
        {
            stats.RecordJudge(result, delta);

            if (summaryText != null)
                summaryText.text = stats.GetSummaryText();
        }

        Debug.Log(result + " | Δt: " + delta);
    }

    void HandleSummaryToggle()
    {
        if (summaryText == null || stats == null) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool newState = !summaryText.gameObject.activeSelf;
            summaryText.gameObject.SetActive(newState);

            if (newState)
            {
                summaryText.text = stats.GetSummaryText();
            }
        }
    }

    void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (stats != null)
                stats.ResetStats();

            if (judgementText != null)
                judgementText.text = "";

            if (summaryText != null)
                summaryText.text = "";
        }
    }

    double GetNearestBeatTime(double currentTime)
    {
        int beatIndex = conductor.GetNearestBeatIndex(currentTime);
        return conductor.GetBeatTime(beatIndex);
    }
}