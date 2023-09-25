using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {
    public float moveSpeed = 5f;
    public InputAction playerControls;
    public  Camera playerCamera;
    Vector3 moveDirection =  Vector3.zero;
    private void Awake() {
        //playerCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }
    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable() {
        playerControls.Disable();    }
    private void Update() {
        Vector2 RawDirection = playerControls.ReadValue<Vector2>();
        Vector3 Cam_left= Vector3.Cross(playerCamera.transform.forward, Vector3.up).normalized;
        Vector3 Cam_forward = Vector3.Cross(Cam_left, Vector3.up).normalized;
        moveDirection =- RawDirection.y* Cam_forward  -RawDirection.x * Cam_left;
    }
    private void FixedUpdate() {
        transform.position = transform.position+moveSpeed * moveDirection * Time.deltaTime;
    }

}
