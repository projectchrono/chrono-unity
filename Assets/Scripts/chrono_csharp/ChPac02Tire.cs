//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChPac02Tire : ChTire {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChPac02Tire(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChPac02Tire_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChPac02Tire obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_ChPac02Tire(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public override string GetTemplateName() {
    string ret = vehiclePINVOKE.ChPac02Tire_GetTemplateName(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void AddVisualizationAssets(VisualizationType vis) {
    vehiclePINVOKE.ChPac02Tire_AddVisualizationAssets(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void RemoveVisualizationAssets() {
    vehiclePINVOKE.ChPac02Tire_RemoveVisualizationAssets(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double GetRadius() {
    double ret = vehiclePINVOKE.ChPac02Tire_GetRadius(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override TerrainForce ReportTireForce(ChTerrain terrain) {
    TerrainForce ret = new TerrainForce(vehiclePINVOKE.ChPac02Tire_ReportTireForce(swigCPtr, ChTerrain.getCPtr(terrain)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetGammaLimit(double gamma_limit) {
    vehiclePINVOKE.ChPac02Tire_SetGammaLimit(swigCPtr, gamma_limit);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double GetWidth() {
    double ret = vehiclePINVOKE.ChPac02Tire_GetWidth(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetDeflection() {
    double ret = vehiclePINVOKE.ChPac02Tire_GetDeflection(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual double GetVisualizationWidth() {
    double ret = vehiclePINVOKE.ChPac02Tire_GetVisualizationWidth(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetSlipAngle_internal() {
    double ret = vehiclePINVOKE.ChPac02Tire_GetSlipAngle_internal(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetLongitudinalSlip_internal() {
    double ret = vehiclePINVOKE.ChPac02Tire_GetLongitudinalSlip_internal(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetCamberAngle_internal() {
    double ret = vehiclePINVOKE.ChPac02Tire_GetCamberAngle_internal(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
