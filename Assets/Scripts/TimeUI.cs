using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    private TextMeshProUGUI timeText;


    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        UpdateTime();
    }

    private void OnEnable()
    {
        TimeManager.onMinuteChanged += UpdateTime;
        TimeManager.onHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.onMinuteChanged -= UpdateTime;
        TimeManager.onHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {   if(TimeManager.Minute % 5 == 0)
        {
            timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
        }
    }
}
