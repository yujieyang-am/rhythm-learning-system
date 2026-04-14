using UnityEngine;
using System.Collections.Generic;

public class ChartRenderer : MonoBehaviour
{
    [Header("Stage Data")]
    public StageData stageData;

    [Header("References")]
    public RectTransform noteContainer;
    public GameObject quarterNotePrefab;
    public GameObject eighthPairPrefab;

    [Header("Measure Layout")]
    public float[] beatXPositions = new float[4] { -300f, -100f, 100f, 300f };
    public float yPosition = -140f;

    [Header("Debug")]
    public int currentMeasure = 0;

    private void Start()
    {
        RenderMeasure(currentMeasure);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextMeasure();
        }
    }

    public void RenderMeasure(int measureIndex)
    {
        if (stageData == null)
        {
            Debug.LogError("StageData 沒有指定");
            return;
        }

        foreach (Transform child in noteContainer)
        {
            Destroy(child.gameObject);
        }

        int beatsPerBar = stageData.timeSignatureTop;
        float measureStartBeat = measureIndex * beatsPerBar;
        float measureEndBeat = measureStartBeat + beatsPerBar;

        List<NoteData> notesInMeasure = new List<NoteData>();

        foreach (NoteData note in stageData.notes)
        {
            if (note.beat >= measureStartBeat && note.beat < measureEndBeat)
            {
                notesInMeasure.Add(note);
            }
        }

        foreach (NoteData note in notesInMeasure)
        {
            GameObject prefabToSpawn = null;

            if (note.type == "quarter")
            {
                prefabToSpawn = quarterNotePrefab;
            }
            else if (note.type == "eighth_pair")
            {
                prefabToSpawn = eighthPairPrefab;
            }

            if (prefabToSpawn == null)
            {
                Debug.LogWarning("找不到對應 prefab: " + note.type);
                continue;
            }

            int localBeatIndex = Mathf.RoundToInt(note.beat - measureStartBeat);

            if (localBeatIndex < 0 || localBeatIndex >= beatXPositions.Length)
            {
                Debug.LogWarning("拍點超出範圍: " + note.beat);
                continue;
            }

            float x = beatXPositions[localBeatIndex];

            GameObject newNote = Instantiate(prefabToSpawn, noteContainer);
            RectTransform rect = newNote.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(x, yPosition);
        }
    }

    public void NextMeasure()
    {
        currentMeasure++;

        if (currentMeasure >= stageData.bars)
        {
            currentMeasure = 0;
        }

        RenderMeasure(currentMeasure);
    }
}