using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Stress : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] points;
    [SerializeField] private Sprite sprite;
    private int countPoints = 0;

    private StaminaBar stamina;
    private float affectOnStaminaSpeedFill = 6f;
    private float affectOnStaminaWait = 0f;

    private SatietyBar satiety;
    private float affectOnSatietySpeedFill = 0f;
    private float affectOnSatietyWait = -0.3f;

    public static Stress Instance{get; private set;}

    void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }

        return;
    }


    // <----------Other ---------->

    private void DrawPoints() {
        for(int i = 0; i < points.Length; ++i) {
            if(i < countPoints) points[i].enabled = true;
            else points[i].enabled = false;
        }

        return;
    }

    public bool CheckUpdate(int delta) {
        int result = countPoints + delta;
        
        if((result < 0) || (result > 10) || (result == countPoints)) return false;
        
        return true;
    }

    public void UpdateValue(int delta) {
        int result = countPoints + delta;
        if((result < 0) || (result > 10) || (result == countPoints)) return;

        countPoints = result;

        stamina.speedIncrease += delta * affectOnStaminaSpeedFill;
        stamina.wait += delta * affectOnStaminaWait;

        satiety.speedIncrease += delta * affectOnSatietySpeedFill;
        satiety.timeWait += delta * affectOnSatietyWait;
        
        return;
    }



    // <---------- Game ---------->

    void Start() {
        countPoints = 0;
        
        stamina = StaminaBar.Instance;
        satiety = SatietyBar.Instance;
    }

    void Update() {
        // if(Input.GetKeyDown(KeyCode.LeftBracket)) {
        //     UpdateValue(-1);
        // }
        // else if(Input.GetKeyDown(KeyCode.RightBracket)) {
        //     UpdateValue(1);
        // }

        DrawPoints();
    }
}
