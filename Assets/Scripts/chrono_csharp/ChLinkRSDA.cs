//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChLinkRSDA : ChLink {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChLinkRSDA(global::System.IntPtr cPtr, bool cMemoryOwn) : base(corePINVOKE.ChLinkRSDA_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChLinkRSDA obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          corePINVOKE.delete_ChLinkRSDA(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChLinkRSDA() : this(corePINVOKE.new_ChLinkRSDA__SWIG_0(), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLinkRSDA(ChLinkRSDA other) : this(corePINVOKE.new_ChLinkRSDA__SWIG_1(ChLinkRSDA.getCPtr(other)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetSpringCoefficient(double k) {
    corePINVOKE.ChLinkRSDA_SetSpringCoefficient(swigCPtr, k);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetDampingCoefficient(double r) {
    corePINVOKE.ChLinkRSDA_SetDampingCoefficient(swigCPtr, r);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetActuatorTorque(double t) {
    corePINVOKE.ChLinkRSDA_SetActuatorTorque(swigCPtr, t);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRestAngle(double rest_angle) {
    corePINVOKE.ChLinkRSDA_SetRestAngle(swigCPtr, rest_angle);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetNumInitRevolutions(int n) {
    corePINVOKE.ChLinkRSDA_SetNumInitRevolutions(swigCPtr, n);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RegisterTorqueFunctor(TorqueFunctor functor) {
    corePINVOKE.ChLinkRSDA_RegisterTorqueFunctor__SWIG_0(swigCPtr, TorqueFunctor.getCPtr(functor));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public double GetRestAngle() {
    double ret = corePINVOKE.ChLinkRSDA_GetRestAngle(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetAngle() {
    double ret = corePINVOKE.ChLinkRSDA_GetAngle(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetDeformation() {
    double ret = corePINVOKE.ChLinkRSDA_GetDeformation(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetVelocity() {
    double ret = corePINVOKE.ChLinkRSDA_GetVelocity(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public double GetTorque() {
    double ret = corePINVOKE.ChLinkRSDA_GetTorque(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChCoordsysD GetLinkRelativeCoords() {
    ChCoordsysD ret = new ChCoordsysD(corePINVOKE.ChLinkRSDA_GetLinkRelativeCoords(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetVisualModelFrame(uint nclone) {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChLinkRSDA_GetVisualModelFrame__SWIG_0(swigCPtr, nclone), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChFrameD GetVisualModelFrame() {
    ChFrameD ret = new ChFrameD(corePINVOKE.ChLinkRSDA_GetVisualModelFrame__SWIG_1(swigCPtr), true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Initialize(ChBody body1, ChBody body2, ChCoordsysD csys) {
    corePINVOKE.ChLinkRSDA_Initialize__SWIG_0(swigCPtr, ChBody.getCPtr(body1), ChBody.getCPtr(body2), ChCoordsysD.getCPtr(csys));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Initialize(ChBody body1, ChBody body2, bool local, ChCoordsysD csys1, ChCoordsysD csys2) {
    corePINVOKE.ChLinkRSDA_Initialize__SWIG_1(swigCPtr, ChBody.getCPtr(body1), ChBody.getCPtr(body2), local, ChCoordsysD.getCPtr(csys1), ChCoordsysD.getCPtr(csys2));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    corePINVOKE.ChLinkRSDA_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    corePINVOKE.ChLinkRSDA_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RegisterTorqueFunctor(SWIGTYPE_p_std__shared_ptrT_RSDATorqueFunctor_t functor) {
    corePINVOKE.ChLinkRSDA_RegisterTorqueFunctor__SWIG_1(swigCPtr, SWIGTYPE_p_std__shared_ptrT_RSDATorqueFunctor_t.getCPtr(functor));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

}
