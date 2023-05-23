using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightInteractable : IInteractable {

    [SerializeField] private GameObject UIContainer;
    [SerializeField] private Light light;
    private bool _isOn = false;
    private Button interactButton;
    private Animator animator;
    //private NPCHeadLookAt npcHeadLookAt;

    private void Awake() {
        animator = GetComponent<Animator>();
        UIContainer.SetActive(false);
        light.intensity = 0;
        _isTriggered = false;
    }

    public override void CheckTriggered() {
        if (playerTrans) {
            if (playerTrans.gameObject.GetComponent<PlayerInteract>().GetInteractable() != this) {
                interactButton.onClick.RemoveAllListeners();
                UIContainer.SetActive(false);
                _isTriggered = false;
                playerTrans = null;

            }
        }
    }
    public override void Interact(Transform interactorTransform) {
        if (!_isTriggered) {
            _isTriggered = true;
            UIContainer.gameObject.SetActive(true);
            interactButton = UIContainer.transform.Find("Button").gameObject.GetComponent<Button>();
            interactButton.onClick.AddListener(TriggerLight);
            playerTrans = interactorTransform;
        }
    }

    public void TriggerLight() {
        _isOn = !_isOn;
        if (_isOn) {
            light.intensity = 5;
        } else {
            light.intensity = 0;
        }
    }

    public override string GetInteractText() {
        return null;
    }
    public override Transform GetTransform() {
        return transform;
    }

}