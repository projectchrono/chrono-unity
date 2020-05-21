using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchTester : MonoBehaviour
{
    public UChTerrainPatch patch;

    private Vector3 C;  // position of last ray intersection

    private void Start()
    {
        // Find the height and normal at the global origin
        float height;
        bool hit = patch.Project(Vector3.zero, out height, out C);
        Debug.Log("Hit: " + hit + "      height: " + height);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(C, 0.1f);
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }

}
