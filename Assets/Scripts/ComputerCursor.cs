using UnityEngine;
using UnityEngine.UI;

public class ComputerCursor : MonoBehaviour
{

    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform cursor;
    void Start()
    {

    }

    void Update()
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, Camera.main, out anchoredPos);
        if (RectTransformUtility.RectangleContainsScreenPoint(canvas, Input.mousePosition, Camera.main))
        {
            cursor.localPosition = anchoredPos;
        }
    }
}