//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChPathSteeringControllerXT : ChSteeringController {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal ChPathSteeringControllerXT(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChPathSteeringControllerXT_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChPathSteeringControllerXT obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          vehiclePINVOKE.delete_ChPathSteeringControllerXT(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChPathSteeringControllerXT(ChBezierCurve path, bool isClosedPath, double max_wheel_turn_angle) : this(vehiclePINVOKE.new_ChPathSteeringControllerXT__SWIG_0(ChBezierCurve.getCPtr(path), isClosedPath, max_wheel_turn_angle), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathSteeringControllerXT(ChBezierCurve path, bool isClosedPath) : this(vehiclePINVOKE.new_ChPathSteeringControllerXT__SWIG_1(ChBezierCurve.getCPtr(path), isClosedPath), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathSteeringControllerXT(ChBezierCurve path) : this(vehiclePINVOKE.new_ChPathSteeringControllerXT__SWIG_2(ChBezierCurve.getCPtr(path)), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathSteeringControllerXT(string filename, ChBezierCurve path, bool isClosedPath, double max_wheel_turn_angle) : this(vehiclePINVOKE.new_ChPathSteeringControllerXT__SWIG_3(filename, ChBezierCurve.getCPtr(path), isClosedPath, max_wheel_turn_angle), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathSteeringControllerXT(string filename, ChBezierCurve path, bool isClosedPath) : this(vehiclePINVOKE.new_ChPathSteeringControllerXT__SWIG_4(filename, ChBezierCurve.getCPtr(path), isClosedPath), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathSteeringControllerXT(string filename, ChBezierCurve path) : this(vehiclePINVOKE.new_ChPathSteeringControllerXT__SWIG_5(filename, ChBezierCurve.getCPtr(path)), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChBezierCurve GetPath() {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChPathSteeringControllerXT_GetPath(swigCPtr);
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Reset(ChVehicle vehicle) {
    vehiclePINVOKE.ChPathSteeringControllerXT_Reset(swigCPtr, ChVehicle.getCPtr(vehicle));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetGains(double Kp, double W_y_err, double W_heading_err, double W_ackermann) {
    vehiclePINVOKE.ChPathSteeringControllerXT_SetGains__SWIG_0(swigCPtr, Kp, W_y_err, W_heading_err, W_ackermann);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public new void SetGains(double Kp, double W_y_err, double W_heading_err) {
    vehiclePINVOKE.ChPathSteeringControllerXT_SetGains__SWIG_1(swigCPtr, Kp, W_y_err, W_heading_err);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetGains(double Kp, double W_y_err) {
    vehiclePINVOKE.ChPathSteeringControllerXT_SetGains__SWIG_2(swigCPtr, Kp, W_y_err);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetGains(double Kp) {
    vehiclePINVOKE.ChPathSteeringControllerXT_SetGains__SWIG_3(swigCPtr, Kp);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetGains() {
    vehiclePINVOKE.ChPathSteeringControllerXT_SetGains__SWIG_4(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void CalcTargetLocation() {
    vehiclePINVOKE.ChPathSteeringControllerXT_CalcTargetLocation(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public new double Advance(ChVehicle vehicle, double step) {
    double ret = vehiclePINVOKE.ChPathSteeringControllerXT_Advance(swigCPtr, ChVehicle.getCPtr(vehicle), step);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}