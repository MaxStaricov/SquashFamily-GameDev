using TMPro;
using UnityEngine;

public class BalanceDisplay : MonoBehaviour
{
    private TextMeshProUGUI[] moneyTexts;

    private void Start()
    {
        GameObject[] textObjects = GameObject.FindGameObjectsWithTag("MoneyDisplay");

        moneyTexts = new TextMeshProUGUI[textObjects.Length];

        for (int i = 0; i < textObjects.Length; i++)
        {
            moneyTexts[i] = textObjects[i].GetComponent<TextMeshProUGUI>();
        }
        UpdateAllDisplays();
    }

    private void OnEnable()
    {
        MoneyManager.onMoneyChanged += UpdateAllDisplays;
    }

    private void OnDisable()
    {
        MoneyManager.onMoneyChanged -= UpdateAllDisplays;
    }

    private void UpdateAllDisplays()
    {
        foreach (var text in moneyTexts)
        {
            if (text != null)
            {
                text.text = MoneyManager.getBalance().ToString();
            }
        }
    }
}
