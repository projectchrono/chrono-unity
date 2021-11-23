//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class FlatTerrain : ChTerrain {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal FlatTerrain(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.FlatTerrain_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FlatTerrain obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_FlatTerrain(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public FlatTerrain(double height, float friction) : this(vehiclePINVOKE.new_FlatTerrain__SWIG_0(height, friction), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public FlatTerrain(double height) : this(vehiclePINVOKE.new_FlatTerrain__SWIG_1(height), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double GetHeight(ChVectorD loc) {
    double ret = vehiclePINVOKE.FlatTerrain_GetHeight(swigCPtr, ChVectorD.getCPtr(loc));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChVectorD GetNormal(ChVectorD loc) {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.FlatTerrain_GetNormal(swigCPtr, ChVectorD.getCPtr(loc)), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override float GetCoefficientFriction(ChVectorD loc) {
    float ret = vehiclePINVOKE.FlatTerrain_GetCoefficientFriction(swigCPtr, ChVectorD.getCPtr(loc));
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
