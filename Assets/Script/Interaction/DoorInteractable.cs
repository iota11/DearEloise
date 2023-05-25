using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteractable : IInteractable {

    [SerializeField] private Animator animator;
    [SerializeField] public bool auto = true;
    private bool _isOpen = false;
    [HideInInspector]
    [SerializeField] public GameObject UIContainer;
    private Button interactButton;


    private void Awake() {
        _isOpen = false;
        animator.SetBool("Open", _isOpen);
        GameObject cv = GameObject.Find("Canvas");
        UIContainer = cv.transform.Find("TriggerUI").gameObject;
    }

    public override void Deactivate() {
        //check if player leave the interation then close door and set trigger, UI and open back to false

        interactButton?.onClick.RemoveAllListeners();
        UIContainer?.SetActive(false);
        animator.SetBool("Open", false);
        _isOpen = false;
        _isTriggered = false;
        playerTrans = null;
        
    }


    public void ToggleDoor() {
        if (_isElectricOn) {
            _isOpen = !_isOpen;
            animator.SetBool("Open", _isOpen);
            Debug.Log("is open is " + _isOpen);
        }
    }

    public override void InteractCustom(Transform interactorTransform) {
            if (auto) {
                animator.SetBool("Open", true);
                _isOpen = true;
            } else {
                UIContainer.SetActive(true);
                interactButton = UIContainer.transform.Find("Button").gameObject.GetComponent<Button>();
                interactButton.onClick.AddListener(ToggleDoor);
                Debug.Log("active button");
            }
    }
    public override void SetElectricOff(CircuitEnd end) {
        _isElectricOn = false;
        //UIContainer.SetActive(false);
        animator.SetBool("Open", false);
        _isOpen = false;
    }

    public override void SetElectricOn(CircuitEnd end) {
        _isElectricOn = true;


    }

    public override string GetInteractText() {
        return "Open/Close Door";
    }

    public override Transform GetTransform() {
        return transform;
    }
}

[CustomEditor(typeof(DoorInteractable))]
public class DoorInteractable_Editor : IInteractablet_Editor
{
    public override void OnCustomEditor() {
        DoorInteractable script = (DoorInteractable)target;

        if (!script.auto) // if bool is true, show other fields
        {
            script.UIContainer = EditorGUILayout.ObjectField("UI Container", script.UIContainer, typeof(GameObject), true) as GameObject;
        }
    }
}