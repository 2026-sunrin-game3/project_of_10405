using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MathType
{
    Increase,
    Decrease,
    Add,
    Remove
}

public class EntityStat : MonoBehaviour
{
    //공격력,방어력,피증,치확,치피,받피증,공속,이속
    Dictionary<string, float> baseValue = new();
    Dictionary<string, float> resultValue = new();

    public List<Buf> bufs = new();

    public struct Buf
    {
        public string Key;
        public MathType mathtype;
        public float Value;       
    }
    [Serializable]
    public struct StatValue
    {
        public string Key;
        public float Value;
    }
    [SerializeField]
    List<StatValue> defaultStat = new()
    {
        new StatValue{Key="attackDamage", Value=3},
        new StatValue{Key="defense", Value=0},
        new StatValue{Key="increaseDamage", Value=0},
        new StatValue{Key="critPer", Value=30},
        new StatValue{Key="critMul", Value=0},
        new StatValue{Key="hurtDamage", Value=0},
        new StatValue{Key="atkSpeed", Value=0},
        new StatValue{Key="moveSpeed", Value=0}
    };
    void Start()
    {
        foreach (StatValue val in defaultStat)
        {
            baseValue[val.Key] = val.Value;
            Calc(val.Key);
        }
    }
    public float GetResultValue(string key)
    {
        return resultValue[key];
    }

    public float Calc(string key)
    {
        float value = baseValue[key];
        float increase = 100;

        foreach (Buf buf in bufs)
        {
            switch (buf.mathtype)
            {
                case MathType.Increase:
                    increase += buf.Value;
                    break;
                case MathType.Decrease:
                    increase -= buf.Value;
                    break;
                case MathType.Add:
                    increase += buf.Value;
                    break;
                case MathType.Remove:
                    increase -= buf.Value;
                    break;
            }
        }
        return resultValue[key] = value * increase * 0.01f;
    }
}
