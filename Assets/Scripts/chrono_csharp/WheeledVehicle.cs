//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class WheeledVehicle : ChWheeledVehicle {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal WheeledVehicle(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.WheeledVehicle_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(WheeledVehicle obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_WheeledVehicle(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public WheeledVehicle(string filename, ChContactMethod contact_method, bool create_powertrain, bool create_tires) : this(vehiclePINVOKE.new_WheeledVehicle__SWIG_0(filename, (int)contact_method, create_powertrain, create_tires), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public WheeledVehicle(string filename, ChContactMethod contact_method, bool create_powertrain) : this(vehiclePINVOKE.new_WheeledVehicle__SWIG_1(filename, (int)contact_method, create_powertrain), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public WheeledVehicle(string filename, ChContactMethod contact_method) : this(vehiclePINVOKE.new_WheeledVehicle__SWIG_2(filename, (int)contact_method), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public WheeledVehicle(string filename) : this(vehiclePINVOKE.new_WheeledVehicle__SWIG_3(filename), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public WheeledVehicle(ChSystem system, string filename, bool create_powertrain, bool create_tires) : this(vehiclePINVOKE.new_WheeledVehicle__SWIG_4(ChSystem.getCPtr(system), filename, create_powertrain, create_tires), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public WheeledVehicle(ChSystem system, string filename, bool create_powertrain) : this(vehiclePINVOKE.new_WheeledVehicle__SWIG_5(ChSystem.getCPtr(system), filename, create_powertrain), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public WheeledVehicle(ChSystem system, string filename) : this(vehiclePINVOKE.new_WheeledVehicle__SWIG_6(ChSystem.getCPtr(system), filename), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override int GetNumberAxles() {
    int ret = vehiclePINVOKE.WheeledVehicle_GetNumberAxles(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetWheelbase() {
    double ret = vehiclePINVOKE.WheeledVehicle_GetWheelbase(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetMinTurningRadius() {
    double ret = vehiclePINVOKE.WheeledVehicle_GetMinTurningRadius(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetMaxSteeringAngle() {
    double ret = vehiclePINVOKE.WheeledVehicle_GetMaxSteeringAngle(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Initialize(ChCoordsysD chassisPos, double chassisFwdVel) {
    vehiclePINVOKE.WheeledVehicle_Initialize__SWIG_0(swigCPtr, ChCoordsysD.getCPtr(chassisPos), chassisFwdVel);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Initialize(ChCoordsysD chassisPos) {
    vehiclePINVOKE.WheeledVehicle_Initialize__SWIG_1(swigCPtr, ChCoordsysD.getCPtr(chassisPos));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}