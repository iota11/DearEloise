using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchInteractable : IInteractable {

    private GameObject UIContainer;
    [SerializeField] private CircuitSwitch _switch;
    private int _switchOption = 0;
    private int _optionNum = 1;
    private Button interactButton;
    private Animator animator;
    //private NPCHeadLookAt npcHeadLookAt;

    private void Awake() {
        animator = GetComponent<Animator>();
        _isTriggered = false;
        _optionNum = _switch.GetOpetionsNum();
        //assign UI
        GameObject cv = GameObject.Find("Canvas");
        UIContainer = cv.transform.Find("SwitchUI").gameObject;
    }

    public override void Deactivate() {
        interactButton.onClick?.RemoveAllListeners();
           UIContainer.SetActive(false);
                _isTriggered = false;
                playerTrans = null;
    }
    public override void InteractCustom(Transform interactorTransform) {
        //activate UI and button
            UIContainer.gameObject.SetActive(true);
            interactButton = UIContainer.transform.Find("Button").gameObject.GetComponent<Button>();
            interactButton.onClick.AddListener(TriggerSwitch);
    }
    public void DisableUI() {
        interactButton?.onClick?.RemoveAllListeners();
        UIContainer?.SetActive(false);
    }

    public override void SetElectricOn(CircuitEnd end) {
        _isElectricOn = true;
    }

    public override void SetElectricOff(CircuitEnd end) {
        _isElectricOn = false;
    }
    public void TriggerSwitch() {
        if ((_isElectric&& _isElectricOn) || !_isElectric) {
            _switchOption++;
            _switchOption %= _optionNum;
            _switch.SwitchNo = _switchOption;//apply switch
        }
    }

    public override string GetInteractText() {
        return null;
    }
    public override Transform GetTransform() {
        return transform;
    }

}