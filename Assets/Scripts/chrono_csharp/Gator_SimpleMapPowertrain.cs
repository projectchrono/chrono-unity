//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class Gator_SimpleMapPowertrain : ChSimpleMapPowertrain {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal Gator_SimpleMapPowertrain(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.Gator_SimpleMapPowertrain_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Gator_SimpleMapPowertrain obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_Gator_SimpleMapPowertrain(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public Gator_SimpleMapPowertrain(string name) : this(vehiclePINVOKE.new_Gator_SimpleMapPowertrain(name), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual double GetMaxEngineSpeed() {
    double ret = vehiclePINVOKE.Gator_SimpleMapPowertrain_GetMaxEngineSpeed(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void SetEngineTorqueMaps(SWIGTYPE_p_ChFunction_Recorder map0, SWIGTYPE_p_ChFunction_Recorder mapF) {
    vehiclePINVOKE.Gator_SimpleMapPowertrain_SetEngineTorqueMaps(swigCPtr, SWIGTYPE_p_ChFunction_Recorder.getCPtr(map0), SWIGTYPE_p_ChFunction_Recorder.getCPtr(mapF));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetGearRatios(vector_double fwd, SWIGTYPE_p_double rev) {
    vehiclePINVOKE.Gator_SimpleMapPowertrain_SetGearRatios(swigCPtr, vector_double.getCPtr(fwd), SWIGTYPE_p_double.getCPtr(rev));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void SetShiftPoints(SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t shift_bands) {
    vehiclePINVOKE.Gator_SimpleMapPowertrain_SetShiftPoints(swigCPtr, SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t.getCPtr(shift_bands));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}