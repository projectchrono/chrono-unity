﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    public ChMaterialSurface CreateMaterial(ChContactMethod method)
    {
        if (method == ChContactMethod.NSC)
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

[CustomEditor(typeof(UChMaterialSurface))]
public class UChMaterialSurfaceEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var mat = target as UChMaterialSurface;
        string[] options = new string[] { "NSC", "SCM" };

        mat.contact_method = (ChContactMethod)EditorGUILayout.Popup("Contact Method", (int)mat.contact_method, options, EditorStyles.popup);

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
