using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChSystem : MonoBehaviour
{
    // ATTENTION: The global Chrono system is set in UChSystem.Awake.
    // Since Unity does not enforce an order of calls to Awake,
    // this can be safely used by other components only in their Start function.

    public static ChSystem chrono_system;

    public Vector3 gravity;
    public double step;

    // -----------------------------
    // Contact method

    public ChContactMethod contact_method;

    // SMC settings
    public ChSystemSMC.ContactForceModel contactModel;
    public ChSystemSMC.AdhesionForceModel adhesionModel;
    public ChSystemSMC.TangentialDisplacementModel tdisplModel;
    public bool useMatProps;

    // -----------------------------
    // Solver

    // Supported solver types
    public enum SolverType
    {
        PSOR,
        BARZILAIBORWEIN,
        APGD,
        SPARSE_LU,
        SPARSE_QR,
        GMRES,
        MINRES
    }
    public SolverType solverType;

    // Iterative solver settings
    public int solverMaxIterations;
    public double solverTolerance;
    public bool solverEnablePrecond;
    public bool solverEnableWarmStart;

    // Direct solver settings
    public bool solverUseLearner;
    public bool solverLockSparsityPattern;
    public double solverSparsityEstimate;

    // -----------------------------
    // Integrator

    // Supported integrator types
    public enum IntegratorType
    {
        EULER_IMPLICIT_LINEARIZED,
        EULER_IMPLICIT_PROJECTED,
        EULER_IMPLICIT,
        HHT
    }
    public IntegratorType integratorType;

    // Implicit integrator settings
    public int integratorMaxIters;
    public double integratorRelTol;
    public double integratorAbsTolS;
    public double integratorAbsTolL;

    // HHT settings
    public double hhtAlpha;
    public bool hhtScaling;


    // -----------------------------
    public UChSystem()
    {
        gravity = new Vector3(0, -9.8f, 0);
        step = 1e-2;
        
        useMatProps = true;

        solverType = SolverType.PSOR;

        solverMaxIterations = 50;
        solverTolerance = 0;
        solverEnablePrecond = true;
        solverEnableWarmStart = false;

        solverUseLearner = true;
        solverLockSparsityPattern = false;
        solverSparsityEstimate = 0.9;

        integratorMaxIters = 10;
        integratorRelTol = 1e-4;
        integratorAbsTolS = 1e-8;
        integratorAbsTolL = 1e-8;

        hhtScaling = true;
        hhtAlpha = -0.2;

        integratorType = IntegratorType.EULER_IMPLICIT_LINEARIZED;
    }

    // -----------------------------
    void Awake()
    {
        switch (contact_method)
        {
            case ChContactMethod.NSC:
                chrono_system = new ChSystemNSC();
                break;
            case ChContactMethod.SMC:
                chrono_system = new ChSystemSMC(useMatProps);
                break;
        }


        Debug.Log("SOLVER: " + solverType);
        Debug.Log("INTEGRATOR: " + integratorType);

        
        // Set solver
        switch (solverType)
        {
            case SolverType.PSOR:
                {
                    var solver = new ChSolverPSOR();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.BARZILAIBORWEIN:
                {
                    var solver = new ChSolverBB();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.APGD:
                {
                    var solver = new ChSolverAPGD();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.SPARSE_LU:
                {
                    var solver = new ChSolverSparseLU();
                    solver.UseSparsityPatternLearner(solverUseLearner);
                    solver.LockSparsityPattern(solverLockSparsityPattern);
                    solver.SetSparsityEstimate(solverSparsityEstimate);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.SPARSE_QR:
                {
                    var solver = new ChSolverSparseQR();
                    solver.UseSparsityPatternLearner(solverUseLearner);
                    solver.LockSparsityPattern(solverLockSparsityPattern);
                    solver.SetSparsityEstimate(solverSparsityEstimate);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.GMRES:
                {
                    var solver = new ChSolverGMRES();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
            case SolverType.MINRES:
                {
                    var solver = new ChSolverMINRES();
                    solver.SetMaxIterations(solverMaxIterations);
                    solver.SetTolerance(solverTolerance);
                    solver.EnableDiagonalPreconditioner(solverEnablePrecond);
                    solver.EnableWarmStart(solverEnableWarmStart);
                    chrono_system.SetSolver(solver);
                    break;
                }
        }

        //// TODO:  Something wrong with setting the system's integrator ==> crash

        /*
        // Set integrator
        switch (integratorType)
        {
            case IntegratorType.EULER_IMPLICIT_LINEARIZED:
                {
                    var integrator = new ChTimestepperEulerImplicitLinearized();
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
            case IntegratorType.EULER_IMPLICIT_PROJECTED:
                {
                    var integrator = new ChTimestepperEulerImplicitProjected();
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
            case IntegratorType.EULER_IMPLICIT:
                {
                    var integrator = new ChTimestepperEulerImplicit();
                    integrator.SetMaxiters(integratorMaxIters);
                    integrator.SetRelTolerance(integratorRelTol);
                    integrator.SetAbsTolerances(integratorAbsTolS, integratorAbsTolL);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
            case IntegratorType.HHT:
                {
                    var integrator = new ChTimestepperHHT();
                    integrator.SetMode(ChTimestepperHHT.HHT_Mode.ACCELERATION);
                    integrator.SetMaxiters(integratorMaxIters);
                    integrator.SetRelTolerance(integratorRelTol);
                    integrator.SetAbsTolerances(integratorAbsTolS, integratorAbsTolL);
                    integrator.SetAlpha(hhtAlpha);
                    integrator.SetScaling(hhtScaling);
                    chrono_system.SetTimestepper(integrator);
                    break;
                }
        }
        */

        chrono_system.Set_G_acc(new ChVectorD(gravity.x, gravity.y, gravity.z));
        chrono_system.SetStep(step);
    }

    // -----------------------------
    void FixedUpdate()
    {
        Time.fixedDeltaTime = (float)step;

        ////Debug.Log("sys Time = " + chrono_system.GetChTime() + "     num bodies: " + chrono_system.GetNbodies());
        chrono_system.DoStepDynamics(step);
    }

    // -----------------------------
    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}

// ==========================================================================================================

[CustomEditor(typeof(UChSystem))]
public class UChSystemEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChSystem sys = (UChSystem)target;

        sys.step = EditorGUILayout.DoubleField("Step", sys.step);
        sys.gravity = EditorGUILayout.Vector3Field("Gravity", sys.gravity);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Contact method options

        string[] options = new string[] { "NSC", "SCM" };
        sys.contact_method = (ChContactMethod)EditorGUILayout.Popup("Contact Method", (int)sys.contact_method, options, EditorStyles.popup);

        if (sys.contact_method == ChContactMethod.SMC)
        {
            string[] force_options = new string[] { "Hooke", "Hertz", "PlainCoulomb" };  // "Flores"
            string[] adhesion_options = new string[] { "Constant", "DMT" }; // "Perko"
            string[] tdispl_options = new string[] { "None", "OneStep", "MultiStep" };
            //EditorGUI.indentLevel++;
            sys.contactModel = (ChSystemSMC.ContactForceModel)EditorGUILayout.Popup("Force Model", (int)sys.contactModel, force_options, EditorStyles.popup);
            sys.adhesionModel = (ChSystemSMC.AdhesionForceModel)EditorGUILayout.Popup("Adhesion Model", (int)sys.adhesionModel, adhesion_options, EditorStyles.popup);
            sys.tdisplModel = (ChSystemSMC.TangentialDisplacementModel)EditorGUILayout.Popup("Tangential Model", (int)sys.tdisplModel, tdispl_options, EditorStyles.popup);
            sys.useMatProps = EditorGUILayout.Toggle("Use Mat. Props.", sys.useMatProps);
            //EditorGUI.indentLevel--;
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Solver options

        string[] solver_options;
        if (sys.contact_method == ChContactMethod.NSC)
            solver_options = new string[] { "PSOR", "BARZILAIBORWEIN", "APGD" };
        else
            solver_options = new string[] { "PSOR", "BARZILAIBORWEIN", "APGD",
                                            "SPARSE_LU", "SPARSE_QR", "GMRES", "MINRES" };
        sys.solverType = (UChSystem.SolverType)EditorGUILayout.Popup("Solver Type", (int)sys.solverType, solver_options, EditorStyles.popup);

        if (sys.solverType == UChSystem.SolverType.SPARSE_LU || sys.solverType == UChSystem.SolverType.SPARSE_QR)
        {
            sys.solverUseLearner = EditorGUILayout.Toggle("Sparsity Learner", sys.solverUseLearner);
            sys.solverLockSparsityPattern = EditorGUILayout.Toggle("Lock Sparsity", sys.solverLockSparsityPattern);
            sys.solverSparsityEstimate = EditorGUILayout.DoubleField("Sparsity Estimate", sys.solverSparsityEstimate);
        }
        else
        {
            sys.solverMaxIterations = EditorGUILayout.IntField("Max. Iterations", sys.solverMaxIterations);
            sys.solverTolerance = EditorGUILayout.DoubleField("Tolerance", sys.solverTolerance);
            sys.solverEnablePrecond = EditorGUILayout.Toggle("Diag. Preconditioner", sys.solverEnablePrecond);
            sys.solverEnableWarmStart = EditorGUILayout.Toggle("Warm Start", sys.solverEnableWarmStart);
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Integrator options

        string[] integrator_options;
        if (sys.contact_method == ChContactMethod.NSC)
            integrator_options = new string[] { "EULER_IMPLICIT_LINEARIZED", "EULER_IMPLICIT_PROJECTED", "EULER_IMPLICIT" };
        else
            integrator_options = new string[] { "EULER_IMPLICIT_LINEARIZED", "EULER_IMPLICIT_PROJECTED", "EULER_IMPLICIT", "HHT" };

        sys.integratorType = (UChSystem.IntegratorType)EditorGUILayout.Popup("Solver Type", (int)sys.integratorType, integrator_options, EditorStyles.popup);

        if (sys.integratorType == UChSystem.IntegratorType.EULER_IMPLICIT || sys.integratorType == UChSystem.IntegratorType.HHT)
        {
            sys.integratorRelTol = EditorGUILayout.DoubleField("Rel. Tol.", sys.integratorRelTol);
            sys.integratorAbsTolS = EditorGUILayout.DoubleField("Abs. Tol. States", sys.integratorAbsTolS);
            sys.integratorAbsTolL = EditorGUILayout.DoubleField("Abs. Tol. Multipliers", sys.integratorAbsTolL);
            sys.solverMaxIterations = EditorGUILayout.IntField("Max. N-R Iterations", sys.solverMaxIterations);
        }

        if (sys.integratorType == UChSystem.IntegratorType.HHT)
        {
            sys.hhtAlpha = EditorGUILayout.DoubleField("HHT Alpha", sys.hhtAlpha);
            sys.hhtScaling = EditorGUILayout.Toggle("HHT Scaling", sys.hhtScaling);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(sys);
        }
    }
}