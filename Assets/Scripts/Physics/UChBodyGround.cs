using UnityEngine;

public class UChBodyGround : UChBody
{
    public UChBodyGround()
    {
        isFixed = true;
        collide = false;
        linearVelocity = Vector3.zero;
        angularVelocity = Vector3.zero;
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}
