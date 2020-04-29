using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ContactMethod
{
    NSC = 0,  //< non-smooth, constraint-based (a.k.a. rigid-body) contact
    SMC = 1   //< smooth, penalty-based (a.k.a. soft-body) contact
};

[System.Serializable]
public class MaterialInfo
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

    public MaterialInfo() {}

    public ChMaterialSurface CreateMaterial(ContactMethod method)
    {
        if (method == ContactMethod.NSC)
        {
            var matNSC = new ChMaterialSurfaceNSC();
            matNSC.SetFriction(mu);
            matNSC.SetRollingFriction(mu_r);
            matNSC.SetSpinningFriction(mu_s);
            matNSC.SetRestitution(cr);
            matNSC.SetCohesion(coh);
            return matNSC;

        }

        var matSMC = new ChMaterialSurfaceSMC();
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
    public ContactMethod contact_method;
    public MaterialInfo mat_info;

    public UChMaterialSurface()
    {
        mat_info = new MaterialInfo();
        mat_info.mu = 0.4f;
    }

    public void DumpInfo()
    {
        Debug.Log("friction: " + mat_info.mu);
        switch (contact_method)
        {
            case ContactMethod.NSC:
                Debug.Log("NSC material\nCohesion: " + mat_info.coh);
                break;
            case ContactMethod.SMC:
                Debug.Log("SMC material\nYoung: " + mat_info.Y);
                break;
        }
    }
}

[CustomEditor(typeof(UChMaterialSurface))]
public class UChMaterialSurfaceEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var mat = target as UChMaterialSurface;
        string[] options = new string[] { "NSC", "SCM" };

        mat.contact_method = (ContactMethod)EditorGUILayout.Popup("Field type:", (int)mat.contact_method, options, EditorStyles.popup);

        mat.mat_info.mu = EditorGUILayout.FloatField("Friction", mat.mat_info.mu);
        mat.mat_info.mu_r = EditorGUILayout.FloatField("Rolling Friction", mat.mat_info.mu_r);
        mat.mat_info.mu_s = EditorGUILayout.FloatField("Spinning Friction", mat.mat_info.mu_s);
        mat.mat_info.cr = EditorGUILayout.FloatField("Restitution", mat.mat_info.cr);
        mat.mat_info.coh = EditorGUILayout.FloatField("Cohesion", mat.mat_info.coh);

        using (new EditorGUI.DisabledScope(mat.contact_method == 0))
        {
            mat.mat_info.Y = EditorGUILayout.FloatField("Young Modulus", mat.mat_info.Y);
            mat.mat_info.nu = EditorGUILayout.FloatField("Poisson Ratio", mat.mat_info.nu);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(mat);
        }
    }
}
