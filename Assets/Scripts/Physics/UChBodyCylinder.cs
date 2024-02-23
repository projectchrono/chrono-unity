using System;

public class UChBodyCylinder : UChBody
{
    public double radius;

    private ChContactMaterial mat;

    public UChBodyCylinder()
    {
        radius = 0.5;
        automaticMass = true;
    }

    public override void CalculateMassProperties()
    {
        var height = 2 * transform.localScale.y;
        mass = density * (Math.PI * Math.Pow(radius, 2) * height);
        inertiaMoments.x = (float)((1.0 / 12.0) * mass * (3 * Math.Pow(radius, 2) + Math.Pow(height, 2)));
        inertiaMoments.y = (float)(0.5 * mass * Math.Pow(radius, 2));
        inertiaMoments.z = (float)((1.0 / 12.0) * mass * (3 * Math.Pow(radius, 2) + Math.Pow(height, 2)));
    }

    public override void Create()
    {
        // Get a handle to the associated material component and create the Chrono material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DebugInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body and its collision shape
        var height = 2 * transform.localScale.y;
        body = new ChBodyAuxRef();
        // Create and add a cylinder collision shape using ChCollisionShapeCylinder
        // ChQuarternion y-to-z: the standard Chrono cylinder shape is defined with its axis along the Z-axis of the shape frame.
        // Whereas in a Y-up world, the axis is along the Y of the shape frame. 
        body.AddCollisionShape(new ChCollisionShapeCylinder(mat, radius, height), new ChFramed(new ChVector3d(), new ChQuaterniond(chrono.Q_ROTATE_Y_TO_Z)));
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
