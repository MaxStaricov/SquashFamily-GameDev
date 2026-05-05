using System.Collections;
using UnityEditor;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{

    public static FridgeManager Instance { get; private set; }
    private static Food[] foods;
    [SerializeField] private int deliveryTime = 15;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            foods = Resources.FindObjectsOfTypeAll<Food>();
        }
    }

    public static bool setFoodInFridge(FoodList foodType)
    {
        if (Instance == null)
        {
            Debug.LogError("FridgeManager instance not found! Make sure FridgeManager is on a GameObject in the scene.");
            return false;
        }

        foreach (Food currentFood in foods)
        {
            if (currentFood.typeOfFood == foodType && !currentFood.gameObject.activeSelf && !currentFood.inDeliver)
            {
                Instance.StartCoroutine(Instance.Delivery(currentFood));
                currentFood.inDeliver = true;
                return true;
            }
        }
        return false;
    }

    IEnumerator Delivery(Food product)
    {
        yield return new WaitForSeconds(deliveryTime);
        product.gameObject.SetActive(true);
        product.inDeliver = false;
    }
}
