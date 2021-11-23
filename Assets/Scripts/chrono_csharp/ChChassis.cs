//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChChassis : ChPart {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChChassis(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChChassis_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChChassis obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_ChChassis(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public virtual double GetMass() {
    double ret = vehiclePINVOKE.ChChassis_GetMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChMatrix33D GetInertia() {
    ChMatrix33D ret = new ChMatrix33D(vehiclePINVOKE.ChChassis_GetInertia(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetLocalPosCOM() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetLocalPosCOM(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChCoordsysD GetLocalDriverCoordsys() {
    ChCoordsysD ret = new ChCoordsysD(vehiclePINVOKE.ChChassis_GetLocalDriverCoordsys(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetLocalPosRearConnector() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetLocalPosRearConnector(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChBodyAuxRef GetBody() {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChChassis_GetBody(swigCPtr);
    ChBodyAuxRef ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBodyAuxRef(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChSystem GetSystem() {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChChassis_GetSystem(swigCPtr);
    ChSystem ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSystem(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPos() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetPos(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetRot() {
    ChQuaternionD ret = new ChQuaternionD(vehiclePINVOKE.ChChassis_GetRot(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetCOMPos() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetCOMPos(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChQuaternionD GetCOMRot() {
    ChQuaternionD ret = new ChQuaternionD(vehiclePINVOKE.ChChassis_GetCOMRot(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetDriverPos() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetDriverPos(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetSpeed() {
    double ret = vehiclePINVOKE.ChChassis_GetSpeed(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetCOMSpeed() {
    double ret = vehiclePINVOKE.ChChassis_GetCOMSpeed(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPointLocation(ChVectorD locpos) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetPointLocation(swigCPtr, ChVectorD.getCPtr(locpos)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPointVelocity(ChVectorD locpos) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetPointVelocity(swigCPtr, ChVectorD.getCPtr(locpos)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChVectorD GetPointAcceleration(ChVectorD locpos) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChChassis_GetPointAcceleration(swigCPtr, ChVectorD.getCPtr(locpos)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void Initialize(ChSystem system, ChCoordsysD chassisPos, double chassisFwdVel, int collision_family) {
    vehiclePINVOKE.ChChassis_Initialize__SWIG_0(swigCPtr, ChSystem.getCPtr(system), ChCoordsysD.getCPtr(chassisPos), chassisFwdVel, collision_family);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Initialize(ChSystem system, ChCoordsysD chassisPos, double chassisFwdVel) {
    vehiclePINVOKE.ChChassis_Initialize__SWIG_1(swigCPtr, ChSystem.getCPtr(system), ChCoordsysD.getCPtr(chassisPos), chassisFwdVel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetCollide(bool state) {
    vehiclePINVOKE.ChChassis_SetCollide(swigCPtr, state);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetFixed(bool val) {
    vehiclePINVOKE.ChChassis_SetFixed(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool IsFixed() {
    bool ret = vehiclePINVOKE.ChChassis_IsFixed(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool HasBushings() {
    bool ret = vehiclePINVOKE.ChChassis_HasBushings(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void AddMarker(string name, ChCoordsysD pos) {
    vehiclePINVOKE.ChChassis_AddMarker(swigCPtr, name, ChCoordsysD.getCPtr(pos));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMarker_t_t GetMarkers() {
    SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMarker_t_t ret = new SWIGTYPE_p_std__vectorT_std__shared_ptrT_chrono__ChMarker_t_t(vehiclePINVOKE.ChChassis_GetMarkers(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetAerodynamicDrag(double Cd, double area, double air_density) {
    vehiclePINVOKE.ChChassis_SetAerodynamicDrag(swigCPtr, Cd, area, air_density);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Synchronize(double time) {
    vehiclePINVOKE.ChChassis_Synchronize(swigCPtr, time);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddJoint(SWIGTYPE_p_std__shared_ptrT_ChVehicleJoint_t joint) {
    vehiclePINVOKE.ChChassis_AddJoint(swigCPtr, SWIGTYPE_p_std__shared_ptrT_ChVehicleJoint_t.getCPtr(joint));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public static void RemoveJoint(SWIGTYPE_p_std__shared_ptrT_ChVehicleJoint_t joint) {
    vehiclePINVOKE.ChChassis_RemoveJoint(SWIGTYPE_p_std__shared_ptrT_ChVehicleJoint_t.getCPtr(joint));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddExternalForce(SWIGTYPE_p_std__shared_ptrT_chrono__vehicle__ChChassis__ExternalForce_t force) {
    vehiclePINVOKE.ChChassis_AddExternalForce(swigCPtr, SWIGTYPE_p_std__shared_ptrT_chrono__vehicle__ChChassis__ExternalForce_t.getCPtr(force));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
