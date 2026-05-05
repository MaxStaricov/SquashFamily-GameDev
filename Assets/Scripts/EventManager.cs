using UnityEngine;

public class EventManager : MonoBehaviour { 
    [SerializeField] public PipeLeakEvent pipeLeakEvent;
    int timeForPipeEvent = 10;

    private void OnEnable()
    {
        TimeManager.onHourChanged += TimeChecker;
    }

    private void OnDisable()
    {
        TimeManager.onHourChanged -= TimeChecker;
    }

    public void Start()
    {
        timeForPipeEvent = Random.Range(1, 24);
    }
    private void TimeChecker()
    {
        if (TimeManager.Hour == 6)
        {
            Debug.Log("123");
        }

        Debug.Log("��������� ����� ��� �������: " + timeForPipeEvent);
        if (TimeManager.Hour == timeForPipeEvent)
        {
            pipeLeakEvent.StartEvent();
        }    
    }
}