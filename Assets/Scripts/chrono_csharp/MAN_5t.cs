//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class MAN_5t : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal MAN_5t(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MAN_5t obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~MAN_5t() {
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
          vehiclePINVOKE.delete_MAN_5t(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public MAN_5t() : this(vehiclePINVOKE.new_MAN_5t__SWIG_0(), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public MAN_5t(ChSystem system) : this(vehiclePINVOKE.new_MAN_5t__SWIG_1(ChSystem.getCPtr(system)), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetContactMethod(ChContactMethod val) {
    vehiclePINVOKE.MAN_5t_SetContactMethod(swigCPtr, (int)val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetChassisFixed(bool val) {
    vehiclePINVOKE.MAN_5t_SetChassisFixed(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetChassisCollisionType(CollisionType val) {
    vehiclePINVOKE.MAN_5t_SetChassisCollisionType(swigCPtr, (int)val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPowertrainType(PowertrainModelType val) {
    vehiclePINVOKE.MAN_5t_SetPowertrainType(swigCPtr, (int)val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetBrakeType(BrakeType brake_type) {
    vehiclePINVOKE.MAN_5t_SetBrakeType(swigCPtr, (int)brake_type);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTireType(TireModelType val) {
    vehiclePINVOKE.MAN_5t_SetTireType(swigCPtr, (int)val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetInitPosition(ChCoordsysD pos) {
    vehiclePINVOKE.MAN_5t_SetInitPosition(swigCPtr, ChCoordsysD.getCPtr(pos));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetInitFwdVel(double fwdVel) {
    vehiclePINVOKE.MAN_5t_SetInitFwdVel(swigCPtr, fwdVel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetInitWheelAngVel(vector_double omega) {
    vehiclePINVOKE.MAN_5t_SetInitWheelAngVel(swigCPtr, vector_double.getCPtr(omega));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTireStepSize(double step_size) {
    vehiclePINVOKE.MAN_5t_SetTireStepSize(swigCPtr, step_size);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void EnableBrakeLocking(bool lock_) {
    vehiclePINVOKE.MAN_5t_EnableBrakeLocking(swigCPtr, lock_);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChSystem GetSystem() {
    global::System.IntPtr cPtr = vehiclePINVOKE.MAN_5t_GetSystem(swigCPtr);
    ChSystem ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSystem(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChWheeledVehicle GetVehicle() {
    ChWheeledVehicle ret = new ChWheeledVehicle(vehiclePINVOKE.MAN_5t_GetVehicle(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChChassis GetChassis() {
    global::System.IntPtr cPtr = vehiclePINVOKE.MAN_5t_GetChassis(swigCPtr);
    ChChassis ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChChassis(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChBodyAuxRef GetChassisBody() {
    global::System.IntPtr cPtr = vehiclePINVOKE.MAN_5t_GetChassisBody(swigCPtr);
    ChBodyAuxRef ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBodyAuxRef(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChPowertrain GetPowertrain() {
    global::System.IntPtr cPtr = vehiclePINVOKE.MAN_5t_GetPowertrain(swigCPtr);
    ChPowertrain ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPowertrain(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTotalMass() {
    double ret = vehiclePINVOKE.MAN_5t_GetTotalMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Initialize() {
    vehiclePINVOKE.MAN_5t_Initialize(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetAerodynamicDrag(double Cd, double area, double air_density) {
    vehiclePINVOKE.MAN_5t_SetAerodynamicDrag(swigCPtr, Cd, area, air_density);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetChassisVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.MAN_5t_SetChassisVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSuspensionVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.MAN_5t_SetSuspensionVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSteeringVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.MAN_5t_SetSteeringVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetWheelVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.MAN_5t_SetWheelVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetTireVisualizationType(VisualizationType vis) {
    vehiclePINVOKE.MAN_5t_SetTireVisualizationType(swigCPtr, (int)vis);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Synchronize(double time, DriverInputs driver_inputs, ChTerrain terrain) {
    vehiclePINVOKE.MAN_5t_Synchronize(swigCPtr, time, DriverInputs.getCPtr(driver_inputs), ChTerrain.getCPtr(terrain));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Advance(double step) {
    vehiclePINVOKE.MAN_5t_Advance(swigCPtr, step);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void LogHardpointLocations() {
    vehiclePINVOKE.MAN_5t_LogHardpointLocations(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void DebugLog(int what) {
    vehiclePINVOKE.MAN_5t_DebugLog(swigCPtr, what);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
