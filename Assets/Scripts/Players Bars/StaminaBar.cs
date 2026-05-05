using System.Collections;
using UnityEngine;

public class StaminaBar : MonoBehaviour {
    [SerializeField] private StatusBar stamina;
    private SatietyBar satiety;
    private Coroutine coroutine = null;

    public float speedIncrease = 60f;
    public float speedDecrease = 1f;
    private float timer = 0f;
    public float wait = 10f;



    // <---------- Singlton ---------->

    public StatusBar StatusBar => stamina;

    public static StaminaBar Instance {get; private set;}

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
        yield return BarUtilities.CoroutineUpdateFix(stamina, delta, speed);
        coroutine = null;
    }

    public bool CheckUpdate(float delta) {
        return (stamina.Present.value + delta < stamina.Present.minValue) ? false : true;
    }

    public bool CheckEmpty() {
        return (stamina.Present.value <= stamina.Present.minValue) ? true : false;
    }

    public void UpdateValue(float delta, float speed) {
        if(coroutine != null) {
            float signDelta = Mathf.Sign(delta);
            float signAnimation = Mathf.Sign(stamina.Future.value - stamina.Present.value);

            if(signDelta == signAnimation) stamina.Future.value += delta;
            else if(signDelta < 0 && signAnimation > 0) stamina.UpdateValue(delta);
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
            float signAnimation = Mathf.Sign(stamina.Future.value - stamina.Present.value);

            if(signDelta == signAnimation) stamina.Future.value += delta;
            else if(signDelta < 0 && signAnimation > 0) stamina.UpdateValue(delta);
            else if(signDelta > 0 && signAnimation < 0) {
                BreakCoroutine();
                coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speedIncrease));
            }

            return;
        }

        float speed = delta > 0 ? speedIncrease : speedDecrease;

        BreakCoroutine();
        coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speed));

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



    // <---------- Other ---------->

    private void AutoUpdateStatusBar() {
        if(coroutine == null) {
            timer += Time.deltaTime;
        }

        if(timer >= wait && stamina.Present.value < satiety.StatusBar.Present.value) {
            BreakCoroutine();

            float delta = satiety.StatusBar.Present.value - stamina.Present.value;

            coroutine = StartCoroutine(RunCoroutineUpdateFix(delta, speedIncrease));
            
            // Debug.Log($"{timer} / {wait}");
        }

        if(timer >= wait) {
            timer = 0;
        }

        return;
    }



    // <---------- Game ---------->

    void Start() {
        satiety = SatietyBar.Instance;

        stamina.SetThresholds(0, 100);
        stamina.SetValue(0);

        UpdateValue(stamina.Present.maxValue, SatietyBar.Instance.speedIncrease);
    }

    void Update() {
        // if(Input.GetKeyDown(KeyCode.Semicolon)) {
        //     UpdateValue(-10f);
        // }
        // else if(Input.GetKeyDown(KeyCode.Quote)) {
        //     UpdateValue(10f, 3f);
        // }

        AutoUpdateStatusBar();
    }

}
