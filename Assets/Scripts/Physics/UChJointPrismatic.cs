using UnityEngine;

public class UChJointPrismatic : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool enableLimits = false;
    public double minDisplacement = 0;
    public double maxDisplacement = 0;

    public double displacement = 0;  // current joint displacement

    private ChLinkLockPrismatic joint;

    public ChLinkLockPrismatic GetChJoint()
    {
        return joint;
    }

    void Start()
    {
        joint = new ChLinkLockPrismatic();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        if (enableLimits)
        {
            joint.GetLimit_Z().SetActive(true);
            joint.GetLimit_Z().SetMin(minDisplacement);
            joint.GetLimit_Z().SetMax(maxDisplacement);
        }

        UChSystem.chrono_system.AddLink(joint);
    }

    void Update()
    {
        var csys = joint.GetMarker1().GetAbsCoordsys();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);

        displacement = joint.GetMarker1().GetAbsCoordsys().pos.z - joint.GetMarker2().GetAbsCoordsys().pos.z;
        displacement = (int)(displacement * 1000.0f) / 1000.0f;
    }
}
