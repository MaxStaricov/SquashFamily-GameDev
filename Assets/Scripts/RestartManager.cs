using UnityEngine;

public class RestartManager : MonoBehaviour
{
    public static RestartManager Instance { get; private set; }

    public bool firstPlay = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}