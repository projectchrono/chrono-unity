//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChSimpleDrivelineXWD : ChDrivelineWV {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChSimpleDrivelineXWD(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChSimpleDrivelineXWD_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChSimpleDrivelineXWD obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_ChSimpleDrivelineXWD(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public override string GetTemplateName() {
    string ret = vehiclePINVOKE.ChSimpleDrivelineXWD_GetTemplateName(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int GetNumDrivenAxles() {
    int ret = vehiclePINVOKE.ChSimpleDrivelineXWD_GetNumDrivenAxles(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Initialize(ChChassis chassis, ChAxleList axles, vector_int driven_axles) {
    vehiclePINVOKE.ChSimpleDrivelineXWD_Initialize(swigCPtr, ChChassis.getCPtr(chassis), ChAxleList.getCPtr(axles), vector_int.getCPtr(driven_axles));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Synchronize(double time, DriverInputs driver_inputs, double torque) {
    vehiclePINVOKE.ChSimpleDrivelineXWD_Synchronize(swigCPtr, time, DriverInputs.getCPtr(driver_inputs), torque);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double GetSpindleTorque(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.ChSimpleDrivelineXWD_GetSpindleTorque(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Disconnect() {
    vehiclePINVOKE.ChSimpleDrivelineXWD_Disconnect(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
