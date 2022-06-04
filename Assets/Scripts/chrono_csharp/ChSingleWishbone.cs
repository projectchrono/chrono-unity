//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChSingleWishbone : ChSuspension {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChSingleWishbone(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChSingleWishbone_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChSingleWishbone obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_ChSingleWishbone(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public override string GetTemplateName() {
    string ret = vehiclePINVOKE.ChSingleWishbone_GetTemplateName(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool IsSteerable() {
    bool ret = vehiclePINVOKE.ChSingleWishbone_IsSteerable(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override bool IsIndependent() {
    bool ret = vehiclePINVOKE.ChSingleWishbone_IsIndependent(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Initialize(ChChassis chassis, ChSubchassis subchassis, ChSteering steering, ChVectorD location, double left_ang_vel, double right_ang_vel) {
    vehiclePINVOKE.ChSingleWishbone_Initialize__SWIG_0(swigCPtr, ChChassis.getCPtr(chassis), ChSubchassis.getCPtr(subchassis), ChSteering.getCPtr(steering), ChVectorD.getCPtr(location), left_ang_vel, right_ang_vel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Initialize(ChChassis chassis, ChSubchassis subchassis, ChSteering steering, ChVectorD location, double left_ang_vel) {
    vehiclePINVOKE.ChSingleWishbone_Initialize__SWIG_1(swigCPtr, ChChassis.getCPtr(chassis), ChSubchassis.getCPtr(subchassis), ChSteering.getCPtr(steering), ChVectorD.getCPtr(location), left_ang_vel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Initialize(ChChassis chassis, ChSubchassis subchassis, ChSteering steering, ChVectorD location) {
    vehiclePINVOKE.ChSingleWishbone_Initialize__SWIG_2(swigCPtr, ChChassis.getCPtr(chassis), ChSubchassis.getCPtr(subchassis), ChSteering.getCPtr(steering), ChVectorD.getCPtr(location));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void AddVisualizationAssets(VisualizationType vis) {
    vehiclePINVOKE.ChSingleWishbone_AddVisualizationAssets(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void RemoveVisualizationAssets() {
    vehiclePINVOKE.ChSingleWishbone_RemoveVisualizationAssets(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double GetTrack() {
    double ret = vehiclePINVOKE.ChSingleWishbone_GetTrack(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChLinkTSDA GetShock(VehicleSide side) {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChSingleWishbone_GetShock(swigCPtr, (int)side);
    ChLinkTSDA ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChLinkTSDA(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override Force ReportSuspensionForce(VehicleSide side) {
    Force ret = new Force(vehiclePINVOKE.ChSingleWishbone_ReportSuspensionForce(swigCPtr, (int)side), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockForce(VehicleSide side) {
    double ret = vehiclePINVOKE.ChSingleWishbone_GetShockForce(swigCPtr, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockLength(VehicleSide side) {
    double ret = vehiclePINVOKE.ChSingleWishbone_GetShockLength(swigCPtr, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockVelocity(VehicleSide side) {
    double ret = vehiclePINVOKE.ChSingleWishbone_GetShockVelocity(swigCPtr, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void LogConstraintViolations(VehicleSide side) {
    vehiclePINVOKE.ChSingleWishbone_LogConstraintViolations(swigCPtr, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override ChBody GetAntirollBody(VehicleSide side) {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChSingleWishbone_GetAntirollBody(swigCPtr, (int)side);
    ChBody ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBody(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void LogHardpointLocations(ChVectorD ref_, bool inches) {
    vehiclePINVOKE.ChSingleWishbone_LogHardpointLocations__SWIG_0(swigCPtr, ChVectorD.getCPtr(ref_), inches);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LogHardpointLocations(ChVectorD ref_) {
    vehiclePINVOKE.ChSingleWishbone_LogHardpointLocations__SWIG_1(swigCPtr, ChVectorD.getCPtr(ref_));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
