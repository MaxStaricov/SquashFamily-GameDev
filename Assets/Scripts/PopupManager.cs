using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popupMessage; // Твой Panel/Text элемент
    [SerializeField] private Text messageText;        // Поле текста внутри popup

    private float displayDuration = 3f; // Сколько времени показывать popup

    // Метод для вызова popup из других скриптов
    public void ShowPopup(string message)
    {
        if (messageText != null)
            messageText.text = message;

        popupMessage.SetActive(true);

        Invoke("HidePopup", displayDuration);
    }

    private void HidePopup()
    {
        popupMessage.SetActive(false);
    }
}