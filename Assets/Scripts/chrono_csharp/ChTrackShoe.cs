//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChTrackShoe : ChPart {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChTrackShoe(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.ChTrackShoe_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChTrackShoe obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_ChTrackShoe(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public virtual GuidePinType GetType() {
    GuidePinType ret = (GuidePinType)vehiclePINVOKE.ChTrackShoe_GetType(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public uint GetIndex() {
    uint ret = vehiclePINVOKE.ChTrackShoe_GetIndex(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public ChBody GetShoeBody() {
    global::System.IntPtr cPtr = vehiclePINVOKE.ChTrackShoe_GetShoeBody(swigCPtr);
    ChBody ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBody(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetTension() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackShoe_GetTension(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual double GetHeight() {
    double ret = vehiclePINVOKE.ChTrackShoe_GetHeight(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual double GetPitch() {
    double ret = vehiclePINVOKE.ChTrackShoe_GetPitch(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetLateralContactPoint() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.ChTrackShoe_GetLateralContactPoint(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetCollide(bool val) {
    vehiclePINVOKE.ChTrackShoe_SetCollide(swigCPtr, val);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Initialize(ChBodyAuxRef chassis, ChVectorD location, ChQuaternionD rotation) {
    vehiclePINVOKE.ChTrackShoe_Initialize(swigCPtr, ChBodyAuxRef.getCPtr(chassis), ChVectorD.getCPtr(location), ChQuaternionD.getCPtr(rotation));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetIndex(uint index) {
    vehiclePINVOKE.ChTrackShoe_SetIndex(swigCPtr, index);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Connect(ChTrackShoe next, ChTrackAssembly assembly, ChChassis chassis, bool ccw) {
    vehiclePINVOKE.ChTrackShoe_Connect(swigCPtr, ChTrackShoe.getCPtr(next), ChTrackAssembly.getCPtr(assembly), ChChassis.getCPtr(chassis), ccw);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
