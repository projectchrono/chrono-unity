//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ChLineArc : ChLine {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal ChLineArc(global::System.IntPtr cPtr, bool cMemoryOwn) : base(corePINVOKE.ChLineArc_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ChLineArc obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          corePINVOKE.delete_ChLineArc(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public ChCoordsysD origin {
    set {
      corePINVOKE.ChLineArc_origin_set(swigCPtr, ChCoordsysD.getCPtr(value));
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = corePINVOKE.ChLineArc_origin_get(swigCPtr);
      ChCoordsysD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChCoordsysD(cPtr, false);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double radius {
    set {
      corePINVOKE.ChLineArc_radius_set(swigCPtr, value);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = corePINVOKE.ChLineArc_radius_get(swigCPtr);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double angle1 {
    set {
      corePINVOKE.ChLineArc_angle1_set(swigCPtr, value);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = corePINVOKE.ChLineArc_angle1_get(swigCPtr);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public double angle2 {
    set {
      corePINVOKE.ChLineArc_angle2_set(swigCPtr, value);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      double ret = corePINVOKE.ChLineArc_angle2_get(swigCPtr);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public bool counterclockwise {
    set {
      corePINVOKE.ChLineArc_counterclockwise_set(swigCPtr, value);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      bool ret = corePINVOKE.ChLineArc_counterclockwise_get(swigCPtr);
      if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChLineArc(ChCoordsysD morigin, double mradius, double mangle1, double mangle2, bool mcounterclockwise) : this(corePINVOKE.new_ChLineArc__SWIG_0(ChCoordsysD.getCPtr(morigin), mradius, mangle1, mangle2, mcounterclockwise), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineArc(ChCoordsysD morigin, double mradius, double mangle1, double mangle2) : this(corePINVOKE.new_ChLineArc__SWIG_1(ChCoordsysD.getCPtr(morigin), mradius, mangle1, mangle2), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineArc(ChCoordsysD morigin, double mradius, double mangle1) : this(corePINVOKE.new_ChLineArc__SWIG_2(ChCoordsysD.getCPtr(morigin), mradius, mangle1), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineArc(ChCoordsysD morigin, double mradius) : this(corePINVOKE.new_ChLineArc__SWIG_3(ChCoordsysD.getCPtr(morigin), mradius), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineArc(ChCoordsysD morigin) : this(corePINVOKE.new_ChLineArc__SWIG_4(ChCoordsysD.getCPtr(morigin)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineArc() : this(corePINVOKE.new_ChLineArc__SWIG_5(), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChLineArc(ChLineArc source) : this(corePINVOKE.new_ChLineArc__SWIG_6(ChLineArc.getCPtr(source)), true) {
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override ChGeometry Clone() {
    global::System.IntPtr cPtr = corePINVOKE.ChLineArc_Clone(swigCPtr);
    ChLineArc ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChLineArc(cPtr, true);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChGeometry.GeometryType GetClassType() {
    ChGeometry.GeometryType ret = (ChGeometry.GeometryType)corePINVOKE.ChLineArc_GetClassType(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override int Get_complexity() {
    int ret = corePINVOKE.ChLineArc_Get_complexity(swigCPtr);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override void Evaluate(ChVectorD pos, double parU) {
    corePINVOKE.ChLineArc_Evaluate(swigCPtr, ChVectorD.getCPtr(pos), parU);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double Length(int sampling) {
    double ret = corePINVOKE.ChLineArc_Length(swigCPtr, sampling);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void SetCounterclockwise(bool mcc) {
    corePINVOKE.ChLineArc_SetCounterclockwise(swigCPtr, mcc);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetAngle1deg(double a1) {
    corePINVOKE.ChLineArc_SetAngle1deg(swigCPtr, a1);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetAngle2deg(double a2) {
    corePINVOKE.ChLineArc_SetAngle2deg(swigCPtr, a2);
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveOUT(SWIGTYPE_p_ChArchiveOut marchive) {
    corePINVOKE.ChLineArc_ArchiveOUT(swigCPtr, SWIGTYPE_p_ChArchiveOut.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

  public override void ArchiveIN(SWIGTYPE_p_chrono__ChArchiveIn marchive) {
    corePINVOKE.ChLineArc_ArchiveIN(swigCPtr, SWIGTYPE_p_chrono__ChArchiveIn.getCPtr(marchive));
    if (corePINVOKE.SWIGPendingException.Pending) throw corePINVOKE.SWIGPendingException.Retrieve();
  }

}
