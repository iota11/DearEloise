using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public abstract class IInteractable: MonoBehaviour {
    protected bool _isTriggered = false;

    protected Transform playerTrans;
    protected bool _isElectricOn = false;

    public bool _isElectric = true;
    [HideInInspector]
    public CircuitEnd InputEnd;
    public void Interact(Transform interactorTransform) {
        if (!_isTriggered) {
            _isTriggered = true;
            playerTrans = interactorTransform;
            InteractCustom(interactorTransform);
        }
    }
    public abstract void InteractCustom(Transform interactorTransform);
    public abstract string GetInteractText();
    public abstract Transform GetTransform();
    public abstract void Deactivate();
    protected void OnEnable() {
        if (_isElectric) {
            InputEnd.OnEndTriggered += SetElectricOn;
            InputEnd.OnEndQuenched += SetElectricOff;
        }
    }

    public virtual void SetElectricOn(CircuitEnd end) {
        _isElectricOn = true;

    }
    public virtual void SetElectricOff(CircuitEnd end) {
        _isElectricOn = false;
        
    }

    public void OnDrawGizmos() {
        
    }

}


[CustomEditor(typeof(IInteractable))]
public class IInteractablet_Editor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        IInteractable script = (IInteractable)target;

        // draw checkbox for the bool
        //script._isElectric = EditorGUILayout.Toggle("is electtic", script._isElectric);
        if (script._isElectric) // if bool is true, show other fields
        {
            script.InputEnd = EditorGUILayout.ObjectField("input end", script.InputEnd, typeof(CircuitEnd), true) as CircuitEnd;
        }
        OnCustomEditor();
    }
    virtual public void OnCustomEditor() {
        Debug.Log("should not called");
    }
}
