using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitEnd : MonoBehaviour
{
    public event Action<CircuitEnd> OnEndTriggered;
    public event Action<CircuitEnd> OnEndQuenched;
    public bool _isTriggered = false;
    public bool _isInput = false;
    private Material mat;
    private void OnEnable() {
        mat=GetComponent<Renderer>().material;
    }
    private void TriggeredByWire() {
        _isTriggered = true;
        mat.color = Color.yellow;
    }

    private void QuenchedByWire() {
        _isTriggered = false;
        mat.color = Color.white;

    }
    private void SetEndOn() {
        Debug.Log("collide");
        _isInput = true;
        OnEndTriggered.Invoke(this); 
    }

    private void SetEndFalse() {
        _isInput = false;
        OnEndQuenched.Invoke(this); 
    }

    public void SetInWire(CircuitWire wire) {
        wire.OnWireTriggered += TriggeredByWire;
        wire.OnWireQuenched += QuenchedByWire;
    }



    //test
    private void OnTriggerEnter(Collider other) {
        SetEndOn();
    }

    private void OnTriggerExit(Collider other) {
        SetEndFalse();
    }

}
