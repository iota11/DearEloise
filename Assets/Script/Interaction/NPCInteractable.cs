using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : IInteractable {

    [SerializeField] private string interactText;
    [SerializeField] private GameObject UIContainer;

    private Button interactButton;
    private Animator animator;
    private bool _isTriggered;
    //private NPCHeadLookAt npcHeadLookAt;

    private void Awake() {
        animator = GetComponent<Animator>();
        UIContainer.SetActive(false);
        _isTriggered = false;
        //npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public override void CheckTriggered() {
        if (playerTrans) {
            if (playerTrans.gameObject.GetComponent<PlayerInteract>().GetInteractable() != this) {
                UIContainer.SetActive(false);
                _isTriggered = false;
            }
        }
    }
    public override void Interact(Transform interactorTransform) {
        //ChatBubble3D.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), ChatBubble3D.IconType.Happy, "Hello there!");

        //animator.SetTrigger("Talk");
        //Debug.Log(interactText);
        //float playerHeight = 1.7f;
        //npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
        _isTriggered = true;
        UIContainer.gameObject.SetActive(true);
        interactButton = UIContainer.transform.Find("Button").gameObject.GetComponent<Button>();
        interactButton.onClick.AddListener(Talk);
        playerTrans = interactorTransform;
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