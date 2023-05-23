using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteractable : IInteractable {

    [SerializeField] private Animator animator;
    [SerializeField] public bool auto = true;
    [SerializeField] private GameObject UIContainer;
    private Button interactButton;
    private bool _isOpen = false;

    private void Awake() {
        _isOpen = false;
        animator.SetBool("Open", _isOpen);
    }

    public override void CheckTriggered() {
        if (playerTrans) {
            //check if player leave the interation then close door and set trigger, UI and open back to false
            if (playerTrans.gameObject.GetComponent<PlayerInteract>().GetInteractable() != this) {
                interactButton?.onClick.RemoveAllListeners();
                UIContainer?.SetActive(false);
                animator.SetBool("Open", false);
                _isOpen = false;
                _isTriggered = false;
                playerTrans = null;
            }
        }
    }
    public void ToggleDoor() {
        _isOpen = !_isOpen;
        animator.SetBool("Open", _isOpen);
    }

    public override void Interact(Transform interactorTransform) {
        if (!_isTriggered) {
            _isTriggered = true;
            playerTrans = interactorTransform;
            if (auto) {
                animator.SetBool("Open", true);
                _isOpen = true;
            } else {
                UIContainer.gameObject.SetActive(true);
                interactButton = UIContainer.transform.Find("Button").gameObject.GetComponent<Button>();
                interactButton.onClick.AddListener(ToggleDoor);

            }
        }
    }

    
    public override string GetInteractText() {
        return "Open/Close Door";
    }

    public override Transform GetTransform() {
        return transform;
    }
}