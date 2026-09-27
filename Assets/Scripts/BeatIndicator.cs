using UnityEngine;
using TMPro;

public class BeatIndicator : MonoBehaviour
{
    private TextMeshProUGUI beatText;

    public float displayTime = 0.15f;

    void Start()
    {
        beatText = GetComponent<TextMeshProUGUI>();

        beatText.enabled = false;

        BeatManager.Instance.OnBeat += ShowBeat;
    }

    void ShowBeat()
    {
        beatText.enabled = true;

        CancelInvoke(nameof(HideBeat));
        Invoke(nameof(HideBeat), displayTime);
    }

    void HideBeat()
    {
        beatText.enabled = false;
    }

    void OnDestroy()
    {
        if (BeatManager.Instance != null)
        {
            BeatManager.Instance.OnBeat -= ShowBeat;
        }
    }
}