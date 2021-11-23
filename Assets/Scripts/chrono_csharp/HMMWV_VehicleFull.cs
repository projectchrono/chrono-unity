//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class HMMWV_VehicleFull : HMMWV_Vehicle {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal HMMWV_VehicleFull(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.HMMWV_VehicleFull_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(HMMWV_VehicleFull obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_HMMWV_VehicleFull(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public HMMWV_VehicleFull(bool fixed_, DrivelineTypeWV drive_type, BrakeType brake_type, SteeringTypeWV steering_type, bool use_tierod_bodies, bool rigid_steering_column, ChContactMethod contact_method, CollisionType chassis_collision_type) : this(vehiclePINVOKE.new_HMMWV_VehicleFull__SWIG_0(fixed_, (int)drive_type, (int)brake_type, (int)steering_type, use_tierod_bodies, rigid_steering_column, (int)contact_method, (int)chassis_collision_type), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public HMMWV_VehicleFull(ChSystem system, bool fixed_, DrivelineTypeWV drive_type, BrakeType brake_type, SteeringTypeWV steering_type, bool use_tierod_bodies, bool rigid_steering_column, CollisionType chassis_collision_type) : this(vehiclePINVOKE.new_HMMWV_VehicleFull__SWIG_1(ChSystem.getCPtr(system), fixed_, (int)drive_type, (int)brake_type, (int)steering_type, use_tierod_bodies, rigid_steering_column, (int)chassis_collision_type), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetSpringForce(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.HMMWV_VehicleFull_GetSpringForce(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetSpringLength(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.HMMWV_VehicleFull_GetSpringLength(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetSpringDeformation(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.HMMWV_VehicleFull_GetSpringDeformation(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockForce(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.HMMWV_VehicleFull_GetShockForce(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockLength(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.HMMWV_VehicleFull_GetShockLength(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockVelocity(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.HMMWV_VehicleFull_GetShockVelocity(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Initialize(ChCoordsysD chassisPos, double chassisFwdVel) {
    vehiclePINVOKE.HMMWV_VehicleFull_Initialize__SWIG_0(swigCPtr, ChCoordsysD.getCPtr(chassisPos), chassisFwdVel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Initialize(ChCoordsysD chassisPos) {
    vehiclePINVOKE.HMMWV_VehicleFull_Initialize__SWIG_1(swigCPtr, ChCoordsysD.getCPtr(chassisPos));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LogHardpointLocations() {
    vehiclePINVOKE.HMMWV_VehicleFull_LogHardpointLocations(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void DebugLog(int what) {
    vehiclePINVOKE.HMMWV_VehicleFull_DebugLog(swigCPtr, what);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
