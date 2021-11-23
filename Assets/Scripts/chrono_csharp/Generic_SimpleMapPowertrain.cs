//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class Generic_SimpleMapPowertrain : ChPowertrain {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal Generic_SimpleMapPowertrain(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.Generic_SimpleMapPowertrain_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Generic_SimpleMapPowertrain obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_Generic_SimpleMapPowertrain(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public Generic_SimpleMapPowertrain(string name) : this(vehiclePINVOKE.new_Generic_SimpleMapPowertrain(name), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override string GetTemplateName() {
    string ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetTemplateName(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetMotorSpeed() {
    double ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetMotorSpeed(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetMotorTorque() {
    double ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetMotorTorque(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetTorqueConverterSlippage() {
    double ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetTorqueConverterSlippage(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetTorqueConverterInputTorque() {
    double ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetTorqueConverterInputTorque(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetTorqueConverterOutputTorque() {
    double ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetTorqueConverterOutputTorque(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetTorqueConverterOutputSpeed() {
    double ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetTorqueConverterOutputSpeed(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override double GetOutputTorque() {
    double ret = vehiclePINVOKE.Generic_SimpleMapPowertrain_GetOutputTorque(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
