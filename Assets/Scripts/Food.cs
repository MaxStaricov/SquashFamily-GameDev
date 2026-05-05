using UnityEngine;

public class Food : MonoBehaviour, Interactable
{

    public FoodList typeOfFood;
    public  bool inDeliver = false;
    public void OnInteract()
    {
        switch (typeOfFood)
        {
            case FoodList.doshik:
                StaminaBar.Instance.UpdateValue(-15);
                SatietyBar.Instance.UpdateValue(30);
                break;
            case FoodList.apple:
                StaminaBar.Instance.UpdateValue(25);
                SatietyBar.Instance.UpdateValue(5);
                break;
            case FoodList.beer:
                StaminaBar.Instance.UpdateValue(45, 5);
                SatietyBar.Instance.UpdateValue(-10);
                break;
        }
        gameObject.SetActive(false);
    }
}
