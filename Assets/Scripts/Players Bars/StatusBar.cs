using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Slider present;
    [SerializeField] private Slider future;

    public Slider Present => present;
    public Slider Future => future;

    public void SetThresholds(float min, float max) {
        present.minValue = future.minValue = min;
        present.maxValue = future.maxValue = max;

        return;
    }

    public bool SetValue(float newValue) {
        if((newValue < present.minValue) || (newValue > present.maxValue)) return false;
    
        present.value = future.value = newValue;
        return true;
    }

    public void UpdateValue(float delta) {
        present.value = Mathf.Clamp(present.value + delta, present.minValue, present.maxValue);
        future.value = Math.Clamp(future.value + delta, future.minValue, future.maxValue);

        return;
    }

    public float Capacity() {
        return Present.maxValue - Present.minValue;
    }

}
