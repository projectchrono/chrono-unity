//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class BodyState : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal BodyState(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(BodyState obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~BodyState() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          vehiclePINVOKE.delete_BodyState(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public ChVectorD pos {
    set {
      vehiclePINVOKE.BodyState_pos_set(swigCPtr, ChVectorD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.BodyState_pos_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChQuaternionD rot {
    set {
      vehiclePINVOKE.BodyState_rot_set(swigCPtr, ChQuaternionD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.BodyState_rot_get(swigCPtr);
      ChQuaternionD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChQuaternionD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChVectorD lin_vel {
    set {
      vehiclePINVOKE.BodyState_lin_vel_set(swigCPtr, ChVectorD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.BodyState_lin_vel_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChVectorD ang_vel {
    set {
      vehiclePINVOKE.BodyState_ang_vel_set(swigCPtr, ChVectorD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.BodyState_ang_vel_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public BodyState() : this(vehiclePINVOKE.new_BodyState(), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}