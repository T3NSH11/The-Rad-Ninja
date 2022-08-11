using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> SubGoals;
    public bool Remove;

    public SubGoal(string s, int i, bool r)
    {
        SubGoals = new Dictionary<string, int>();
        SubGoals.Add(s, i);
        Remove = r;
    }
}

public class EnemyAgent : MonoBehaviour
{
    public List<EnemyAction> actions = new List<EnemyAction>();
    public Dictionary<SubGoal, int > goals = new Dictionary<SubGoal, int>();

    Planner planner;
    Queue<EnemyAction> ActionQueue;
    public EnemyAction CurrentAction;
    SubGoal CurrentSubGoal;
    void Start()
    {
        EnemyAction[] acts = this.GetComponents<EnemyAction>();

        foreach(EnemyAction a in acts)
        {
            actions.Add(a);
        }
    }

    void LateUpdate()
    {
        
    }
}
