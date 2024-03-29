// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution and at
// http://projectchrono.org/license-chrono.txt.
//
// =============================================================================
// Authors: Radu Serban
// =============================================================================

using UnityEngine;
using UnityEditor;

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
