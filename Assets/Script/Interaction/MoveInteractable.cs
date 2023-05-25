using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveInteractable : IInteractable {

    private GameObject UIContainer;
    private Button interactButton;
    private bool _isFollowing = false;
    private Animator animator;
    //private NPCHeadLookAt npcHeadLookAt;

    private void Awake() {
        animator = GetComponent<Animator>();
        _isTriggered = false;
        GameObject cv = GameObject.Find("Canvas");
        UIContainer = cv.transform.Find("MoveUI").gameObject;
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
            interactButton.onClick.AddListener(SetFollow);

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
    public void SetFollow() {
        _isFollowing = !_isFollowing;
        playerTrans.gameObject.GetComponent<PlayerInteract>().onHold = _isFollowing;

        if (_isFollowing) {
            Vector3 offset = (transform.position - playerTrans.position).normalized;
            StartCoroutine(FollowPlayer(offset));
        }
    }

    private IEnumerator FollowPlayer(Vector3 offset) {
        while (_isFollowing) {
            Debug.Log("following");
            transform.position = playerTrans.position + offset;
            yield return null;
        }
    }
    public override string GetInteractText() {
        return null;
    }
    public override Transform GetTransform() {
        return transform;
    }

}