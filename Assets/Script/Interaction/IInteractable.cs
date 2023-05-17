using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractable: MonoBehaviour {
    protected bool _isTriggered = false;

    protected Transform playerTrans;
    public abstract void Interact(Transform interactorTransform);
    public abstract string GetInteractText();
    public abstract Transform GetTransform();
    public abstract void CheckTriggered();
    protected void Update() {
        CheckTriggered();
        
    }
}