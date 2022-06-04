//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class LoadLoaderForceOnSurface : ChLoadBase {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal LoadLoaderForceOnSurface(global::System.IntPtr cPtr, bool cMemoryOwn) : base(corePINVOKE.LoadLoaderForceOnSurface_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LoadLoaderForceOnSurface obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          corePINVOKE.delete_LoadLoaderForceOnSurface(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChLoaderForceOnSurface loader {
    set {
      corePINVOKE.LoadLoaderForceOnSurface_loader_set(swigCPtr, ChLoaderForceOnSurface.getCPtr(value));
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      ChLoaderForceOnSurface ret = new ChLoaderForceOnSurface(corePINVOKE.LoadLoaderForceOnSurface_loader_get(swigCPtr), true);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public LoadLoaderForceOnSurface(ChLoadableUV mloadable) : this(corePINVOKE.new_LoadLoaderForceOnSurface(ChLoadableUV.getCPtr(mloadable)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override int LoadGet_ndof_x() {
    int ret = corePINVOKE.LoadLoaderForceOnSurface_LoadGet_ndof_x(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int LoadGet_ndof_w() {
    int ret = corePINVOKE.LoadLoaderForceOnSurface_LoadGet_ndof_w(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void LoadGetStateBlock_x(ChState mD) {
    corePINVOKE.LoadLoaderForceOnSurface_LoadGetStateBlock_x(swigCPtr, ChState.getCPtr(mD));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void LoadGetStateBlock_w(ChStateDelta mD) {
    corePINVOKE.LoadLoaderForceOnSurface_LoadGetStateBlock_w(swigCPtr, ChStateDelta.getCPtr(mD));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void LoadStateIncrement(ChState x, ChStateDelta dw, ChState x_new) {
    corePINVOKE.LoadLoaderForceOnSurface_LoadStateIncrement(swigCPtr, ChState.getCPtr(x), ChStateDelta.getCPtr(dw), ChState.getCPtr(x_new));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override int LoadGet_field_ncoords() {
    int ret = corePINVOKE.LoadLoaderForceOnSurface_LoadGet_field_ncoords(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ComputeQ(ChState state_x, ChStateDelta state_w) {
    corePINVOKE.LoadLoaderForceOnSurface_ComputeQ(swigCPtr, ChState.getCPtr(state_x), ChStateDelta.getCPtr(state_w));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void LoadIntLoadResidual_F(ChVectorDynamicD R, double c) {
    corePINVOKE.LoadLoaderForceOnSurface_LoadIntLoadResidual_F(swigCPtr, ChVectorDynamicD.getCPtr(R), c);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void LoadIntLoadResidual_Mv(ChVectorDynamicD R, ChVectorDynamicD w, double c) {
    corePINVOKE.LoadLoaderForceOnSurface_LoadIntLoadResidual_Mv(swigCPtr, ChVectorDynamicD.getCPtr(R), ChVectorDynamicD.getCPtr(w), c);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override bool IsStiff() {
    bool ret = corePINVOKE.LoadLoaderForceOnSurface_IsStiff(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void CreateJacobianMatrices() {
    corePINVOKE.LoadLoaderForceOnSurface_CreateJacobianMatrices(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

}
