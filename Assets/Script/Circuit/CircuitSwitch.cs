using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class CircuitSwitch : CircuitWire
{
    [SerializeField]private int switchNo = 0;
    public int test;
    public event Action OnSwitchChange;
    public int SwitchNo {
        get { return switchNo; }
        set {
            if (switchNo != value) {
                switchNo = value;
                Debug.Log("change to " + switchNo);
                OnSwitchChange?.Invoke();
            }
            switchNo = value;
        }
    }
    public List<CircuitEnd> _options = new List<CircuitEnd>();
    public override void SetUp() { 
        foreach (CircuitEnd end in endsList) {
            end.SetInWire(this);
            if (end._isInput) {
                end.OnEndTriggered += SetOn;   //listen to input trigger
                end.OnEndQuenched += SetOff;
                inputEndsList.Add(end);
            } else {
               // _options.Add(end);
            }
        }
        SetValidEnd();
        OnSwitchChange += SetValidEnd;
    }

    public int GetOpetionsNum() {
        return _options.Count;
    }
    private void SetValidEnd() {
        for (int i = 0; i < _options.Count; i++) {
            CircuitEnd end = _options[i];
            if(i == SwitchNo) {
                end._IsTriggerable = true;
            } else {
                end._IsTriggerable = false ;
            }
        }
        Refresh();
    }
}
