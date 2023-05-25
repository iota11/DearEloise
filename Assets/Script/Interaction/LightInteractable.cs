using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightInteractable : IInteractable {

    private GameObject UIContainer;
    [SerializeField] private Light light;
    private bool _isOn = false;
    private Button interactButton;
    private Animator animator;
    //private NPCHeadLookAt npcHeadLookAt;

    private void Awake() {
        animator = GetComponent<Animator>();
        //UIContainer.SetActive(false);
        light.intensity = 0;
        _isTriggered = false;
        GameObject cv = GameObject.Find("Canvas");
        UIContainer = cv.transform.Find("TriggerUI").gameObject;
    }

    public override void Deactivate() {
        interactButton.onClick?.RemoveAllListeners();
           UIContainer.SetActive(false);
                _isTriggered = false;
                playerTrans = null;
    }
    public override void InteractCustom(Transform interactorTransform) {
            UIContainer.gameObject.SetActive(true);
            interactButton = UIContainer.transform.Find("Button").gameObject.GetComponent<Button>();
            interactButton.onClick.AddListener(TriggerLight);
            if (_isOn) {
                light.intensity = 5;
            }
    }
    public void DisableUI() {
        interactButton?.onClick?.RemoveAllListeners();
        UIContainer?.SetActive(false);
    }

    public override void SetElectricOn(CircuitEnd end) {
        _isElectricOn = true;
        if (_isOn) {
            light.intensity = 5;
        }
    }

    public override void SetElectricOff(CircuitEnd end) {
        _isElectricOn = false;
        light.intensity = 0;
        //switch is still on
    }
    public void TriggerLight() {
        if ((_isElectricOn&&_isElectric) || !_isElectric) {
            _isOn = !_isOn;
            if (_isOn) {
                light.intensity = 5;
            } else {
                light.intensity = 0;
            }
        }
    }

    public override string GetInteractText() {
        return null;
    }
    public override Transform GetTransform() {
        return transform;
    }

}