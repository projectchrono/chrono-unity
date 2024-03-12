using UnityEngine;

//// TODO:  Double-check check proper implementation of joint limits.
////        Must take into account body order, orientation, etc.


// Physics items will cause Unity to crash if trying to add bodies to the system before any of those
// have intialised first. Therefore, force the execution to be after these (default execution order is '0')
[DefaultExecutionOrder(100)]
public class UChJointRevolute : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool enableLimits = false;
    public double minAngle = 0;
    public double maxAngle = 0;

    public double angle;  // current joint relative angle

    private ChLinkLockRevolute joint;

    public ChLinkLockRevolute GetChJoint()
    {
        return joint;
    }

    void Start()
    {
        joint = new ChLinkLockRevolute();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        
        if (enableLimits)
        {
            joint.GetLimit_Rz().SetActive(true);
            joint.GetLimit_Rz().SetMin(minAngle * Mathf.PI / 180);
            joint.GetLimit_Rz().SetMax(maxAngle * Mathf.PI / 180);
        }
        
        if (joint != null) {
            ////Debug.Log("Joint is functional " + joint.GetName());
            UChSystem.chrono_system.AddLink(joint);
        }
    }

    void Update()
    {
        
        var csys = joint.GetMarker1().GetAbsCoordsys();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);

        angle = joint.GetRelAngle() * (180 / Mathf.PI);
        angle = (int)(angle * 100.0f) / 100.0f;
        
    }
}
