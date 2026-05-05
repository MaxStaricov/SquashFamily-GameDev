using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{

    public FoodList typeOfFood;
    private Button buyButton;
    public int price;
    public StatusPanelController statusPanelController;

    void Start()
    {
        buyButton = GetComponent<Button>();
        buyButton.onClick.AddListener(processTransaction);
    }


    private void processTransaction()
    {
        string popupText = MoneyManager.provideTransaction(price, typeOfFood);
        statusPanelController.ShowStatusPanel(popupText);
    }


}
