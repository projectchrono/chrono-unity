//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class MapSpringDamperActuatorForce : ForceFunctor {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal MapSpringDamperActuatorForce(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.MapSpringDamperActuatorForce_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MapSpringDamperActuatorForce obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_MapSpringDamperActuatorForce(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public MapSpringDamperActuatorForce() : this(vehiclePINVOKE.new_MapSpringDamperActuatorForce__SWIG_0(), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public MapSpringDamperActuatorForce(SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t dataK, SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t dataC, double f) : this(vehiclePINVOKE.new_MapSpringDamperActuatorForce__SWIG_1(SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t.getCPtr(dataK), SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t.getCPtr(dataC), f), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void add_pointK(double x, double y) {
    vehiclePINVOKE.MapSpringDamperActuatorForce_add_pointK(swigCPtr, x, y);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void add_pointC(double x, double y) {
    vehiclePINVOKE.MapSpringDamperActuatorForce_add_pointC(swigCPtr, x, y);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void set_f(double f) {
    vehiclePINVOKE.MapSpringDamperActuatorForce_set_f(swigCPtr, f);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

}
