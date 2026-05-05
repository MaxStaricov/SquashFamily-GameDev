// using UnityEngine;

// public static class CockroachManager
// {
//     public static int maxCount{get; private set;} = 100;
//     public static int currentCount{get; private set;} = 0;

//     // public static CockroachManager Instance{get; private set;}

//     public static void Decrease(int count) {
//         currentCount -= Mathf.Abs(count);

//         if(currentCount / 9 < currentCount) Stress.Instance.UpdateValue(-1);
//     }

//     public static void Increase(int count) {
//         currentCount += Mathf.Abs(count);
        
//         if(currentCount / 9 > currentCount) Stress.Instance.UpdateValue(1);
//     }
// }


using UnityEngine;

public static class CockroachManager
{
    public static int maxCount { get; private set; } = 120;
    public static int currentCount { get; private set; } = 0;

    private static int previousWave = 0;

    public static void Increase(int count) {
        currentCount = Mathf.Min(maxCount, currentCount + Mathf.Abs(count));
        UpdateStress();
    }

    public static void Decrease(int count) {
        currentCount = Mathf.Max(0, currentCount - Mathf.Abs(count));
        UpdateStress();
    }

    public static void Reset() {
        currentCount = 0;
    }

    private static void UpdateStress() {
        int wave = currentCount / 3; 
        int stressWave = Mathf.Max(0, wave - 3);

        int prevWave = previousWave;
        int prevStressWave = Mathf.Max(0, prevWave - 3);

        int stressChange = stressWave - prevStressWave;

        if (stressChange != 0)
        {
            Stress.Instance.UpdateValue(stressChange);
        }

        previousWave = wave;
    }
}
