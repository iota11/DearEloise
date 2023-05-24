using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SetTarget : MonoBehaviour
{
    private bool onUI = false;
    public Vector3 posScreen;
    public Camera cam;
    private bool _isSet;
    [SerializeField] public GameObject target;
    public LayerMask mask;
    private void Awake() {
        cam = this.GetComponent<Camera>();
    }


    public void OnSetTarget(InputAction.CallbackContext context) {
        _isSet = context.started || context.performed;

        if (_isSet) {
            if (!onUI) {//if the click is not UI element
                RaycastHit hit;
                if (Physics.Raycast(cam.ScreenPointToRay(posScreen), out hit, Mathf.Infinity, mask)) {
                    Debug.Log("hit");
                    target.transform.position = hit.point;
                }
            }
            
        }
    }


    public void OnReadPos(InputAction.CallbackContext context) {
        Vector2 pos = context.ReadValue<Vector2>();
        //Debug.Log(pos);
        posScreen = new Vector3(pos.x, pos.y, 0);
       
    }

    public void Update() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            onUI = true;
        } else {
            onUI = false;
        }
    }
}
