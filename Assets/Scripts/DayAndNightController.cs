using UnityEngine;

public class DayAndNightController : MonoBehaviour
{
    [SerializeField] private Light Sun;
    private float currentAmbient = 1.0f;
    private Color baseAmbientColor = new Color(0.5f, 0.5f, 0.5f);
    private void OnEnable()
    {
        TimeManager.onDayChanged += resetDayLight;
        TimeManager.onSecondChanged += rotareSkybox;
        TimeManager.onHourChanged += HandleHourChange;
    }

    private void OnDisable()
    {
        TimeManager.onDayChanged -= resetDayLight;
        TimeManager.onSecondChanged -= rotareSkybox;
        TimeManager.onHourChanged -= HandleHourChange;
    }



    private void rotareSkybox()
    {
        transform.Rotate(0.004167f, 0, 0);
    }

    private void resetDayLight()
    {
        currentAmbient = 1.0f;
        transform.rotation = Quaternion.Euler(-90, -90, 0);
    }


    private void HandleHourChange()
    {
        if (TimeManager.Hour == 19)
        {
            TimeManager.onSecondChanged += nightIntensity;
        } 
        if(TimeManager.Hour == 23)
        {
            TimeManager.onSecondChanged -= nightIntensity;
        }
        if (TimeManager.Hour == 7)
        {
            TimeManager.onSecondChanged += dayIntensity;
        }
        if (TimeManager.Hour == 9)
        {
            TimeManager.onSecondChanged -= dayIntensity;
        }
    }

    private void dayIntensity()
    {
        Sun.intensity += 0.00023f;
        currentAmbient += 0.00027f;
        UpdateAmbient();
    }

    private void nightIntensity()
    {
        if(Sun.intensity < 0)
        {
            TimeManager.onSecondChanged -= nightIntensity;
        }
        Sun.intensity -= 0.00026f;
        currentAmbient -= 0.00027f;
        UpdateAmbient();
    }

    private void UpdateAmbient()
    {
        currentAmbient = Mathf.Clamp(currentAmbient, 0f, 1f);

        RenderSettings.skybox.color = baseAmbientColor * currentAmbient;

        DynamicGI.UpdateEnvironment();
    }

}
