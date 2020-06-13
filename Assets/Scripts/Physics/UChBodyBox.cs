using System;

public class UChBodyBox : UChBody
{
    private ChMaterialSurface mat;

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
        body = new ChBodyAuxRef();
        body.GetCollisionModel().ClearModel();
        body.GetCollisionModel().AddBox(mat, size.x * 0.5, size.y * 0.5, size.z * 0.5);
        body.GetCollisionModel().BuildModel();
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
