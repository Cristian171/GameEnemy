using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class AIProfile : MonoBehaviour 
{
    protected abstract float viewDistance { get; }
    protected abstract float waitForTurn { get; }
    protected abstract List<Vector3> waypoints { get; }
    public abstract void ExecuteProfile();
    public virtual void OnPlayerDetected() => Debug.Log("Player found");
    public abstract void DetectPlayer();
}