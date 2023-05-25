using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {
    private IInteractable interactable;
    public bool onHold = false;

    private IInteractable Interactable {
        get { return interactable; }
        set {
            //Debug.Log("checking");
            if (value != interactable) {
                interactable?.Deactivate();
                interactable = value;
                if (interactable) {
                    Debug.Log("interact with " + interactable?.name);
                    interactable.Interact(transform);
                }
            }
        }
     }
    private void Update() {
        if (!onHold) {
            Interactable = GetInteractableObject();
        }
        /*
            if (interactable != null) {
                interactable.Interact(transform);
            }*/
    }

    public IInteractable GetInteractable() {
        return interactable;
    }

    public IInteractable GetInteractableObject() {
        List<IInteractable> interactableList = new List<IInteractable>();
        float interactRange = 3f;
        LayerMask msk = LayerMask.GetMask("Interactable");
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, msk );
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent(out IInteractable interactable)) {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList) {
            if (closestInteractable == null) {
                closestInteractable = interactable;
            } else {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) < 
                    Vector3.Distance(transform.position, closestInteractable.GetTransform().position)) {
                    // Closer
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }

}