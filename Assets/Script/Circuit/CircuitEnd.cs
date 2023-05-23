using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CircuitEnd : MonoBehaviour
{
    public event Action<CircuitEnd> OnEndTriggered;
    public event Action<CircuitEnd> OnEndQuenched;
    public bool _isTriggered = false;
    public bool _isInput = false;
    private bool _isTriggerable = true;
    private CircuitEnd _parent;
    private Material mat;

    public bool _IsTriggerable {
        get { return _isTriggerable; }
        set { 
            _isTriggerable = value;

            if (value == false) {
                Debug.Log("make it false");
                //QuenchedByWire();
            }
        }
    }
    private void OnEnable() {
        mat=GetComponent<Renderer>().material;
    }
    private void TriggeredByWire() {
        if (!_isInput && _isTriggerable) {
            _isTriggered = true;
            mat.color = Color.yellow;
            OnEndTriggered?.Invoke(this);
        } else {
            _isTriggered = false;
            mat.color = Color.white;
            OnEndQuenched?.Invoke(this);
        }
    }

    private void QuenchedByWire() {
        if (!_isInput) {
            _isTriggered = false;
            mat.color = Color.white;
            OnEndQuenched?.Invoke(this);
        }
    }
    private void SetEndOn(CircuitEnd parent) {
        Debug.Log("collide");
        _isTriggered = true;
        _parent = parent;
        OnEndTriggered?.Invoke(this); 
    }

    private void SetEndFalse(CircuitEnd parent) {
        _parent = null;
        _isTriggered = false;
        OnEndQuenched?.Invoke(this); 
    }

    public void SetInWire(CircuitWire wire) {
        if (!_isInput) {
            wire.OnWireTriggered += TriggeredByWire;
            wire.OnWireQuenched += QuenchedByWire;
        }
    }
    public void RemoveFromWire(CircuitWire wire) {
        if (!_isInput) {
            wire.OnWireTriggered -= TriggeredByWire;
            wire.OnWireQuenched -= QuenchedByWire;
            QuenchedByWire();
        }
    }


    //test
    private void OnTriggerEnter(Collider other) {
        Debug.Log("adfadfa");
        if (other.gameObject.GetComponent<CircuitEnd>()) {
            CircuitEnd otherCE = (CircuitEnd)other.gameObject.GetComponent<CircuitEnd>();

            if (_isInput && !otherCE._isInput) {//plug in
            
                if (otherCE._isTriggered) SetEndOn(otherCE);
                otherCE.OnEndTriggered += SetEndOn; //listen to another output
                otherCE.OnEndQuenched += SetEndFalse;
                Debug.Log("listen to " + otherCE.name);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<CircuitEnd>()) {
            CircuitEnd otherCE = (CircuitEnd)other.gameObject.GetComponent<CircuitEnd>();
            if (_isInput && !otherCE._isInput) {
                SetEndFalse(otherCE);
                otherCE.OnEndTriggered -= SetEndOn;
                otherCE.OnEndQuenched -= SetEndFalse;
                Debug.Log("un listen to " + otherCE.name);
            }
        }
    }
    
}
