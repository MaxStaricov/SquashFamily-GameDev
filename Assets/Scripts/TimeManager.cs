using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action onSecondChanged;
    public static Action onDayChanged;
    public static Action onMinuteChanged;
    public static Action onHourChanged;


    public static int Second;
    public static int Day;
    public static int Minute;
    public static int Hour;
    private float secondToRealSecond = 1f;
    [SerializeField] private static float timeScaleMultiplier = 100f;
    private float timer;

    public static void setMultiply(float k)
    {
        timeScaleMultiplier = k;
    }

    private void Awake()
    {
        Second = 21600;
        Minute = 0;
        Hour = 6;
        timer = secondToRealSecond;
    }

    private void Update()
    {

        timer -= Time.deltaTime * timeScaleMultiplier;

        while (timer <= 0)
        {
            Second++;
            onSecondChanged?.Invoke();

            if (Second % 60 == 0)
            {
                Minute++;
                onMinuteChanged?.Invoke();

                if (Minute >= 59)
                {
                    Hour++;
                    onHourChanged?.Invoke();
                    Minute = 0;

                    if (Hour >= 23)
                    {
                        Second = 0;
                        Hour = 0;
                        Day++;
                        onDayChanged?.Invoke();
                    }
                }
            }

            timer += secondToRealSecond;
        }
    }

    public static void ResetTime()
    {
        Second = 21600;
        Minute = 0;
        Hour = 6;
        Day = 0;
        timeScaleMultiplier = 100f;

        onSecondChanged = null;
        onDayChanged = null;
        onMinuteChanged = null;
        onHourChanged = null;
    }
}
