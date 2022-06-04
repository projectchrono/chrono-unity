//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class MapSpringTorque : TorqueFunctor {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal MapSpringTorque(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.MapSpringTorque_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MapSpringTorque obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_MapSpringTorque(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public MapSpringTorque() : this(vehiclePINVOKE.new_MapSpringTorque__SWIG_0(), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public MapSpringTorque(SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t data, double rest_angle) : this(vehiclePINVOKE.new_MapSpringTorque__SWIG_1(SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t.getCPtr(data), rest_angle), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public MapSpringTorque(SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t data) : this(vehiclePINVOKE.new_MapSpringTorque__SWIG_2(SWIGTYPE_p_std__vectorT_std__pairT_double_double_t_t.getCPtr(data)), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public void add_point(double x, double y) {
    vehiclePINVOKE.MapSpringTorque_add_point(swigCPtr, x, y);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double evaluate(double time, double angle, double vel, ChLinkRSDA link) {
    double ret = vehiclePINVOKE.MapSpringTorque_evaluate(swigCPtr, time, angle, vel, ChLinkRSDA.getCPtr(link));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
