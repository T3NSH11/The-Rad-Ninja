using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    public string ActionName = "Action";
    public float ActionCost = 1.0f;
    public GameObject Target;
    public GameObject TargetTag;
    public float duration = 0;
    public WorldState[] PreConditions;
    public WorldState[] AfterEffects;

    public Dictionary<string, int> Preconditions;
    public Dictionary<string, int> Effects;

    public WorldStates AgentStates;

    public bool Running = false;

    public EnemyAction()
    {
        Preconditions = new Dictionary<string, int>();
        Effects = new Dictionary<string, int>();
    }

    private void Awake()
    {
        if (PreConditions != null)
        {
            foreach (WorldState w in PreConditions)
            {
                Preconditions.Add(w.Key, w.Value);
            }
        }

        if (PreConditions != null)
        {
            foreach (WorldState w in AfterEffects)
            {
                Effects.Add(w.Key, w.Value);
            }
        }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> Conditions)
    {
        foreach (KeyValuePair<string, int> p in Preconditions)
        {
            if (!Conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
