//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class Gator_Vehicle : ChWheeledVehicle {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal Gator_Vehicle(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.Gator_Vehicle_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Gator_Vehicle obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_Gator_Vehicle(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public Gator_Vehicle(bool fixed_, DrivelineType driveline_type, BrakeType brake_type, ChContactMethod contact_method, ChassisCollisionType chassis_collision_type) : this(vehiclePINVOKE.new_Gator_Vehicle__SWIG_0(fixed_, (int)driveline_type, (int)brake_type, (int)contact_method, (int)chassis_collision_type), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public Gator_Vehicle(ChSystem system, bool fixed_, DrivelineType driveline_type, BrakeType brake_type, ChassisCollisionType chassis_collision_type) : this(vehiclePINVOKE.new_Gator_Vehicle__SWIG_1(ChSystem.getCPtr(system), fixed_, (int)driveline_type, (int)brake_type, (int)chassis_collision_type), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override int GetNumberAxles() {
    int ret = vehiclePINVOKE.Gator_Vehicle_GetNumberAxles(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetWheelbase() {
    double ret = vehiclePINVOKE.Gator_Vehicle_GetWheelbase(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetMinTurningRadius() {
    double ret = vehiclePINVOKE.Gator_Vehicle_GetMinTurningRadius(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetMaxSteeringAngle() {
    double ret = vehiclePINVOKE.Gator_Vehicle_GetMaxSteeringAngle(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetInitWheelAngVel(vector_double omega) {
    vehiclePINVOKE.Gator_Vehicle_SetInitWheelAngVel(swigCPtr, vector_double.getCPtr(omega));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetShockForce(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.Gator_Vehicle_GetShockForce(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockLength(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.Gator_Vehicle_GetShockLength(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetShockVelocity(int axle, VehicleSide side) {
    double ret = vehiclePINVOKE.Gator_Vehicle_GetShockVelocity(swigCPtr, axle, (int)side);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Initialize(ChCoordsysD chassisPos, double chassisFwdVel) {
    vehiclePINVOKE.Gator_Vehicle_Initialize__SWIG_0(swigCPtr, ChCoordsysD.getCPtr(chassisPos), chassisFwdVel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Initialize(ChCoordsysD chassisPos) {
    vehiclePINVOKE.Gator_Vehicle_Initialize__SWIG_1(swigCPtr, ChCoordsysD.getCPtr(chassisPos));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LogHardpointLocations() {
    vehiclePINVOKE.Gator_Vehicle_LogHardpointLocations(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void DebugLog(int what) {
    vehiclePINVOKE.Gator_Vehicle_DebugLog(swigCPtr, what);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
