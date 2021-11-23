//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class vehicle {
  public static void SetDataPath(string path) {
    vehiclePINVOKE.SetDataPath(path);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
  }

  public static string GetDataPath() {
    string ret = vehiclePINVOKE.GetDataPath();
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetDataFile(string filename) {
    string ret = vehiclePINVOKE.GetDataFile(filename);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SWIGTYPE_p_std__vectorT_std__string_t splitStr(string s, char delim, SWIGTYPE_p_std__vectorT_std__string_t elems) {
    SWIGTYPE_p_std__vectorT_std__string_t ret = new SWIGTYPE_p_std__vectorT_std__string_t(vehiclePINVOKE.splitStr__SWIG_0(s, delim, SWIGTYPE_p_std__vectorT_std__string_t.getCPtr(elems)), false);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SWIGTYPE_p_std__vectorT_std__string_t splitStr(string s, char delim) {
    SWIGTYPE_p_std__vectorT_std__string_t ret = new SWIGTYPE_p_std__vectorT_std__string_t(vehiclePINVOKE.splitStr__SWIG_1(s, delim), true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChBezierCurve StraightLinePath(ChVectorD start, ChVectorD end, uint num_intermediate) {
    global::System.IntPtr cPtr = vehiclePINVOKE.StraightLinePath__SWIG_0(ChVectorD.getCPtr(start), ChVectorD.getCPtr(end), num_intermediate);
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChBezierCurve StraightLinePath(ChVectorD start, ChVectorD end) {
    global::System.IntPtr cPtr = vehiclePINVOKE.StraightLinePath__SWIG_1(ChVectorD.getCPtr(start), ChVectorD.getCPtr(end));
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChBezierCurve CirclePath(ChVectorD start, double radius, double run, bool left_turn, int num_turns) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CirclePath__SWIG_0(ChVectorD.getCPtr(start), radius, run, left_turn, num_turns);
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChBezierCurve CirclePath(ChVectorD start, double radius, double run, bool left_turn) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CirclePath__SWIG_1(ChVectorD.getCPtr(start), radius, run, left_turn);
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChBezierCurve CirclePath(ChVectorD start, double radius, double run) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CirclePath__SWIG_2(ChVectorD.getCPtr(start), radius, run);
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChBezierCurve DoubleLaneChangePath(ChVectorD start, double ramp, double width, double length, double run, bool left_turn) {
    global::System.IntPtr cPtr = vehiclePINVOKE.DoubleLaneChangePath__SWIG_0(ChVectorD.getCPtr(start), ramp, width, length, run, left_turn);
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChBezierCurve DoubleLaneChangePath(ChVectorD start, double ramp, double width, double length, double run) {
    global::System.IntPtr cPtr = vehiclePINVOKE.DoubleLaneChangePath__SWIG_1(ChVectorD.getCPtr(start), ramp, width, length, run);
    ChBezierCurve ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChBezierCurve(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChDoubleWishbone CastToChDoubleWishbone(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChDoubleWishbone(ChSuspension.getCPtr(in_obj));
    ChDoubleWishbone ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChDoubleWishbone(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChMacPhersonStrut CastToChMacPhersonStrut(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChMacPhersonStrut(ChSuspension.getCPtr(in_obj));
    ChMacPhersonStrut ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChMacPhersonStrut(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChLeafspringAxle CastToChLeafspringAxle(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChLeafspringAxle(ChSuspension.getCPtr(in_obj));
    ChLeafspringAxle ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChLeafspringAxle(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChHendricksonPRIMAXX CastToChHendricksonPRIMAXX(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChHendricksonPRIMAXX(ChSuspension.getCPtr(in_obj));
    ChHendricksonPRIMAXX ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChHendricksonPRIMAXX(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChDoubleWishboneReduced CastToChDoubleWishboneReduced(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChDoubleWishboneReduced(ChSuspension.getCPtr(in_obj));
    ChDoubleWishboneReduced ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChDoubleWishboneReduced(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChMultiLink CastToChMultiLink(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChMultiLink(ChSuspension.getCPtr(in_obj));
    ChMultiLink ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChMultiLink(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChRigidPinnedAxle CastToChRigidPinnedAxle(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChRigidPinnedAxle(ChSuspension.getCPtr(in_obj));
    ChRigidPinnedAxle ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRigidPinnedAxle(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChSemiTrailingArm CastToChSemiTrailingArm(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChSemiTrailingArm(ChSuspension.getCPtr(in_obj));
    ChSemiTrailingArm ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSemiTrailingArm(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChRigidSuspension CastToChRigidSuspension(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChRigidSuspension(ChSuspension.getCPtr(in_obj));
    ChRigidSuspension ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRigidSuspension(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChSolidAxle CastToChSolidAxle(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChSolidAxle(ChSuspension.getCPtr(in_obj));
    ChSolidAxle ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSolidAxle(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChThreeLinkIRS CastToChThreeLinkIRS(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChThreeLinkIRS(ChSuspension.getCPtr(in_obj));
    ChThreeLinkIRS ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChThreeLinkIRS(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChToeBarLeafspringAxle CastToChToeBarLeafspringAxle(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChToeBarLeafspringAxle(ChSuspension.getCPtr(in_obj));
    ChToeBarLeafspringAxle ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChToeBarLeafspringAxle(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChSolidBellcrankThreeLinkAxle CastToChSolidBellcrankThreeLinkAxle(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChSolidBellcrankThreeLinkAxle(ChSuspension.getCPtr(in_obj));
    ChSolidBellcrankThreeLinkAxle ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSolidBellcrankThreeLinkAxle(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChSolidThreeLinkAxle CastToChSolidThreeLinkAxle(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChSolidThreeLinkAxle(ChSuspension.getCPtr(in_obj));
    ChSolidThreeLinkAxle ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSolidThreeLinkAxle(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChSingleWishbone CastToChSingleWishbone(ChSuspension in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChSingleWishbone(ChSuspension.getCPtr(in_obj));
    ChSingleWishbone ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSingleWishbone(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChPitmanArm CastToChPitmanArm(ChSteering in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChPitmanArm(ChSteering.getCPtr(in_obj));
    ChPitmanArm ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPitmanArm(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChPitmanArmShafts CastToChPitmanArmShafts(ChSteering in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChPitmanArmShafts(ChSteering.getCPtr(in_obj));
    ChPitmanArmShafts ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPitmanArmShafts(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChRackPinion CastToChRackPinion(ChSteering in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChRackPinion(ChSteering.getCPtr(in_obj));
    ChRackPinion ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRackPinion(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChRotaryArm CastToChRotaryArm(ChSteering in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChRotaryArm(ChSteering.getCPtr(in_obj));
    ChRotaryArm ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRotaryArm(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChRigidChassis CastToChRigidChassis(ChChassis in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChRigidChassis(ChChassis.getCPtr(in_obj));
    ChRigidChassis ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRigidChassis(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChRigidChassisRear CastToChRigidChassisRear(ChChassisRear in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChRigidChassisRear(ChChassisRear.getCPtr(in_obj));
    ChRigidChassisRear ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRigidChassisRear(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChChassisConnectorArticulated CastToChChassisConnectorArticulated(ChChassisConnector in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChChassisConnectorArticulated(ChChassisConnector.getCPtr(in_obj));
    ChChassisConnectorArticulated ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChChassisConnectorArticulated(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChChassisConnectorHitch CastToChChassisConnectorHitch(ChChassisConnector in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChChassisConnectorHitch(ChChassisConnector.getCPtr(in_obj));
    ChChassisConnectorHitch ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChChassisConnectorHitch(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChChassisConnectorTorsion CastToChChassisConnectorTorsion(ChChassisConnector in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChChassisConnectorTorsion(ChChassisConnector.getCPtr(in_obj));
    ChChassisConnectorTorsion ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChChassisConnectorTorsion(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChTMeasyTire CastToChTMeasyTire(ChTire in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChTMeasyTire(ChTire.getCPtr(in_obj));
    ChTMeasyTire ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChTMeasyTire(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChRigidTire CastToChRigidTire(ChTire in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChRigidTire(ChTire.getCPtr(in_obj));
    ChRigidTire ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChRigidTire(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SWIGTYPE_p_chrono__vehicle__ChReissnerTire CastToChReissnerTire(ChTire in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChReissnerTire(ChTire.getCPtr(in_obj));
    SWIGTYPE_p_chrono__vehicle__ChReissnerTire ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_chrono__vehicle__ChReissnerTire(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChPacejkaTire CastToChPacejkaTire(ChTire in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChPacejkaTire(ChTire.getCPtr(in_obj));
    ChPacejkaTire ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPacejkaTire(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChPac89Tire CastToChPac89Tire(ChTire in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChPac89Tire(ChTire.getCPtr(in_obj));
    ChPac89Tire ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChPac89Tire(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChLugreTire CastToChLugreTire(ChTire in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChLugreTire(ChTire.getCPtr(in_obj));
    ChLugreTire ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChLugreTire(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChFialaTire CastToChFialaTire(ChTire in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChFialaTire(ChTire.getCPtr(in_obj));
    ChFialaTire ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChFialaTire(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SimplePowertrain CastToSimplePowertrain(ChPowertrain in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToSimplePowertrain(ChPowertrain.getCPtr(in_obj));
    SimplePowertrain ret = (cPtr == global::System.IntPtr.Zero) ? null : new SimplePowertrain(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SimpleMapPowertrain CastToSimpleMapPowertrain(ChPowertrain in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToSimpleMapPowertrain(ChPowertrain.getCPtr(in_obj));
    SimpleMapPowertrain ret = (cPtr == global::System.IntPtr.Zero) ? null : new SimpleMapPowertrain(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static SimpleCVTPowertrain CastToSimpleCVTPowertrain(ChPowertrain in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToSimpleCVTPowertrain(ChPowertrain.getCPtr(in_obj));
    SimpleCVTPowertrain ret = (cPtr == global::System.IntPtr.Zero) ? null : new SimpleCVTPowertrain(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ShaftsPowertrain CastToShaftsPowertrain(ChPowertrain in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToShaftsPowertrain(ChPowertrain.getCPtr(in_obj));
    ShaftsPowertrain ret = (cPtr == global::System.IntPtr.Zero) ? null : new ShaftsPowertrain(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChDrivelineWV CastToChDrivelineWV(ChDriveline in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChDrivelineWV(ChDriveline.getCPtr(in_obj));
    ChDrivelineWV ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChDrivelineWV(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChShaftsDriveline2WD CastToChShaftsDriveline2WD(ChDriveline in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChShaftsDriveline2WD(ChDriveline.getCPtr(in_obj));
    ChShaftsDriveline2WD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChShaftsDriveline2WD(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChShaftsDriveline4WD CastToChShaftsDriveline4WD(ChDriveline in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChShaftsDriveline4WD(ChDriveline.getCPtr(in_obj));
    ChShaftsDriveline4WD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChShaftsDriveline4WD(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChSimpleDriveline CastToChSimpleDriveline(ChDriveline in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChSimpleDriveline(ChDriveline.getCPtr(in_obj));
    ChSimpleDriveline ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSimpleDriveline(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static ChSimpleDrivelineXWD CastToChSimpleDrivelineXWD(ChDriveline in_obj) {
    global::System.IntPtr cPtr = vehiclePINVOKE.CastToChSimpleDrivelineXWD(ChDriveline.getCPtr(in_obj));
    ChSimpleDrivelineXWD ret = (cPtr == global::System.IntPtr.Zero) ? null : new ChSimpleDrivelineXWD(cPtr, true);
    if (vehiclePINVOKE.SWIGPendingException.Pending) throw vehiclePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}
