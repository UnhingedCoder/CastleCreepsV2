using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "Scriptable Objects/Level Info")]
public class ScriptableObject_Levels : ScriptableObject
{
    [SerializeField] public List<LevelInfo> levels;


   
}

public enum QuestionType
{
    WHOLE,
    EQUATION
}

public enum OperandsAllowed
{
    PLUS,
    MINUS,
   // BOTH
}



