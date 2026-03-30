using System.Collections.Generic;
using UnityEngine;

public class PracticeStats : MonoBehaviour
{
    private List<double> offsets = new List<double>();

    public int perfectCount { get; private set; }
    public int earlyCount   { get; private set; }
    public int lateCount    { get; private set; }
    public int missCount    { get; private set; }

    public void ResetStats()
    {
        offsets.Clear();
        perfectCount = 0;
        earlyCount = 0;
        lateCount = 0;
        missCount = 0;
    }

    public void RecordJudge(string result, double delta)
    {
        switch (result)
        {
            case "Perfect":
                perfectCount++;
                offsets.Add(delta);
                break;
            case "Early":
                earlyCount++;
                offsets.Add(delta);
                break;
            case "Late":
                lateCount++;
                offsets.Add(delta);
                break;
            case "Miss":
                missCount++;
                break;
        }
    }

    public int GetTotal()
    {
        return perfectCount + earlyCount + lateCount + missCount;
    }

    public float GetAccuracy()
    {
        int total = GetTotal();
        if (total == 0) return 0f;
        return (float)perfectCount / total * 100f;
    }

    public double GetMeanOffset()
    {
        if (offsets.Count == 0) return 0;

        double sum = 0;
        foreach (double d in offsets) sum += d;
        return sum / offsets.Count;
    }

    public double GetStdOffset()
    {
        if (offsets.Count == 0) return 0;

        double mean = GetMeanOffset();
        double sum = 0;

        foreach (double d in offsets)
        {
            double diff = d - mean;
            sum += diff * diff;
        }

        return System.Math.Sqrt(sum / offsets.Count);
    }

    public string GetTendency()
    {
        double mean = GetMeanOffset();

        if (mean < -0.02) return "Rushing";
        if (mean > 0.02) return "Dragging";
        return "Stable";
    }

    public string GetSummaryText()
    {
        return
            $"Perfect: {perfectCount}\n" +
            $"Early: {earlyCount}\n" +
            $"Late: {lateCount}\n" +
            $"Miss: {missCount}\n" +
            $"Accuracy: {GetAccuracy():F1}%\n" +
            $"Mean Offset: {GetMeanOffset():+0.000;-0.000;0.000}s\n" +
            $"STD: {GetStdOffset():0.000}s\n" +
            $"Tendency: {GetTendency()}";
    }
}