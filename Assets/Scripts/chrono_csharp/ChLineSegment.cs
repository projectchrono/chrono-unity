//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChLineSegment : ChLine {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChLineSegment(global::System.IntPtr cPtr, bool cMemoryOwn) : base(corePINVOKE.ChLineSegment_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChLineSegment obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          corePINVOKE.delete_ChLineSegment(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChVectorD pA {
    set {
      corePINVOKE.ChLineSegment_pA_set(swigCPtr, ChVectorD.getCPtr(value));
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = corePINVOKE.ChLineSegment_pA_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChVectorD pB {
    set {
      corePINVOKE.ChLineSegment_pB_set(swigCPtr, ChVectorD.getCPtr(value));
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = corePINVOKE.ChLineSegment_pB_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChLineSegment(ChVectorD mA, ChVectorD mB) : this(corePINVOKE.new_ChLineSegment__SWIG_0(ChVectorD.getCPtr(mA), ChVectorD.getCPtr(mB)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineSegment(ChVectorD mA) : this(corePINVOKE.new_ChLineSegment__SWIG_1(ChVectorD.getCPtr(mA)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineSegment() : this(corePINVOKE.new_ChLineSegment__SWIG_2(), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineSegment(ChLineSegment source) : this(corePINVOKE.new_ChLineSegment__SWIG_3(ChLineSegment.getCPtr(source)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override ChGeometry Clone() {
    global::System.IntPtr cPtr = corePINVOKE.ChLineSegment_Clone(swigCPtr);
    ChLineSegment ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChLineSegment(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChGeometry.GeometryType GetClassType() {
    ChGeometry.GeometryType ret = (ChGeometry.GeometryType)corePINVOKE.ChLineSegment_GetClassType(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int Get_complexity() {
    int ret = corePINVOKE.ChLineSegment_Get_complexity(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Evaluate(ChVectorD pos, double parU) {
    corePINVOKE.ChLineSegment_Evaluate(swigCPtr, ChVectorD.getCPtr(pos), parU);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double Length(int sampling) {
    double ret = corePINVOKE.ChLineSegment_Length(swigCPtr, sampling);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    corePINVOKE.ChLineSegment_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    corePINVOKE.ChLineSegment_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

}