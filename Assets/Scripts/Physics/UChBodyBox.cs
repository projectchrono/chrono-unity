using System;
using UnityEngine;

[DefaultExecutionOrder(50)]
public class UChBodyBox : UChBody
{
    private ChContactMaterial mat;

    public UChBodyBox()
    {
        automaticMass = true;
    }

    public override void CalculateMassProperties()
    {
        var size = transform.localScale;
        mass = density * (size.x * size.y * size.z);
        inertiaMoments.x = (float)((1.0 / 12.0) * mass * (Math.Pow(size.y, 2) + Math.Pow(size.z, 2)));
        inertiaMoments.y = (float)((1.0 / 12.0) * mass * (Math.Pow(size.x, 2) + Math.Pow(size.z, 2)));
        inertiaMoments.z = (float)((1.0 / 12.0) * mass * (Math.Pow(size.x, 2) + Math.Pow(size.y, 2)));
    }

    public override void Create()
    {
        // Get a handle to the associated material component and create the Chrono material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DebugInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body and its collision shape
        var size = transform.localScale;
        body = new ChBodyAuxRef();   // Rigid bodies have no collision model by default, can be populated by calling add collision shape
        // TODO: ChFrame should be set.
        body.AddCollisionShape(new ChCollisionShapeBox(mat, size.x, size.y, size.z), new ChFramed());
        // Note that the function `BuildModel` was obsoleted as there is no need anymore to indicate the end of specification of a collision model; indeed, processing of the generic collision models now occurs at a later time, during initialization of the collision detection system (see below)
        //https://github.com/projectchrono/chrono/blob/e377b8645b4bee95bee0623bb7402c68a76a229f/CHANGELOG.md?plain=1#L131
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
