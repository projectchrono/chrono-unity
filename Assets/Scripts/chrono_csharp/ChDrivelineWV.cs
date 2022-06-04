//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChDrivelineWV : ChDriveline {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChDrivelineWV(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChDrivelineWV_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChDrivelineWV obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_ChDrivelineWV(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public virtual int GetNumDrivenAxles() {
    int ret = vehiclePINVOKE.ChDrivelineWV_GetNumDrivenAxles(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void Initialize(ChChassis chassis, ChAxleList axles, vector_int driven_axles) {
    vehiclePINVOKE.ChDrivelineWV_Initialize(swigCPtr, ChChassis.getCPtr(chassis), ChAxleList.getCPtr(axles), vector_int.getCPtr(driven_axles));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Synchronize(double time, DriverInputs driver_inputs, double torque) {
    vehiclePINVOKE.ChDrivelineWV_Synchronize(swigCPtr, time, DriverInputs.getCPtr(driver_inputs), torque);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void LockAxleDifferential(int axle, bool lock_) {
    vehiclePINVOKE.ChDrivelineWV_LockAxleDifferential(swigCPtr, axle, lock_);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void LockCentralDifferential(int which, bool lock_) {
    vehiclePINVOKE.ChDrivelineWV_LockCentralDifferential(swigCPtr, which, lock_);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public vector_int GetDrivenAxleIndexes() {
    vector_int ret = new vector_int(vehiclePINVOKE.ChDrivelineWV_GetDrivenAxleIndexes(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual double GetSpindleTorque(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.ChDrivelineWV_GetSpindleTorque(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void Disconnect() {
    vehiclePINVOKE.ChDrivelineWV_Disconnect(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
