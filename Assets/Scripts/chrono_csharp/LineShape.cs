//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class LineShape : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal LineShape(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(LineShape obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~LineShape() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          vehiclePINVOKE.delete_LineShape(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public LineShape(ChVectorD pos, ChQuaternionD rot, SWIGTYPE_p_std__shared_ptrT_geometry__ChLine_t line) : this(vehiclePINVOKE.new_LineShape(ChVectorD.getCPtr(pos), ChQuaternionD.getCPtr(rot), SWIGTYPE_p_std__shared_ptrT_geometry__ChLine_t.getCPtr(line)), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public ChVectorD m_pos {
    set {
      vehiclePINVOKE.LineShape_m_pos_set(swigCPtr, ChVectorD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.LineShape_m_pos_get(swigCPtr);
      ChVectorD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChVectorD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public ChQuaternionD m_rot {
    set {
      vehiclePINVOKE.LineShape_m_rot_set(swigCPtr, ChQuaternionD.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.LineShape_m_rot_get(swigCPtr);
      ChQuaternionD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChQuaternionD(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SWIGTYPE_p_std__shared_ptrT_geometry__ChLine_t m_line {
    set {
      vehiclePINVOKE.LineShape_m_line_set(swigCPtr, SWIGTYPE_p_std__shared_ptrT_geometry__ChLine_t.getCPtr(value));
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      global::System.IntPtr cPtr = vehiclePINVOKE.LineShape_m_line_get(swigCPtr);
      SWIGTYPE_p_std__shared_ptrT_geometry__ChLine_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_std__shared_ptrT_geometry__ChLine_t(cPtr, false);
      if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

}
