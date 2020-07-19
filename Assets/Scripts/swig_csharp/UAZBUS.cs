//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class UAZBUS : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal UAZBUS(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(UAZBUS obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~UAZBUS() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnBase) {
          swigCMemOwnBase = false;
          vehiclePINVOKE.delete_UAZBUS(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public UAZBUS() : this(vehiclePINVOKE.new_UAZBUS__SWIG_0(), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public UAZBUS(ChSystem system) : this(vehiclePINVOKE.new_UAZBUS__SWIG_1(ChSystem.getCPtr(system)), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetContactMethod(ChContactMethod val) {
    vehiclePINVOKE.UAZBUS_SetContactMethod(swigCPtr, (int)val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetChassisFixed(bool val) {
    vehiclePINVOKE.UAZBUS_SetChassisFixed(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetChassisCollisionType(ChassisCollisionType val) {
    vehiclePINVOKE.UAZBUS_SetChassisCollisionType(swigCPtr, (int)val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetBrakeType(BrakeType brake_type) {
    vehiclePINVOKE.UAZBUS_SetBrakeType(swigCPtr, (int)brake_type);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTireType(TireModelType val) {
    vehiclePINVOKE.UAZBUS_SetTireType(swigCPtr, (int)val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetInitPosition(ChCoordsysD pos) {
    vehiclePINVOKE.UAZBUS_SetInitPosition(swigCPtr, ChCoordsysD.getCPtr(pos));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetInitFwdVel(double fwdVel) {
    vehiclePINVOKE.UAZBUS_SetInitFwdVel(swigCPtr, fwdVel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetInitWheelAngVel(vector_double omega) {
    vehiclePINVOKE.UAZBUS_SetInitWheelAngVel(swigCPtr, vector_double.getCPtr(omega));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTireStepSize(double step_size) {
    vehiclePINVOKE.UAZBUS_SetTireStepSize(swigCPtr, step_size);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void EnableBrakeLocking(bool lock_) {
    vehiclePINVOKE.UAZBUS_EnableBrakeLocking(swigCPtr, lock_);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChSystem GetSystem() {
    global::System.IntPtr cPtr = vehiclePINVOKE.UAZBUS_GetSystem(swigCPtr);
    ChSystem ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSystem(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChWheeledVehicle GetVehicle() {
    ChWheeledVehicle ret = new ChWheeledVehicle(vehiclePINVOKE.UAZBUS_GetVehicle(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChChassis GetChassis() {
    global::System.IntPtr cPtr = vehiclePINVOKE.UAZBUS_GetChassis(swigCPtr);
    ChChassis ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChChassis(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChBodyAuxRef GetChassisBody() {
    global::System.IntPtr cPtr = vehiclePINVOKE.UAZBUS_GetChassisBody(swigCPtr);
    ChBodyAuxRef ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBodyAuxRef(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChPowertrain GetPowertrain() {
    global::System.IntPtr cPtr = vehiclePINVOKE.UAZBUS_GetPowertrain(swigCPtr);
    ChPowertrain ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPowertrain(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTotalMass() {
    double ret = vehiclePINVOKE.UAZBUS_GetTotalMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Initialize() {
    vehiclePINVOKE.UAZBUS_Initialize(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LockAxleDifferential(int axle, bool lock_) {
    vehiclePINVOKE.UAZBUS_LockAxleDifferential(swigCPtr, axle, lock_);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LockCentralDifferential(int which, bool lock_) {
    vehiclePINVOKE.UAZBUS_LockCentralDifferential(swigCPtr, which, lock_);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetAerodynamicDrag(double Cd, double area, double air_density) {
    vehiclePINVOKE.UAZBUS_SetAerodynamicDrag(swigCPtr, Cd, area, air_density);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetChassisVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.UAZBUS_SetChassisVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSuspensionVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.UAZBUS_SetSuspensionVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSteeringVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.UAZBUS_SetSteeringVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetWheelVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.UAZBUS_SetWheelVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTireVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.UAZBUS_SetTireVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Synchronize(double time, DriverInputs driver_inputs, ChTerrain terrain) {
    vehiclePINVOKE.UAZBUS_Synchronize(swigCPtr, time, DriverInputs.getCPtr(driver_inputs), ChTerrain.getCPtr(terrain));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Advance(double step) {
    vehiclePINVOKE.UAZBUS_Advance(swigCPtr, step);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LogHardpointLocations() {
    vehiclePINVOKE.UAZBUS_LogHardpointLocations(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void DebugLog(int what) {
    vehiclePINVOKE.UAZBUS_DebugLog(swigCPtr, what);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
