using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stage_data", menuName = "Rhythm/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageId;
    public string title;
    public int chapter;
    public string region;
    public int bpm;
    public int timeSignatureTop;
    public int timeSignatureBottom;
    public int bars;
    public List<NoteData> notes;
}