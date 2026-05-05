using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string[] losePhrases{get; private set;} = {
        "Откинулся",
        "Склеил ласты",
        "Помер", 
        "Пельмени не пережил...",
        "На корм тараканам",
        "Покойся с шавермой",
        "Ушёл в гастрономический закат",
        "Последний дошик так и не сварил",
        "Не поел — не пожил",
        "Заказал еду — не дождался",
        "Пищевой крах настиг",
        "Проиграл холодильнику",
        "Пал в битве",
        "Не дожил до стипендии",
        "Забыли покормить студента",
        "Гастрит победил",
        "Второй ужин оказался последним",
        "Перешёл на рацион святого духа",
        "Стал частью пищевой цепи общажных тараканов",
        "Обнулил счётчик калорий до критического минимума",
        "Отправился искать котлеты на Марсе",
        "Ушёл на свидание с пакетом гречки, но так и не дошёл",
        "Был переварен временем",
        "Пост добрался до высшей точки",
        "Сгинул между плитой и холодильником",
        "Растворился в аромате доширака",
        "Кони двинул",
        "Коньки отбросил"
    };

    public string[] winPhrases{get; private set;} = {
        "Переиграл тараканов и систему образования.",
        "Голод не тётка, но ты и её уделал.",
        "Дошёл до финиша — и не развалился по дороге.",
        "Даже тараканы аплодируют стоя.",
        "Стипендия теперь тебе по праву!",
        "А теперь можешь вернуться в реальность. Тренировка окончена.",
        "Дошик — съеден. Курсовая — сдана. Победа — твоя.",
        "Ты сделал невозможное... закончил семестр.",
        "Курсач сдан. В желудке — пусто, в глазах — огонь.",
        "Выжил назло тараканам.",
        "Теперь ты можешь спать. Неделю.",
        "Стал богом общаги. По версии тараканов.",
        "Ты стал сильнее. Голоднее. Мудрее.",
        "Сдался бы — остался бы жив, но туп.",
        "Теперь ты знаешь, как пахнет победа. И это не доширак.",
        "Победа! Остался только один босс — диплом.",
        "Одержал победу и не стал частью рациона.",
        "Съел всех тараканов и написал курсовую их лапками.",
        "Теперь твой желудок пуст, но душа полна смысла.",
        "И тараканы, и голод — всё было не зря.",
        "Когда-нибудь об этом снимут фильм. В жанре 'ужасы'.",
        "Сдал курсовую, хотя твой организм давно не верил в тебя.",
        "Ты пережил общагу. А значит, переживёшь и жизнь.",
        "Теперь ты достоин кружки «Я выжил в общаге»",
        "Курсовая сдана. Никто не верил. Даже ты.",
        "Теперь ты официально не зря ел весь этот дошик.",
        "Ты победил систему. Ну, один раз. С натяжкой.",
        "Общага: 0, Ты: 1.",
        "И кто сказал, что 43 правки — это много?",
        "Курсовая сдана. Гастрит – бонусом.",
        "Тараканы теперь просят у тебя советы по выживанию.",
        "Голод не помешал. А вот орфография – да.",
        "Выжил, сдал, пожалел, но поздно.",
        "Теперь можешь с гордостью сказать: «Я это сделал, но зачем?»",
        "С курсовой покончено. Осталась только вся остальная жизнь.",
        "Победа достигнута. По всем статьям студенческого выживания.",
        "Сдал курсовую и стал на шаг ближе к диплому... и выгоранию.",
        "Теперь даже тараканы боятся твоей решимости.",
        "Теперь твой мозг может официально уйти в отпуск."
    };

    [SerializeField] private GameObject[] gameObjects;
    private bool isRunning = false;
    [SerializeField] private Image startScreen;
    [SerializeField] private float fadeDuration = 10f;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject startCamera;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI loseText;

    private float currentAlpha = 1f;
    private Color startColor;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        restartButton.onClick.AddListener(Restart);
    }

    private void Start()
    {
        if (RestartManager.Instance.firstPlay)
        {
            startColor = startScreen.color;
            currentAlpha = startColor.a;

            foreach (var go in gameObjects)
            {
                go.SetActive(false);
            }



            if (loseScreen != null)
            {
                loseScreen.SetActive(false);
            }
            else
            {
                Debug.LogWarning("GameManager: Lose Screen �� �������� � ����������!");
            }
        }
        else
        {
            startScreen.gameObject.SetActive(false);
            startCamera.SetActive(false);
            foreach (var go in gameObjects)
            {
                go.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (RestartManager.Instance.firstPlay)
        {
            if (!isRunning)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    isRunning = true;
                    startText.SetActive(false);
                    StartCoroutine(Delay());
                }
            }
            else
            {
                currentAlpha -= Time.deltaTime / fadeDuration;

                if (currentAlpha <= 0f)
                {
                    currentAlpha = 0f;

                }

                startScreen.color = new Color(startColor.r, startColor.g, startColor.b, currentAlpha);
            }
        }
        {

        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);
        startCamera.SetActive(false);
        foreach (var go in gameObjects)
        {
            go.SetActive(true);
        }

        startScreen.gameObject.SetActive(false);
    }

    private void Restart()
    {
        TimeManager.ResetTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // RestartManager.Instance.firstPlay = false;
        CockroachManager.Reset();
    }

    public void Lose(string text = "")
    {
        Cursor.lockState = CursorLockMode.None;
        foreach (var go in gameObjects)
        {
            go.SetActive(false);
        }
        loseScreen.SetActive(true);
        loseText.text = text += "\n\n\n *У нас не хватило бюджета на меню проирыша";
    }

    public void Win(string text = "")
    {
        Cursor.lockState = CursorLockMode.None;
        foreach (var go in gameObjects)
        {
            go.SetActive(false);
        }
        loseScreen.SetActive(true);
        loseText.text = text += "\n\n\n *У нас не хватило бюджета на меню победы";
    }
}