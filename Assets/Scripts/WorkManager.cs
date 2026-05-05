using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class WorkManager : MonoBehaviour
{
    public Slider timerSlider;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI problemText;
    [SerializeField] private TextMeshProUGUI solution;
    [SerializeField] private Image startScreen;
    [SerializeField] private Image workScreen;
    public Image fillImage;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;

    public Color startColor = Color.green;
    public Color endColor = Color.red;
    private Gradient gradient = new Gradient();
    private static (string problem, int answer) problem;

    public float timeLimit = 10f;

    private void Start()
    {
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.green, 0.0f),
                new GradientColorKey(Color.yellow, 0.5f),
                new GradientColorKey(Color.red, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1, 0.0f),
                new GradientAlphaKey(1, 1.0f)
            }
        );

        if (fillImage != null)
            fillImage.color = gradient.Evaluate(1.0f);
        startButton.onClick.AddListener(startSession);
        acceptButton.onClick.AddListener(checkAnswer);
        timerSlider.minValue = 0;
        timerSlider.maxValue = timeLimit;
        timerSlider.value = timeLimit;
        cancelButton.onClick.AddListener(endSession);
    }

    private void startSession()
    {
        workScreen.gameObject.SetActive(true);
        newExample();
    }

    private void endSession()
    {
        workScreen.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private void newExample()
    {
        StopAllCoroutines();
        problem = ArithmeticAlgorithm.GenerateProblem();
        problemText.text = problem.problem;
        StartCoroutine(TimerCoroutine());
    }

    public void UpdateSliderColor(float timeLeft, float totalTime)
    {
        float ratio = timeLeft / totalTime;
        fillImage.color = gradient.Evaluate(ratio);
    }
    IEnumerator TimerCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < timeLimit)
        {
            elapsedTime += Time.deltaTime;
            timerSlider.value = timeLimit - elapsedTime; 
            UpdateSliderColor(elapsedTime, timeLimit);
            if (SatietyBar.Instance.CheckEmpty() || StaminaBar.Instance.CheckEmpty())
            {
                endSession();
            }
            yield return null;
        }
        MoneyManager.decreasBalance(15);
        // SatietyBar.Instance.UpdateValue(-15);
        newExample(); 
    }
    private void checkAnswer()
    {
        string userAnswer = solution.text.Trim((char)8203);
        if(userAnswer == Convert.ToString(problem.answer))
        {
            MoneyManager.increaseBalance(15);
            // SatietyBar.Instance.UpdateValue(-15);
            StaminaBar.Instance.UpdateValue(-15);
        }
        else
        {
            MoneyManager.decreasBalance(15);
            // SatietyBar.Instance.UpdateValue(-15);
            StaminaBar.Instance.UpdateValue(-15);
        }
        newExample();
    }


}
