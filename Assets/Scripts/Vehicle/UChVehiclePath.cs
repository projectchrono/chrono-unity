﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChVehiclePath : MonoBehaviour
{
    public enum Type { StraightLine, Circle, DoubleLaneChange }
    public Type type;

    // Straight line
    public Vector3 lineStart;
    public Vector3 lineEnd;
    public uint lineNumPoints;

    // Circle path
    public Vector3 circleStart;
    public double circleRadius;
    public double circleRun;
    public bool circleLeftTurn;
    public int circleNumTurns;

    // Double lane change
    public Vector3 dlcStart;
    public double dlcRamp;
    public double dlcWidth;
    public double dlcLength;
    public double dlcRun;
    public bool dlcLeftTurn;

    private ChBezierCurve curve;
    public int numRenderPoints;

    public UChVehiclePath()
    {
        type = Type.StraightLine;
        circleNumTurns = 1;
        numRenderPoints = 100;
    }

    public ChBezierCurve GetChVehiclePath() { return curve; }

    private void Awake()
    {
        UpdateCurve();
    }

    private void Start()
    {
    }

    private ChBezierCurve ConstructCurve()
    {
        switch (type)
        {
            case Type.StraightLine:
            default:
                return vehicle.StraightLinePath(Utils.ToChrono(lineStart), Utils.ToChrono(lineEnd), lineNumPoints);
            case Type.Circle:
                return vehicle.CirclePath(Utils.ToChrono(circleStart), circleRadius, circleRun, circleLeftTurn, circleNumTurns);
            case Type.DoubleLaneChange:
                return vehicle.DoubleLaneChangePath(Utils.ToChrono(dlcStart), dlcRamp, dlcWidth, dlcLength, dlcRun, dlcLeftTurn);
        }
    }

    public void UpdateCurve()
    {
        var lineRenderer = this.GetComponent<LineRenderer>();
        ChBezierCurve c = ConstructCurve();

        double delta = 1.0 / numRenderPoints;
        var points = new Vector3[numRenderPoints];
        for (int i = 0; i < numRenderPoints; i++)
            points[i] = Utils.FromChronoFlip(c.eval(delta * i));

        lineRenderer.positionCount = numRenderPoints;
        lineRenderer.SetPositions(points);
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}

[CustomEditor(typeof(UChVehiclePath))]
public class UChVehiclePathEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChVehiclePath path = (UChVehiclePath)target;

        string[] options = new string[] { "Straight Line", "Circle", "Double Lane Change" };
        path.type = (UChVehiclePath.Type)EditorGUILayout.Popup("Type", (int)path.type, options, EditorStyles.popup);

        EditorGUI.indentLevel++;
        switch (path.type)
        {
            case UChVehiclePath.Type.StraightLine:
                path.lineStart = EditorGUILayout.Vector3Field("Start point", path.lineStart);
                path.lineEnd = EditorGUILayout.Vector3Field("End point", path.lineEnd);
                path.lineNumPoints = (uint)EditorGUILayout.IntField("Num. Intermediate", (int)path.lineNumPoints);
                break;
            case UChVehiclePath.Type.Circle:
                path.circleStart = EditorGUILayout.Vector3Field("Start point", path.circleStart);
                path.circleRadius = EditorGUILayout.DoubleField("Radius", path.circleRadius);
                path.circleRun = EditorGUILayout.DoubleField("Run", path.circleRun);
                path.circleNumTurns = EditorGUILayout.IntField("Num. Turns", path.circleNumTurns);
                path.circleLeftTurn = EditorGUILayout.Toggle("Left Turn", path.circleLeftTurn);
                break;
            case UChVehiclePath.Type.DoubleLaneChange:
                path.dlcStart = EditorGUILayout.Vector3Field("Start point", path.dlcStart);
                path.dlcRamp = EditorGUILayout.DoubleField("Ramp", path.dlcRamp);
                path.dlcWidth = EditorGUILayout.DoubleField("Width", path.dlcWidth);
                path.dlcLength = EditorGUILayout.DoubleField("Length", path.dlcLength);
                path.dlcRun = EditorGUILayout.DoubleField("Run", path.dlcRun);
                path.dlcLeftTurn = EditorGUILayout.Toggle("Left Turn", path.dlcLeftTurn);
                break;
        }
        EditorGUI.indentLevel--;

        path.numRenderPoints = EditorGUILayout.IntField("Num. Render Points", path.numRenderPoints);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(path);
            path.UpdateCurve();
        }
    }
}
