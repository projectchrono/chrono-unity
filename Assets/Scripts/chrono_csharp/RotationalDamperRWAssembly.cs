//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class RotationalDamperRWAssembly : ChRotationalDamperRWAssembly {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal RotationalDamperRWAssembly(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.RotationalDamperRWAssembly_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RotationalDamperRWAssembly obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_RotationalDamperRWAssembly(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public RotationalDamperRWAssembly(string filename, bool has_shock) : this(vehiclePINVOKE.new_RotationalDamperRWAssembly__SWIG_0(filename, has_shock), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public RotationalDamperRWAssembly(SWIGTYPE_p_rapidjson__Document d, bool has_shock) : this(vehiclePINVOKE.new_RotationalDamperRWAssembly__SWIG_1(SWIGTYPE_p_rapidjson__Document.getCPtr(d), has_shock), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual double GetArmMass() {
    double ret = vehiclePINVOKE.RotationalDamperRWAssembly_GetArmMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD GetArmInertia() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.RotationalDamperRWAssembly_GetArmInertia(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual double GetArmVisRadius() {
    double ret = vehiclePINVOKE.RotationalDamperRWAssembly_GetArmVisRadius(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual TorqueFunctor GetSpringTorqueFunctor() {
    global::System.IntPtr cPtr = vehiclePINVOKE.RotationalDamperRWAssembly_GetSpringTorqueFunctor(swigCPtr);
    TorqueFunctor ret = (cPtr == global::System.IntPtr.Zero) ? null : new TorqueFunctor(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual TorqueFunctor GetShockTorqueCallback() {
    global::System.IntPtr cPtr = vehiclePINVOKE.RotationalDamperRWAssembly_GetShockTorqueCallback(swigCPtr);
    TorqueFunctor ret = (cPtr == global::System.IntPtr.Zero) ? null : new TorqueFunctor(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}