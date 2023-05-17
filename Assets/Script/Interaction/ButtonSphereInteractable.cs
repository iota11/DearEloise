using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSphereInteractable : IInteractable {


    [SerializeField] private MeshRenderer sphereMeshRenderer;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material yellowMaterial;

    private bool isColorYellow;

    private void SetColorBlue() {
        sphereMeshRenderer.material = blueMaterial;
    }

    private void SetColorYellow() {
        sphereMeshRenderer.material = yellowMaterial;
    }

    private void ToggleColor() {
        isColorYellow = !isColorYellow;
        if (isColorYellow) {
            SetColorYellow();
        } else {
            SetColorBlue();
        }
    }

    public void PushButton() {
        ToggleColor();
    }

    public override void Interact(Transform interactorTransform) {
        PushButton();
    }

    public override void CheckTriggered() {

    }

    public override string GetInteractText() {
        return "Push button";
    }

    public override Transform GetTransform() {
        return transform;
    }

}