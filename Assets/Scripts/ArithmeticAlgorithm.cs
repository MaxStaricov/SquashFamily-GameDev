using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArithmeticAlgorithm : MonoBehaviour
{


    [Serializable]
    public  struct ProblemRange
    {
        public int min;
        public int max;
    }

    [Header("Диапазоны чисел")]
    public static ProblemRange numberRange = new ProblemRange { min = 1, max = 10 };

    [Header("Максимальное количество операций в примере")]
    [Range(1, 5)] public static int maxOperations = 3;

    [Header("Вероятность операций")]
    [Range(0f, 1f)] public static float chanceAddition = 0.4f;
    [Range(0f, 1f)] public static float chanceSubtraction = 0.4f;
    [Range(0f, 1f)] public static float chanceMultiplication = 0.2f;

    public static (string problem, int answer) GenerateProblem()
    {
        int operationCount = UnityEngine.Random.Range(1, maxOperations + 1);
        List<string> tokens = new List<string>();

        tokens.Add(GetRandomNumber().ToString());

        for (int i = 0; i < operationCount; i++)
        {
            string op = GetRandomOperation();
            tokens.Add(op);
            tokens.Add(GetRandomNumber().ToString());
        }

        string problem = string.Join("", tokens);
        int answer = RPN.CalculationArithmetic(problem);

        return (problem, answer);
    }

    private static int GetRandomNumber()
    {
        return UnityEngine.Random.Range(numberRange.min, numberRange.max + 1);
    }

    private static string GetRandomOperation()
    {
        float roll = UnityEngine.Random.value;

        if (roll < chanceAddition)
            return "+";
        else if (roll < chanceAddition + chanceSubtraction)
            return "-";
        else
            return "*";
    }
    void Start()
    {

        var result = GenerateProblem();
        Debug.Log($"Пример: {result.problem}");
        Debug.Log($"Правильный ответ: {result.answer}");
    }


}
