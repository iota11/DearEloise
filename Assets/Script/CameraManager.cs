using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    private Vector2 _delta;
    private Vector2 _deltaZoom;

    private bool _isMoving;
    private bool _isRotating;
    private float _xRotation;
    private GameObject cam;
    private float zoomLerp = 0;
    [SerializeField] public Vector2 rangeZoom = new Vector2(3, 100);
    [SerializeField] public Vector2 rangeRotateX = new Vector2(3, 100);
    [SerializeField] public GameObject target;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float zoomSpeed = 10.0f;


    private void Awake() {
        _xRotation = transform.rotation.eulerAngles.x;
        cam = transform.Find("Main Camera").gameObject;

    }
    public void OnLook(InputAction.CallbackContext context) {
        _delta = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context) {
        _deltaZoom = context.ReadValue<Vector2>();
    }


    public void OnMove(InputAction.CallbackContext context) {
        _isMoving = context.started || context.performed;

    }

    public void OnRotate(InputAction.CallbackContext context) {
        _isRotating = context.started || context.performed;
       
    }

    IEnumerator ReCenter() {
        while(Vector3.Distance(transform.position, target.transform.position)> 0.1f) {
            transform.position = Vector3.Lerp( transform.position, target.transform.position, Time.deltaTime);
            yield return null;
        }
        transform.position = target.transform.position;
    }
    private void LateUpdate() {
        if (_isMoving) {
            var position = transform.right * (_delta.x * -movementSpeed);
            position += transform.up * (_delta.y * -movementSpeed);
            transform.position += position * Time.deltaTime;
        }

        if (_isRotating) {
            if (transform.position != target.transform.position) {
                StartCoroutine(ReCenter());
            }
            transform.Rotate(new Vector3(-_delta.y * rotationSpeed, _delta.x * rotationSpeed, 0.0f));
            transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x, rangeRotateX.x, rangeRotateX.y), transform.rotation.eulerAngles.y, 0.0f);
        }

        //Zoom
        Vector3 dirZoom = (cam.transform.position - transform.position).normalized;
        float dis = Vector3.Distance(cam.transform.position, transform.position);
        if(_deltaZoom.y > 1&& dis<rangeZoom.y) {
            cam.transform.position += dirZoom * zoomSpeed * Time.deltaTime;
        }else if(_deltaZoom.y < -1 && dis > rangeZoom.x) {
            cam.transform.position -= dirZoom * zoomSpeed * Time.deltaTime;
        }
    }
}