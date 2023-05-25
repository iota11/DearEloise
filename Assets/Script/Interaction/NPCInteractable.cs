using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : IInteractable {

    [SerializeField] private string interactText;
    private GameObject UIContainer;

    private Button interactButton;
    private Animator animator;
    //private bool _isTriggered;
    //private NPCHeadLookAt npcHeadLookAt;

    private void Awake() {
        animator = GetComponent<Animator>();
        //UIContainer.SetActive(false);
        _isTriggered = false;
        GameObject cv = GameObject.Find("Canvas");
        UIContainer = cv.transform.Find("TalkUI").gameObject;
        //npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public override void Deactivate() {
       
                interactButton.onClick.RemoveAllListeners();
                UIContainer.SetActive(false);
                _isTriggered = false;
                playerTrans = null;

    }
    public override void InteractCustom(Transform interactorTransform) {
        UIContainer.gameObject.SetActive(true);
        interactButton = UIContainer.transform.Find("Button").gameObject.GetComponent<Button>();
        interactButton.onClick.AddListener(Talk);
    }

    public void Talk() {
        Debug.Log("talk talk talk");
    }

    public override string GetInteractText() {
        return interactText;
    }

    public override Transform GetTransform() {
        return transform;
    }

}