//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class MAN_10t_Chassis : ChRigidChassis {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnDerived;

  internal MAN_10t_Chassis(global::System.IntPtr cPtr, bool cMemoryOwn) : base(vehiclePINVOKE.MAN_10t_Chassis_SWIGSmartPtrUpcast(cPtr), true) {
    swigCMemOwnDerived = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MAN_10t_Chassis obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  protected override void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnDerived) {
          swigCMemOwnDerived = false;
          vehiclePINVOKE.delete_MAN_10t_Chassis(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      base.Dispose(disposing);
    }
  }

  public MAN_10t_Chassis(string name, bool fixed_, CollisionType chassis_collision_type) : this(vehiclePINVOKE.new_MAN_10t_Chassis__SWIG_0(name, fixed_, (int)chassis_collision_type), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public MAN_10t_Chassis(string name, bool fixed_) : this(vehiclePINVOKE.new_MAN_10t_Chassis__SWIG_1(name, fixed_), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public MAN_10t_Chassis(string name) : this(vehiclePINVOKE.new_MAN_10t_Chassis__SWIG_2(name), true) {
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public override double GetMass() {
    double ret = vehiclePINVOKE.MAN_10t_Chassis_GetMass(swigCPtr);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChMatrix33D GetInertia() {
    ChMatrix33D ret = new ChMatrix33D(vehiclePINVOKE.MAN_10t_Chassis_GetInertia(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChVectorD GetLocalPosCOM() {
    ChVectorD ret = new ChVectorD(vehiclePINVOKE.MAN_10t_Chassis_GetLocalPosCOM(swigCPtr), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public override ChCoordsysD GetLocalDriverCoordsys() {
    ChCoordsysD ret = new ChCoordsysD(vehiclePINVOKE.MAN_10t_Chassis_GetLocalDriverCoordsys(swigCPtr), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
