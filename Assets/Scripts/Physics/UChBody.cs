using UnityEngine;
// Ensure this base script executes just after the Chsystem, but before everything else.
[DefaultExecutionOrder(-999)]
public class UChBody : MonoBehaviour
{
    // ATTENTION: The underlying ChBody must be created in UChBody.Awake.
    // Other components (e.g. links, motors, etc) access their ChBody references in their Start function and
    // Unity does not enforce an order of calls to Start.

    // This is mitigated by adjusting the script execution order to -999 (currently set to +100 in motors and links scripts)


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

    protected bool isBodyInitialised = false;  // Flag used for checking if exists prior to accessing by other components


    public UChBody()
    {
        isFixed = false;
        collide = true;
        showFrameGizmo = false;
        comRadiusGizmo = 0.1f;
        automaticMass = false;
        // density = 1000; // obsolete? double check
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
        // Avoid crash if adding object that's not been instantised
        if (body == null)
        {
            Debug.Log("No body to add to system (" + gameObject.name + ")");
        }
        else
        {
            UChSystem.chrono_system.AddBody(body);
        }
        DebugInfo();
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

    public virtual void InstanceCreation() // Changed from Awake, added virtual
    {
        ///Debug.Log("Body Awake()");
        
        Create();

        if (body == null)   
            return;

        CalculateMassProperties();
        // body.SetDensity(density); // - redundant if Mass COG in new collision method? Need to do more research on it.
        body.SetMass(mass);
        //// TODO: we should really set an entire frame here...
        body.SetFrame_COG_to_REF(new ChFramed(Utils.ToChrono(COM)));
        body.SetInertiaXX(Utils.ToChrono(inertiaMoments));
        body.SetInertiaXY(Utils.ToChrono(inertiaProducts));

        body.SetBodyFixed(isFixed);
        body.SetCollide(collide);

        body.SetFrame_REF_to_abs(new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation)));

        body.SetPosDer(Utils.ToChrono(linearVelocity));
        body.SetAngVelLocal(Utils.ToChrono(angularVelocity));


        ////DebugInfo();
        if (UChSystem.chrono_system != null)
        { AddToSystem(); }
    }

    // ---  UNITY METHODS ---
    void Awake()
    {
        InstanceCreation();
        //isBodyInitialised = true; // Flag for creation of body
    }


    public void Start()
    {
        // Do nothing.
    }

    void Update()
    {
        ////Debug.Log("body Time = " + body.GetChTime());

        // Update body state
        var frame = body.GetFrame_REF_to_abs();
        transform.position = Utils.FromChrono(frame.GetPos());
        transform.rotation = Utils.FromChrono(frame.GetRot());
        linearVelocity = Utils.FromChrono(frame.GetPosDer());
        angularVelocity = Utils.FromChrono(frame.GetAngVelLocal());
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
