using System;
using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;




public class BarUtilities: MonoBehaviour {

    // public static IEnumerator CoroutineUpdateDuration(StatusBar bar, float duration) {
    //     if(duration <= 0) yield break;

    //     float elapsed = 0f;
    //     while(elapsed < duration) {
    //         //
    //     }
    // }

    public static IEnumerator CoroutineUpdateFix(StatusBar bar, float delta, float speed) {
        if (bar == null || speed <= 0 || delta == 0) yield break;

        float future = Mathf.Clamp(bar.Future.value + delta, bar.Future.minValue, bar.Future.maxValue);
        bar.Future.value = future;

        float volume = Mathf.Abs(future - bar.Present.value);

        if(volume <= Mathf.Epsilon) yield break;
        
        // Скорость роста (ед/сек)
        float speedFill = bar.Capacity() / speed;
        
        float elapsed = 0f;
        while (volume > Mathf.Epsilon) {
            if(!Mathf.Approximately(bar.Future.value, future)) {
                speedFill = bar.Capacity() / speed;
                future = bar.Future.value;
                volume = Mathf.Abs(future - bar.Present.value);
                elapsed = 0f;
            }
            
            float deltaValue = Mathf.Min(speedFill * Time.deltaTime, volume);
            deltaValue *= Mathf.Sign(future - bar.Present.value);

            bar.Present.value += deltaValue;
            volume = Mathf.Abs(future - bar.Present.value);
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        bar.Present.value = future;
    }
}
