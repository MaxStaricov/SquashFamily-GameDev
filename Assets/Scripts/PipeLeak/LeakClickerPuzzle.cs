using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeakClickerPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject leakButtonPrefab; // Префаб кнопки
    [SerializeField] private Transform leakContainer;     // Контейнер для кнопок
    [SerializeField] private PipeLeakEvent pipeLeakEvent;

    [SerializeField] private int totalLeaks = 5; // Сколько всего протечек

    [SerializeField] private float minDistanceBetweenLeaks = 60f; // Минимальное расстояние между кнопками
    [SerializeField] private TextMeshProUGUI timerText; // Текст для отображения времени (назначь в инспекторе)

    private List<Button> activeLeaks = new List<Button>();
    private int leaksClosed = 0;

    private float timeLeft = 10f; // Начальное время
    private bool isTimerRunning = true;

    // Область труб
    private Rect pipeArea = new Rect(-400, -200, 800, 400); // xMin, yMin, width, height

    void OnEnable()
    {
        timeLeft = 4f;
        isTimerRunning = true;
        GenerateRandomLeaks();
    }

    void Update()
    {
        if (isTimerRunning && timeLeft > 0)
        {
            timeLeft -= Time.unscaledDeltaTime; // Работает даже если Time.timeScale = 0
            UpdateTimerUI();

            if (timeLeft <= 0)
            {
                OnTimeUp();
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeLeft).ToString() + " с";
        }
    }

    void GenerateRandomLeaks()
    {
        RectTransform containerRect = leakContainer.GetComponent<RectTransform>();

        for (int i = 0; i < totalLeaks; i++)
        {
            Vector2 randomPosition = GetRandomPositionInPipeArea();

            // Проверяем, чтобы новая позиция была достаточно далеко от других
            int attempts = 0;
            while (IsTooClose(randomPosition) && attempts < 100)
            {
                randomPosition = GetRandomPositionInPipeArea();
                attempts++;
            }

            if (attempts >= 100)
            {
                Debug.LogWarning("Не могу найти подходящее место для протечки");
                continue;
            }

            GameObject buttonGO = Instantiate(leakButtonPrefab, leakContainer);
            RectTransform rect = buttonGO.GetComponent<RectTransform>();
            rect.anchoredPosition = randomPosition;

            Button button = buttonGO.GetComponent<Button>();
            activeLeaks.Add(button);
            button.onClick.AddListener(() => CloseLeak(button));
        }

        leaksClosed = 0;
    }

    Vector2 GetRandomPositionInPipeArea()
    {
        float x = Random.Range(pipeArea.xMin, pipeArea.xMax);
        float y = Random.Range(pipeArea.yMin, pipeArea.yMax);
        return new Vector2(x, y);
    }

    bool IsTooClose(Vector2 position)
    {
        foreach (Button existing in activeLeaks)
        {
            RectTransform rect = existing.GetComponent<RectTransform>();
            if (Vector2.Distance(position, rect.anchoredPosition) < minDistanceBetweenLeaks)
            {
                return true;
            }
        }
        return false;
    }

    void CloseLeak(Button clickedLeak)
    {
        clickedLeak.interactable = false;
        leaksClosed++;

        timeLeft += 0.5f; // Добавляем время за каждую закрытую пробоину
        UpdateTimerUI();

        if (leaksClosed >= totalLeaks)
        {
            Debug.Log("Головоломка пройдена!");
            isTimerRunning = false;
            pipeLeakEvent.FinishPuzzle(0);

            // Удаляем все кнопки после завершения
            foreach (Transform child in leakContainer)
            {
                Destroy(child.gameObject);
            }

            activeLeaks.Clear();
        }
    }

    void OnTimeUp()
    {
        Debug.Log("Время вышло!");

        isTimerRunning = false;

        // Можно добавить реакцию на проигрыш
        // Например: показать экран поражения или повторить попытку

        pipeLeakEvent.FinishPuzzle(-1); // Или сделать отдельный метод GameOver()

        foreach (Transform child in leakContainer)
        {
            Destroy(child.gameObject);
        }

        activeLeaks.Clear();
    }
}