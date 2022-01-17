using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] ScriptableObject_Levels lvlConfig;

    public int CurrentLevel { get; private set; }
    public void SetCurrentLevel(int lvl)
    {
        CurrentLevel = lvl;
    }

    int numOfMutantsSpawned = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CurrentLevel = 1;
    }

    LevelInfo GetCurrentLevelInfo()
    {

        return lvlConfig.levels[CurrentLevel - 1];

    }

    public MutantInfo GetMutant()
    {
        LevelInfo currentLevelInfo = GetCurrentLevelInfo();
        numOfMutantsSpawned++;
        return new MutantInfo(null, GetHpAttributes(currentLevelInfo._questionInfo));

    }

    string a = "# + #";
    string b = "# - #";
    string c = "# + # - #";
    string d = "# - # + #";

    HPAttributes GetHpAttributes(QuestionInfo questionInfo)
    {
        var regex = new Regex(Regex.Escape("#"));
        string operationString = "#";
        int numberOfOperands = 1;
        int totalHp = 0;

        if (questionInfo._questionType == QuestionType.EQUATION)
        {
            if (questionInfo._operandsAllowed == OperandsAllowed.PLUS)
            {
                operationString = a;
                numberOfOperands = 2;

                totalHp = GetFeasibleNum(questionInfo.lowerLimit, questionInfo.upperLimit + 1);
                //80% chance to round off number
                totalHp = UnityEngine.Random.Range(0, 10) < 5 ? RoundNumberOff(totalHp) : totalHp;

                int num = GetFeasibleNum(1, totalHp);
                //80% chance to round off number
                num = UnityEngine.Random.Range(0, 10) < 8 ? RoundNumberOff(num) : num;
                operationString = regex.Replace(operationString, num.ToString(), 1);


                operationString = regex.Replace(operationString, (totalHp - num).ToString(), 1);

            }
            else if (questionInfo._operandsAllowed == OperandsAllowed.MINUS)
            {
                operationString = b;
                numberOfOperands = 2;

                int num1 = GetFeasibleNum(questionInfo.lowerLimit, questionInfo.upperLimit);
                //80% chance to round off number
                num1 = UnityEngine.Random.Range(0, 10) < 5 ? RoundNumberOff(num1) : num1;

                int num2 = GetFeasibleNum(num1 + 1, questionInfo.upperLimit + 1);
                //80% chance to round off number
                num2 = UnityEngine.Random.Range(0, 10) < 8 ? RoundNumberOff(num2) : num2;
                num2 = num2 == num1 ? (num2 + GetFeasibleNum(1, 9)) : num2;

                operationString = regex.Replace(operationString, num2.ToString(), 1);

                Debug.LogError(num1);
                totalHp = num1;
                operationString = regex.Replace(operationString, (num2 - num1).ToString(), 1);
            }
            else
            {
                operationString = UnityEngine.Random.Range(0, 2) == 0 ? c : d;
                numberOfOperands = 3;
            }
        }
        else 
        {
            totalHp = GetFeasibleNum(questionInfo.lowerLimit, questionInfo.upperLimit + 1);
            operationString = totalHp.ToString();
        }



        //for (int i = 0; i < numberOfOperands; i++)
        //{
        //    int num = GetFeasibleNum(questionInfo.lowerLimit, questionInfo.upperLimit);
        //    operationString = regex.Replace(operationString, num.ToString(), 1);
        //    totalHp = num;


        //}

        return new HPAttributes(operationString, totalHp);
        //return new HPAttributes("40+50", 90);
    }

    int GetFeasibleNum(int lowerLimit,int upperLimit)
    {
        return UnityEngine.Random.Range(lowerLimit, upperLimit);
    }

    int RoundNumberOff(int num)
    {
        return num / 10 * 10;
    }

    public bool CanSpawnMutant()
    {
        LevelInfo currentLevelInfo = GetCurrentLevelInfo();
        return numOfMutantsSpawned < currentLevelInfo.noOfMutants ? true: false;
    }
}

[Serializable]
public class LevelInfo
{
    [SerializeField] public int noOfMutants;

    [SerializeField] public QuestionInfo _questionInfo;
}

[Serializable]
public class QuestionInfo
{
    [SerializeField] public QuestionType _questionType;
    [SerializeField] public OperandsAllowed _operandsAllowed;

    [SerializeField] public int lowerLimit;
    [SerializeField] public int upperLimit;

}