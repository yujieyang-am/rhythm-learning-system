using UnityEngine;
using UnityEngine.UI;

public class BeatVisual : MonoBehaviour
{
    private Image img;
    private float timer = 0f;

    void Awake()
    {
        img = GetComponentInChildren<Image>();

        if (img == null)
        {
            Debug.LogError(gameObject.name + " 沒有找到 Image 元件！");
        }
        else
        {
            Debug.Log(gameObject.name + " 已抓到 Image: " + img.gameObject.name);
        }
    }

    void Update()
    {
        if (img == null) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                img.color = Color.white;
            }
        }
    }

    public void ShowPerfect()
    {
        if (img == null) return;
        Debug.Log(gameObject.name + " ShowPerfect");
        img.color = Color.green;
        timer = 0.3f;
    }

    public void ShowEarly()
    {
        if (img == null) return;
        Debug.Log(gameObject.name + " ShowEarly");
        img.color = Color.yellow;
        timer = 0.3f;
    }

    public void ShowLate()
    {
        if (img == null) return;
        Debug.Log(gameObject.name + " ShowLate");
        img.color = new Color(1f, 0.5f, 0f);
        timer = 0.3f;
    }

    public void ShowMiss()
    {
        if (img == null) return;
        Debug.Log(gameObject.name + " ShowMiss");
        img.color = Color.red;
        timer = 0.3f;
    }
}