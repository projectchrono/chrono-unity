//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChLoadXYZnodeXYZnodeBushing : ChLoadXYZnodeXYZnode {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChLoadXYZnodeXYZnodeBushing(global::System.IntPtr cPtr, bool cMemoryOwn) : base(ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChLoadXYZnodeXYZnodeBushing obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          ChronoEngine_csharpPINVOKE.delete_ChLoadXYZnodeXYZnodeBushing(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChLoadXYZnodeXYZnodeBushing(SWIGTYPE_p_std__shared_ptrT_ChNodeXYZ_t mnodeA, SWIGTYPE_p_std__shared_ptrT_ChNodeXYZ_t mnodeB) : this(ChronoEngine_csharpPINVOKE.new_ChLoadXYZnodeXYZnodeBushing(SWIGTYPE_p_std__shared_ptrT_ChNodeXYZ_t.getCPtr(mnodeA), SWIGTYPE_p_std__shared_ptrT_ChNodeXYZ_t.getCPtr(mnodeB)), true) {
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public override ChObj Clone() {
    global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_Clone(swigCPtr);
    ChLoadXYZnodeXYZnodeBushing ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChLoadXYZnodeXYZnodeBushing(cPtr, true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ComputeForce(ChVectorD rel_pos, ChVectorD rel_vel, ChVectorD abs_force) {
    ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_ComputeForce(swigCPtr, ChVectorD.getCPtr(rel_pos), ChVectorD.getCPtr(rel_vel), ChVectorD.getCPtr(abs_force));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetFunctionForceX(ChFunction fx) {
    ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_SetFunctionForceX(swigCPtr, ChFunction.getCPtr(fx));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetFunctionForceY(ChFunction fy) {
    ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_SetFunctionForceY(swigCPtr, ChFunction.getCPtr(fy));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetFunctionForceZ(ChFunction fz) {
    ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_SetFunctionForceZ(swigCPtr, ChFunction.getCPtr(fz));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetDamping(ChVectorD mdamping) {
    ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_SetDamping(swigCPtr, ChVectorD.getCPtr(mdamping));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD GetDamping() {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_GetDamping(swigCPtr), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetStiff(bool ms) {
    ChronoEngine_csharpPINVOKE.ChLoadXYZnodeXYZnodeBushing_SetStiff(swigCPtr, ms);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

}
