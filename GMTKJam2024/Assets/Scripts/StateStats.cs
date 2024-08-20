using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStats
{
    public int sizeToNextState;
    public List<GameObject> obstaclesToSpawn;
    public string nextAnimationName;
    public int nextZoomOutAmount;
    public int objectToDestroy;

    public StateStats(int sizeToNextState, List<GameObject> obstaclesToSpawn, string nextAnimationName, int nextZoomOutAmount, int objectToDestroy)
    {
        this.sizeToNextState = sizeToNextState;
        this.obstaclesToSpawn = obstaclesToSpawn;
        this.nextAnimationName = nextAnimationName;
        this.nextZoomOutAmount = nextZoomOutAmount;
        this.objectToDestroy = objectToDestroy;
    }

}
