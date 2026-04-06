using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public TMP_Dropdown bpmDropdown;

    private List<int> bpmOptions = new List<int> { 60, 70, 80, 90, 100, 110, 120 };

    void Start()
    {
        SetupDropdown();
    }

    void SetupDropdown()
    {
        bpmDropdown.ClearOptions();

        List<string> optionTexts = new List<string>();

        for (int i = 0; i < bpmOptions.Count; i++)
        {
            optionTexts.Add(bpmOptions[i].ToString());
        }

        bpmDropdown.AddOptions(optionTexts);

        // 預設選 60 BPM
        int defaultIndex = bpmOptions.IndexOf(60);
        if (defaultIndex >= 0)
        {
            bpmDropdown.value = defaultIndex;
        }

        bpmDropdown.RefreshShownValue();
    }

    public void StartGame()
    {
        int selectedBpm = bpmOptions[bpmDropdown.value];

        PlayerPrefs.SetInt("SelectedBPM", selectedBpm);
        PlayerPrefs.Save();

        Debug.Log("選擇的 BPM: " + selectedBpm);

        SceneManager.LoadScene("MainGame");
    }
}