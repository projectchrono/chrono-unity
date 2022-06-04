//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class M113_TrackAssemblySinglePin : ChTrackAssemblySinglePin {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal M113_TrackAssemblySinglePin(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.M113_TrackAssemblySinglePin_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(M113_TrackAssemblySinglePin obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_M113_TrackAssemblySinglePin(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public M113_TrackAssemblySinglePin(VehicleSide side, BrakeType brake_type, bool use_track_bushings, bool use_suspension_bushings, bool use_track_RSDA) : this(vehiclePINVOKE.new_M113_TrackAssemblySinglePin((int)side, (int)brake_type, use_track_bushings, use_suspension_bushings, use_track_RSDA), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override ChVectorD GetSprocketLocation() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.M113_TrackAssemblySinglePin_GetSprocketLocation(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChVectorD GetIdlerLocation() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.M113_TrackAssemblySinglePin_GetIdlerLocation(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChVectorD GetRoadWhelAssemblyLocation(int which) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.M113_TrackAssemblySinglePin_GetRoadWhelAssemblyLocation(swigCPtr, which), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}