using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChBody : MonoBehaviour
{
    // ATTENTION: The underlying ChBody must be created in UChBody.Awake.
    // Other components (e.g. links, motors, etc) access their ChBody references in their Start function and
    // Unity does not enforce an order of calls to Start.

    public bool isFixed = false;
    public bool collide = true;
    public bool showFrameGizmo = false;
    public Vector3 linearVelocity;
    public Vector3 angularVelocity;

    protected ChBody body;

    public ChBody GetChBody()
    {
        return body;
    }

    public virtual void Create()
    {
        body = new ChBody();
    }

    public void Destroy()
    {
        body = null;
    }

    public virtual void AddToSystem()
    {
        UChSystem.chrono_system.AddBody(body);
    }

    public virtual void OnDrawGizmosExtra() { }

    // ---  UNITY METHODS ---

    public void Awake()
    {
        ////Debug.Log("Body Awake()");

        Create();

        if (body == null)
            return;

        body.SetBodyFixed(isFixed);
        body.SetCollide(collide);
        body.SetPos(Utils.ToChrono(transform.position));
        body.SetRot(Utils.ToChrono(transform.rotation));
        body.SetPos_dt(Utils.ToChrono(linearVelocity));
        body.SetWvel_loc(Utils.ToChrono(angularVelocity));
    }

    void Start()
    {
        AddToSystem();
    }

    void FixedUpdate()
    {
        ////Debug.Log("body Time = " + body.GetChTime());

        // Update state in inspector
        transform.position = Utils.FromChrono(body.GetPos());
        transform.rotation = Utils.FromChrono(body.GetRot());
        linearVelocity = Utils.FromChrono(body.GetPos_dt());
        angularVelocity = Utils.FromChrono(body.GetWvel_loc());
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
        }

        OnDrawGizmosExtra();
    }
}
