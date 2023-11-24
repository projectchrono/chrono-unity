using System;
using System.Security.AccessControl;

public class UChBodySphere : UChBody
{
    public double radius;

    private ChMaterialSurface mat;

    public UChBodySphere()
    {
        radius = 0.5;
        automaticMass = true;
    }

    public override void CalculateMassProperties()
    {
        mass = density * ((4.0 / 3.0) * Math.PI * Math.Pow(radius, 3));
        double inertia = (2.0 / 5.0) * mass * Math.Pow(radius, 2);
        inertiaMoments.x = (float)inertia;
        inertiaMoments.y = (float)inertia;
        inertiaMoments.z = (float)inertia;
    }

    public override void Create()
    {
        // Get a handle to the associated material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DebugInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body and its collision shape
        body = new ChBodyAuxRef();
        body.GetCollisionModel().Clear();
        // Create a sphere collision shape with the new approach ChCollisionShape
        body.GetCollisionModel().AddShape(new ChCollisionShapeSphere(mat, radius));
        body.GetCollisionModel().Build();
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
