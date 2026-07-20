using System.Collections.Generic;
using UnityEngine;
public enum MathType
{
    Increase,
    Decrease,
    Add,
    Remove
}
public class EntityStat : MonoBehaviour
{
    Dictionary<string, float> baseValue = new();

    Dictionary<string, float> resultValue = new();

    public List<Buf> bufs = new();
    public struct Buf
    {
        public string Key;

        public MathType mathType;

        public float Value;
    }
    [System.Serializable]
    struct StatValue
    {
        public string Key;
        public float Value;
    }

    [SerializeField]
    List<StatValue> defaultStat = new()
    {
        new StatValue{Key="attackDamage", Value=3},
        new StatValue{Key="defense", Value = 0},
        new StatValue{Key="increaseDamage", Value=0},
        new StatValue{Key="critPer", Value=3},
        new StatValue{Key="critMul", Value=0},
        new StatValue{Key="hurtDamage", Value=0},
        new StatValue{Key="atkSpeed", Value=0},
        new StatValue{Key="moveSpeed", Value=0}
    };
   
    // 공겨격, 방어력, 가하는 피해 증가, 치명타 확률 / 피해, 받는 피해 증가, 공격 속도, 이동 속도
    void Start()
    {
        foreach (StatValue val in defaultStat)
        {
            baseValue[val.Key] = val.Value;
            Calc(val.Key);
        }
    }

    public float GetResultValue(string Key)
    {
        return resultValue[Key];
    }
    public float GetBaseValue(string Key)
    {
        return baseValue[Key];
    }
    public float Calc(string Key)
    {
        float value = baseValue[Key];
        float increase = 100;

        foreach (Buf buf in bufs)
        {
            if(buf.Key != Key)
                continue;
            switch (buf.mathType)
            {
                case MathType.Increase:
                    increase += buf.Value;
                    break;
                case MathType.Decrease:
                    increase -= buf.Value;
                    break;
                case MathType.Add:
                    value += buf.Value;
                    break;
                case MathType.Remove:
                    value -= buf.Value;
                    break;
            }

        }

        return resultValue[Key] = value * increase * 0.01f;
    }
}