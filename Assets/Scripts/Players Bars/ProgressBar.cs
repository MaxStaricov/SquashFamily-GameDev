using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private StatusBar progress;
    private float speedIncrease = 3f;
    private StaminaBar stamina = StaminaBar.Instance;
    public static ProgressBar Instance{get; private set;}
    private Coroutine coroutine = null;



    // <---------- Singlton ---------->

    public StatusBar StatusBar => progress;

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

    private IEnumerator RunCoroutineUpdateFix(float delta, float speed) {
        yield return BarUtilities.CoroutineUpdateFix(progress, delta, speed);
        coroutine = null;
        
        if(CheckFull()) {
            string[] frases = GameManager.Instance.winPhrases;
            string frase = frases[UnityEngine.Random.Range(0, frases.Length)];
            GameManager.Instance.Win(frase);
        }
    }

    // public void UpdateValue(float duration) {
    //     UpdateValue(stamina.StatusBar.Present.value, st)
    // }

    public bool CheckEmpty() {
        return StatusBar.Present.value == StatusBar.Present.minValue;
    }

    public bool CheckFull() {
        return StatusBar.Present.value == StatusBar.Present.maxValue;
    }

    public bool CheckUpdate(float delta) {
        if(stamina.StatusBar.Present.value + delta < stamina.StatusBar.Present.minValue) return false;
        
        return true;
    }

    public void UpdateValue(float delta, float speed) {
        if(coroutine != null) {
            float signDelta = Mathf.Sign(delta);
            float signAnimation = Mathf.Sign(stamina.StatusBar.Future.value - stamina.StatusBar.Present.value);

            if(signDelta == signAnimation) stamina.StatusBar.Future.value += delta;
            else if(signDelta < 0 && signAnimation > 0) stamina.UpdateValue(delta);
            else if(signDelta > 0 && signAnimation < 0) {
                BreakCoroutine();
                coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speed));
            }

            return;
        }

        BreakCoroutine();
        coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speed));

        return;
    }

    public void UpdateValue(float delta) {
        if(coroutine != null) {
            float signDelta = Mathf.Sign(delta);
            float signAnimation = Mathf.Sign(stamina.StatusBar.Future.value - stamina.StatusBar.Present.value);

            if(signDelta == signAnimation) stamina.StatusBar.Future.value += delta;
            else if(signDelta < 0 && signAnimation > 0) stamina.UpdateValue(delta);
            else if(signDelta > 0 && signAnimation < 0) {
                BreakCoroutine();
                coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speedIncrease));
            }

            return;
        }

        BreakCoroutine();
        coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speedIncrease));

        return;
    }

    public void BreakCoroutine() {
        if(coroutine != null) {
            StopCoroutine(coroutine);
            // stamina.BreakCoroutine();
        }

        coroutine = null;
        return;
    }


    void Start() {
        stamina = StaminaBar.Instance;

        progress.SetThresholds(0, 700);
        progress.SetValue(0);
    }

    // void Update() {
    //     // if(Input.GetKeyDown(KeyCode.Equals)) {
    //     //     // float points = stamina.StatusBar.Present.value - stamina.StatusBar.Present.minValue;
    //     //     // UpdateValue(points, poi);
    //     //     UpdateValue(100);
    //     // }
    //     // else if(Input.GetKeyDown(KeyCode.Minus)) {
    //     //     if(coroutine != null) {
    //     //         stamina.BreakCoroutine();
    //     //         stamina.StatusBar.SetValue(stamina.StatusBar.Present.value);
    //     //     }
    //     //     BreakCoroutine();
    //     //     // progress.SetValue(0);
    //     // }
    // }
}
