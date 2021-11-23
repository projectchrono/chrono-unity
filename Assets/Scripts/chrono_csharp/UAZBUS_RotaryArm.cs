//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class UAZBUS_RotaryArm : ChRotaryArm {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal UAZBUS_RotaryArm(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.UAZBUS_RotaryArm_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(UAZBUS_RotaryArm obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_UAZBUS_RotaryArm(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public UAZBUS_RotaryArm(string name) : this(vehiclePINVOKE.new_UAZBUS_RotaryArm(name), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual double getPitmanArmMass() {
    double ret = vehiclePINVOKE.UAZBUS_RotaryArm_getPitmanArmMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual double getPitmanArmRadius() {
    double ret = vehiclePINVOKE.UAZBUS_RotaryArm_getPitmanArmRadius(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD getPitmanArmInertiaMoments() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.UAZBUS_RotaryArm_getPitmanArmInertiaMoments(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD getPitmanArmInertiaProducts() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.UAZBUS_RotaryArm_getPitmanArmInertiaProducts(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual double getMaxAngle() {
    double ret = vehiclePINVOKE.UAZBUS_RotaryArm_getMaxAngle(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
