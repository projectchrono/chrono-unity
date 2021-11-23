//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChPathFollowerACCDriver : ChDriver {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal ChPathFollowerACCDriver(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChPathFollowerACCDriver_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChPathFollowerACCDriver obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          vehiclePINVOKE.delete_ChPathFollowerACCDriver(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChPathFollowerACCDriver(ChVehicle vehicle, ChBezierCurve path, string path_name, double target_speed, double target_following_time, double target_min_distance, double current_distance, bool isClosedPath) : this(vehiclePINVOKE.new_ChPathFollowerACCDriver__SWIG_0(ChVehicle.getCPtr(vehicle), ChBezierCurve.getCPtr(path), path_name, target_speed, target_following_time, target_min_distance, current_distance, isClosedPath), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathFollowerACCDriver(ChVehicle vehicle, ChBezierCurve path, string path_name, double target_speed, double target_following_time, double target_min_distance, double current_distance) : this(vehiclePINVOKE.new_ChPathFollowerACCDriver__SWIG_1(ChVehicle.getCPtr(vehicle), ChBezierCurve.getCPtr(path), path_name, target_speed, target_following_time, target_min_distance, current_distance), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathFollowerACCDriver(ChVehicle vehicle, string steering_filename, string speed_filename, ChBezierCurve path, string path_name, double target_speed, double target_following_time, double target_min_distance, double current_distance, bool isClosedPath) : this(vehiclePINVOKE.new_ChPathFollowerACCDriver__SWIG_2(ChVehicle.getCPtr(vehicle), steering_filename, speed_filename, ChBezierCurve.getCPtr(path), path_name, target_speed, target_following_time, target_min_distance, current_distance, isClosedPath), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathFollowerACCDriver(ChVehicle vehicle, string steering_filename, string speed_filename, ChBezierCurve path, string path_name, double target_speed, double target_following_time, double target_min_distance, double current_distance) : this(vehiclePINVOKE.new_ChPathFollowerACCDriver__SWIG_3(ChVehicle.getCPtr(vehicle), steering_filename, speed_filename, ChBezierCurve.getCPtr(path), path_name, target_speed, target_following_time, target_min_distance, current_distance), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetDesiredSpeed(double val) {
    vehiclePINVOKE.ChPathFollowerACCDriver_SetDesiredSpeed(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetDesiredFollowingTime(double val) {
    vehiclePINVOKE.ChPathFollowerACCDriver_SetDesiredFollowingTime(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetDesiredFollowingMinDistance(double val) {
    vehiclePINVOKE.ChPathFollowerACCDriver_SetDesiredFollowingMinDistance(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetCurrentDistance(double val) {
    vehiclePINVOKE.ChPathFollowerACCDriver_SetCurrentDistance(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetThresholdThrottle(double val) {
    vehiclePINVOKE.ChPathFollowerACCDriver_SetThresholdThrottle(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChPathSteeringController GetSteeringController() {
    ChPathSteeringController ret = new ChPathSteeringController(vehiclePINVOKE.ChPathFollowerACCDriver_GetSteeringController(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChAdaptiveSpeedController GetSpeedController() {
    ChAdaptiveSpeedController ret = new ChAdaptiveSpeedController(vehiclePINVOKE.ChPathFollowerACCDriver_GetSpeedController(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reset() {
    vehiclePINVOKE.ChPathFollowerACCDriver_Reset(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void Advance(double step) {
    vehiclePINVOKE.ChPathFollowerACCDriver_Advance(swigCPtr, step);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void ExportPathPovray(string out_dir) {
    vehiclePINVOKE.ChPathFollowerACCDriver_ExportPathPovray(swigCPtr, out_dir);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
