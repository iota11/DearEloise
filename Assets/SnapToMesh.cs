using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.HID;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SnapToMesh : MonoBehaviour
{
    public GameObject obj_snap;
    private Mesh mesh_snap;
    private Transform trans_snap;
    public LayerMask mask;
    Vector3 closestPoint;
    private void Start() {
        mesh_snap = obj_snap.GetComponent<MeshFilter>().mesh;
        trans_snap = obj_snap.GetComponent<Transform>();
    }
    void Update() {
        RaycastHit hit;
        
        if (!Physics.Raycast(transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), out hit, Mathf.Infinity, mask)) {
            transform.position = GetCloestPoint(transform.position);
            Debug.Log("fall down");
        }
        /*
        if(Physics.OverlapSphere(transform.position + new Vector3(0, -0.1f, 0), 0.2f, mask).Length<1) {
            transform.position = Vector3.Lerp(transform.position, GetCloestPoint(transform.position), 0.5f);

        }*/
    }
    private Vector3 GetCloestPoint(Vector3 location) {
        Vector3[] vertices = mesh_snap.vertices;
        float closestDistance = float.MaxValue;
        Vector3 closestPoint = Vector3.zero;
        for (int i = 0; i < vertices.Length; i++) {
            // Get the world space position of the vertex
            Vector3 vertexWorldPosition = trans_snap.TransformPoint(vertices[i]);

            // Calculate the distance between the vertex and the target location
            float distance = Vector3.Distance(vertexWorldPosition, location);

            // Check if this distance is closer than the previous closest distance
            if (distance < closestDistance) {
                closestDistance = distance;
                closestPoint = vertexWorldPosition;
            }
        }
        return closestPoint;

    }
    public void OnDrawGizmos() {
        Gizmos.DrawWireSphere(closestPoint, 0.1f);
    }
}
