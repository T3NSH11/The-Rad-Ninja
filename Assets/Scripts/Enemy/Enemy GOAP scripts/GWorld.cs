using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld Instance = new GWorld();
    private static WorldStates World;

    static GWorld()
    {
        World = new WorldStates();
    }

    private GWorld()
    {
    }

    public static GWorld instance
    {
        get { return Instance; }
    }

    public WorldStates GetWorld()
    {
        return World;
    }
}
