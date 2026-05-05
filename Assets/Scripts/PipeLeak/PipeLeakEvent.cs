using System.Collections.Generic;
using System.Diagnostics;
using Mono.Cecil.Cil;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PipeLeakEvent : MonoBehaviour, Interactable
{
    [SerializeField] private List<GameObject> particleSystem; // Эффект протечки
    [SerializeField] private GameObject puzzleUI;       // UI миниигры
    private bool eventStart = false;
    private bool isPuzzleActive = false;                // Состояние головоломки

    public void setActiveParticle(bool value) 
    {
        foreach (GameObject particle in particleSystem)
        {
            particle.SetActive(value);
        }
    }

    public void OnInteract()
    {
        // 🔒 Проверяем, началось ли событие
        if (!eventStart)
        {
            Debug.Log("Событие не началось. Взаимодействие невозможно.");
            return;
        }

        isPuzzleActive = !isPuzzleActive;

        if (isPuzzleActive)
        {
            Debug.Log("Старт миниигры");
            StartPuzzle();
        }
        else
        {
            FinishPuzzle(-1);
        }

        Debug.Log("Взаимодействие: " + (isPuzzleActive ? "Открыто" : "Закрыто"));
    }

    public void StartEvent()
    {
        if (eventStart) return;
        else 
        {
            setActiveParticle(true);
            eventStart = true;
        }
    }
    void StartPuzzle()
    {
        Time.timeScale = 0f;              // Остановить время

        Cursor.lockState = CursorLockMode.None; // Разблокировать курсор
        Cursor.visible = true;            // Показать курсор
        Debug.Log("До запуска UI");
        puzzleUI.SetActive(true);         // Открыть UI миниигры
    }

    public int FinishPuzzle(int code)
    {
        if (code == 0){
            setActiveParticle(false);
            eventStart = false;
        }

        Time.timeScale = 1f;              // Возобновить игру
        Cursor.lockState = CursorLockMode.Locked; // Заблокировать курсор обратно
        Cursor.visible = false;           // Скрыть курсор

        puzzleUI.SetActive(false);        // Закрыть UI
        return code;

    }
}