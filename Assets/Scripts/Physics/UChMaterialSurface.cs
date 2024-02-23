using UnityEngine;

[System.Serializable]
public class UChMaterialInfo
{
    public float mu;   // coefficient of friction
    public float mu_r; // rolling friction
    public float mu_s; // spinning friction
    public float cr;   // coefficient of restitution
    public float coh;  // constant cohesion/adhesion
    public float Y;    // Young's modulus
    public float nu;   // Poisson ratio
    ////public float kn;  // normal stiffness
    ////public float gn;  // normal viscous damping
    ////public float kt;  // tangential stiffness
    ////public float gt;  // tangential viscous damping

    public UChMaterialInfo() {}

    public ChContactMaterial CreateMaterial(ChContactMethod method)
    {
        if (method == ChContactMethod.NSC)
        {
            var matNSC = new ChContactMaterialNSC();
            matNSC.SetFriction(mu);
            matNSC.SetRollingFriction(mu_r);
            matNSC.SetSpinningFriction(mu_s);
            matNSC.SetRestitution(cr);
            matNSC.SetCohesion(coh);
            return matNSC;

        }

        var matSMC = new ChContactMaterialSMC();
        matSMC.SetFriction(mu);
        matSMC.SetRollingFriction(mu_r);
        matSMC.SetSpinningFriction(mu_s);
        matSMC.SetRestitution(cr);
        matSMC.SetAdhesion(coh);
        matSMC.SetYoungModulus(Y);
        matSMC.SetPoissonRatio(nu);
        ////matSMC.SetKn(kn);
        ////matSMC.SetGn(gn);
        ////matSMC.SetKt(kt);
        ////matSMC.SetGt(gt);
        return matSMC;
    }
}

public class UChMaterialSurface : MonoBehaviour
{
    public ChContactMethod contact_method;
    public UChMaterialInfo mat_info;

    public UChMaterialSurface()
    {
        mat_info = new UChMaterialInfo();
        mat_info.mu = 0.4f;
    }

    public void DebugInfo()
    {
        Debug.Log("friction: " + mat_info.mu);
        switch (contact_method)
        {
            case ChContactMethod.NSC:
                Debug.Log("NSC material\nCohesion: " + mat_info.coh);
                break;
            case ChContactMethod.SMC:
                Debug.Log("SMC material\nYoung: " + mat_info.Y);
                break;
        }
    }
}
