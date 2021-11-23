//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChFunctionPosition : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal ChFunctionPosition(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChFunctionPosition obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ChFunctionPosition() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnBase) {
          swigCMemOwnBase = false;
          ChronoEngine_csharpPINVOKE.delete_ChFunctionPosition(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public virtual ChFunctionPosition Clone() {
    global::System.IntPtr cPtr = ChronoEngine_csharpPINVOKE.ChFunctionPosition_Clone(swigCPtr);
    ChFunctionPosition ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChFunctionPosition(cPtr, true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD Get_p(double s) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFunctionPosition_Get_p(swigCPtr, s), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD Get_p_ds(double s) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFunctionPosition_Get_p_ds(swigCPtr, s), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual ChVectorD Get_p_dsds(double s) {
    ChVectorD ret = new ChVectorD(ChronoEngine_csharpPINVOKE.ChFunctionPosition_Get_p_dsds(swigCPtr, s), true);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual void Estimate_s_domain(SWIGTYPE_p_double smin, SWIGTYPE_p_double smax) {
    ChronoEngine_csharpPINVOKE.ChFunctionPosition_Estimate_s_domain(swigCPtr, SWIGTYPE_p_double.getCPtr(smin), SWIGTYPE_p_double.getCPtr(smax));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Estimate_boundingbox(ChVectorD pmin, ChVectorD pmax) {
    ChronoEngine_csharpPINVOKE.ChFunctionPosition_Estimate_boundingbox(swigCPtr, ChVectorD.getCPtr(pmin), ChVectorD.getCPtr(pmax));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void Update(double t) {
    ChronoEngine_csharpPINVOKE.ChFunctionPosition_Update(swigCPtr, t);
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    ChronoEngine_csharpPINVOKE.ChFunctionPosition_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    ChronoEngine_csharpPINVOKE.ChFunctionPosition_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (ChronoEngine_csharpPINVOKE.SWIGPendingException.Pending) throw ChronoEngine_csharpPINVOKE.SWIGPendingException.Retrieve();
  }

}
