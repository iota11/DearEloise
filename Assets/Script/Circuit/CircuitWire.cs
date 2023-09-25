using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CircuitWire : MonoBehaviour
{
    [SerializeField] public List<CircuitEnd> endsList = new List<CircuitEnd>();
    [SerializeField] public bool _isOn = false;
    [HideInInspector]
    public List<CircuitEnd> inputEndsList = new List<CircuitEnd>();
    public event Action OnWireTriggered;
    public event Action OnWireQuenched;
    /*
    public void Update() {
        foreach (CircuitEnd end in endsList) {
            Vector3 pos = end.gameObject.transform.position;
            Debug.DrawLine(transform.position, pos, Color.red, Time.deltaTime*2);

        }
    }*/

    virtual public void SetUp() {
        foreach (CircuitEnd end in endsList) {
            end.SetInWire(this); //make wire listen to the wire
            if (end._isInput) {
                end.OnEndTriggered += SetOn;   //listen to input trigger
                end.OnEndQuenched += SetOff;
                inputEndsList.Add(end);
            }
        }
        Debug.Log("set");
        
    }
    private void OnEnable() {
        SetUp();
    }

    public void Refresh() {
        if (_isOn) {
            OnWireTriggered?.Invoke();
            _isOn = true;
        }
    }
    //when a input end trigger the whole wire, set the whole net on
    public void SetOn(CircuitEnd newInputEnd) {
        Debug.Log("find an input end triggered");
        OnWireTriggered?.Invoke();
        _isOn = true;
    }

    //when an input end quench, check if there is other end triggered. if not set the whole net off
    public void SetOff(CircuitEnd quenchedInputEnd) {
        Debug.Log("find a quenched end triggered");
        bool _existOn = false;
        foreach (CircuitEnd ce in inputEndsList) {
            if (ce._isTriggered) {
                _existOn = true;
            }
        }
        if (!_existOn) {
            OnWireQuenched?.Invoke();
            _isOn = false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = _isOn? Color.yellow: Color.black; 
        foreach (CircuitEnd end in endsList) {
            Vector3 pos = end.gameObject.transform.position;
            Gizmos.DrawLine(transform.position, pos);
        }

    }
}
