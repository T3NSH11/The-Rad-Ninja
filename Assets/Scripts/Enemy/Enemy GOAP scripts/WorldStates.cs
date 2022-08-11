using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState
{
    public string Key;
    public int Value;
}

public class WorldStates
{
    public Dictionary<string, int> states;

    public WorldStates()
    {
        states = new Dictionary<string, int>();
    }

    public bool HasState(string Key)
    {
        return states.ContainsKey(Key);
    }

    void AddState(string Key, int Value)
    {
        states.Add(Key, Value);
    }
    public void RemoveState(string Key)
    {
        if (states.ContainsKey(Key))
            states.Remove(Key);
    }

    public void SetState(string Key, int Value)
    {
        if (states.ContainsKey(Key))
            states[Key] = Value;
        else
            states.Add(Key, Value);
    }

    public Dictionary<string, int> GetStates()
    {
        return states;
    }

    public void ModifyState(string Key, int Value)
    {
        if (states.ContainsKey(Key))
        {
            states[Key] += Value;
            if (states[Key] <= 0)
                RemoveState(Key);
        }
        else
            states.Add(Key, Value);
    }

}
