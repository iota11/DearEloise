using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CircuitWire : MonoBehaviour
{
    [SerializeField] List<CircuitEnd> endsList = new List<CircuitEnd>();

    List<CircuitEnd> inputEnds = new List<CircuitEnd>();
    public event Action OnWireTriggered;
    public event Action OnWireQuenched;
    private void OnEnable() {
        foreach(CircuitEnd end in endsList) {
            end.SetInWire(this);
            end.OnEndTriggered += SetOn;
            end.OnEndQuenched += SetOff;
        }
        Debug.Log("set");
    }

    public void SetOn(CircuitEnd newInputEnd) {
        Debug.Log("find an input end triggered");
        inputEnds.Add(newInputEnd);
        OnWireTriggered?.Invoke();
    }
    public void SetOff(CircuitEnd quenchedInputEnd) {
        Debug.Log("find a quenched end triggered");
        inputEnds.Remove(quenchedInputEnd);
        if (inputEnds.Count == 0) {
            OnWireQuenched?.Invoke();
        }
    }

    private void OnDrawGizmos() {
        
    }
}
