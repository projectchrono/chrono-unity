using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChBody : MonoBehaviour
{
    // ATTENTION: The underlying ChBody must be created in UChBody.Awake.
    // Other components (e.g. links, motors, etc) access their ChBody references in their Start function and
    // Unity does not enforce an order of calls to Start.

    public bool isFixed;
    public bool collide;
    public bool showFrameGizmo;
    public float comRadiusGizmo;

    public bool automaticMass;
    public float density;
    public Vector3 COM;
    public double mass;
    public Vector3 inertiaMoments;
    public Vector3 inertiaProducts;

    public Vector3 linearVelocity;
    public Vector3 angularVelocity;

    protected ChBodyAuxRef body;

    public UChBody()
    {
        isFixed = false;
        collide = true;
        showFrameGizmo = false;
        comRadiusGizmo = 0.1f;
        automaticMass = false;
        density = 1000;
        mass = 1;
        inertiaMoments = Vector3.one;
        inertiaProducts = Vector3.zero;
    }

    public ChBodyAuxRef GetChBody()
    {
        return body;
    }

    public virtual void Create()
    {
        body = new ChBodyAuxRef();
    }

    public void Destroy()
    {
        body = null;
    }

    public virtual void AddToSystem()
    {
        UChSystem.chrono_system.AddBody(body);
    }

    public virtual void CalculateMassProperties() { }

    public virtual void OnDrawGizmosExtra() { }

    public void DebugInfo()
    {
        Debug.Log("Name:      " + gameObject.name + "\n" +
                  "Mass:      " + body.GetMass() + "\n" +
                  "COM:       " + Utils.FromChrono(body.GetFrame_COG_to_REF().GetPos()) + "\n" +
                  "inertiaXX: " + Utils.FromChrono(body.GetInertiaXX()) + "\n" +
                  "inertiaXY: " + Utils.FromChrono(body.GetInertiaXY()));
    }

    // ---  UNITY METHODS ---

    public void Awake()
    {
        ////Debug.Log("Body Awake()");

        Create();

        if (body == null)
            return;

        CalculateMassProperties();
        body.SetDensity(density);
        body.SetMass(mass);
        //// TODO: we should really set an entire frame here...
        body.SetFrame_COG_to_REF(new ChFrameD(Utils.ToChrono(COM)));
        body.SetInertiaXX(Utils.ToChrono(inertiaMoments));
        body.SetInertiaXY(Utils.ToChrono(inertiaProducts));

        body.SetBodyFixed(isFixed);
        body.SetCollide(collide);

        body.SetFrame_REF_to_abs(new ChFrameD(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation)));

        body.SetPos_dt(Utils.ToChrono(linearVelocity));
        body.SetWvel_loc(Utils.ToChrono(angularVelocity));

        ////DebugInfo();
    }

    void Start()
    {
        AddToSystem();
    }

    void Update()
    {
        ////Debug.Log("body Time = " + body.GetChTime());

        // Update body state
        var frame = body.GetFrame_REF_to_abs();
        transform.position = Utils.FromChrono(frame.GetPos());
        transform.rotation = Utils.FromChrono(frame.GetRot());
        linearVelocity = Utils.FromChrono(frame.GetPos_dt());
        angularVelocity = Utils.FromChrono(frame.GetWvel_loc());
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(1, 1, 1));

        if (showFrameGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Vector3.zero, new Vector3(2, 0, 0));
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Vector3.zero, new Vector3(0, 2, 0));
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Vector3.zero, new Vector3(0, 0, 2));

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(COM, comRadiusGizmo);
        }

        OnDrawGizmosExtra();
    }
}

// ==========================================================================================================

[CustomEditor(typeof(UChBody))]
public class UChBodyEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChBody body = (UChBody)target;

        body.isFixed = EditorGUILayout.Toggle("Fixed", body.isFixed);
        body.collide = EditorGUILayout.Toggle("Collide", body.collide);
        body.showFrameGizmo = EditorGUILayout.Toggle("Show Frame Gizmo", body.showFrameGizmo);
        body.comRadiusGizmo = EditorGUILayout.FloatField("COM Radius Gizmo", body.comRadiusGizmo);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        body.automaticMass = EditorGUILayout.Toggle("Automatic Mass/Inertia", body.automaticMass);

        EditorGUI.indentLevel++;
        if (body.automaticMass)
        {
            body.density = EditorGUILayout.FloatField("Density", body.density);
        }
        else
        {
            body.CalculateMassProperties();
            body.mass = EditorGUILayout.DoubleField("Mass", body.mass);
            body.COM = EditorGUILayout.Vector3Field("Center of Mass", body.COM);
            body.inertiaMoments = EditorGUILayout.Vector3Field("Moments of Inertia", body.inertiaMoments);
            body.inertiaProducts = EditorGUILayout.Vector3Field("Products of Inertia", body.inertiaProducts);
        }
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        body.linearVelocity = EditorGUILayout.Vector3Field("Linear Velocity", body.linearVelocity);
        body.angularVelocity = EditorGUILayout.Vector3Field("Angular Velocity", body.angularVelocity);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
