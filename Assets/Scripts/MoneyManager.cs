using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static Action onMoneyChanged;

    private void Awake()
    {
        money = 20;
    }

    private static int money;
    public static int getBalance()
    {
        return money;
    }

    public static void increaseBalance(int addMoney)
    {
        money += addMoney;
        onMoneyChanged?.Invoke();
    }

    public static void decreasBalance(int subMoney) 
    {
        money -= subMoney;
        onMoneyChanged?.Invoke();
    }

    public static string provideTransaction(int price, FoodList typeOfFood)
    {

        if(money >= price)
        {
            bool doesFridgeHasPlace = FridgeManager.setFoodInFridge(typeOfFood);
            if (doesFridgeHasPlace)
            {
                money -= price;
                onMoneyChanged?.Invoke();
                return "������, ��������";
            }
            else
            {
                return "� ������������ ��� �����";
            }
        }
        else
        {
            return "������������ �������";
        }
    }
}
