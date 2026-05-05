using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SatietyBar : MonoBehaviour
{
    [SerializeField] private StatusBar satiety;
    [SerializeField] private Gradient gradient;
    [SerializeField] private UnityEngine.UI.Image fill;
    private Coroutine coroutine = null;
    
    public float speedIncrease{get; set;} = 3f;
    public float speedDecrease{get; set;} = 1f;
    private float timer = 0f;
    public float timeWait{get; set;} = 6f;
    public float decreaseAmount{get; private set;} = 1f;

    public StatusBar StatusBar => satiety;
    public static SatietyBar Instance{get; private set;}

    void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        return;
    }



    // <---------- Coroutine ---------->

    private IEnumerator RunCoroutineUpdateFix(float delta, float duration) {
        yield return BarUtilities.CoroutineUpdateFix(satiety, delta, duration);

        coroutine = null;
    }

    public void UpdateValue(float delta, float speed) {
        if(coroutine != null) {
            float signDelta = Mathf.Sign(delta);
            float signAnimation = Mathf.Sign(satiety.Future.value - satiety.Present.value);

            if(signDelta == signAnimation) satiety.Future.value += delta;
            else if(signDelta < 0 && signAnimation > 0) satiety.UpdateValue(delta);
            else if(signDelta > 0 && signAnimation < 0) {
                BreakCoroutine();
                coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speed));
            }

            return;
        }

        BreakCoroutine();
        coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speed));
        
        timer = 0f;

        return;
    }

    public void UpdateValue(float delta) {
        if(coroutine != null) {
            float signDelta = Mathf.Sign(delta);
            float signAnimation = Mathf.Sign(satiety.Future.value - satiety.Present.value);

            if(signDelta == signAnimation) satiety.Future.value += delta;
            else if(signDelta < 0 && signAnimation > 0) satiety.UpdateValue(delta);
            else if(signDelta > 0 && signAnimation < 0) {
                BreakCoroutine();
                coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speedIncrease));
            }

            return;
        }

        float duration = delta > 0 ? speedIncrease : speedDecrease;

        BreakCoroutine();
        coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, duration));
        
        timer = 0f;

        return;
    }

    public void BreakCoroutine() {
        if(coroutine != null) {
            StopCoroutine(coroutine);
        }

        coroutine = null;
        return;
    }

    public bool CheckEmpty() {
        return (satiety.Present.value <= satiety.Present.minValue) ? true : false;
    }



    // <---------- Other ---------->

    private void AutoUpdateStatusBar() {
        if(coroutine == null) {
            timer += Time.deltaTime;
        }
        
        if(timer >= timeWait) {
            BreakCoroutine();
            coroutine = StartCoroutine(RunCoroutineUpdateFix(-decreaseAmount, 0.1f));
            timer = 0;
        }

        fill.color = gradient.Evaluate(satiety.Present.normalizedValue);

        if(CheckEmpty()) {
            string[] frases = GameManager.Instance.losePhrases;
            string frase = frases[UnityEngine.Random.Range(0, frases.Length)];
            GameManager.Instance.Lose(frase);
        }

        return;
    }



    // <---------- Game ---------->

    void Start() {

        satiety.SetThresholds(0, 100);
        satiety.SetValue(0);
    
        UpdateValue(satiety.Present.maxValue);
    }

    void Update() {
        // if(Input.GetKeyDown(KeyCode.Period)) {
        //     UpdateValue(-10f);
        // }
        // else if(Input.GetKeyDown(KeyCode.Slash)) {
        //     UpdateValue(10f);
        // }

        AutoUpdateStatusBar();
    }
}
