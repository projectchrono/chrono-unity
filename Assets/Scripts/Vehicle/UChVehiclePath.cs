using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class UChVehiclePath : MonoBehaviour
{
    public enum Type { StraightLine, Circle, DoubleLaneChange, Custom, File }
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

    // Custom path
    public List<Vector3> points;

    // Path from file
    public string bezierCurveFile;

    private ChBezierCurve curve;
    public int numRenderPoints;

    public UChVehiclePath()
    {
        type = Type.StraightLine;
        lineStart = Vector3.zero;
        lineEnd = Vector3.one;
        circleNumTurns = 1;
        numRenderPoints = 100;
    }

    public ChBezierCurve GetChVehiclePath() { return curve; }

    private void Awake()
    {
        curve = ConstructBezierCurve();
    }

    private ChBezierCurve ConstructBezierCurve()
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
            case Type.Custom:
                {
                    List<Vector3> points_in = new List<Vector3>();
                    List<Vector3> points_out = new List<Vector3>();
                    ConstructControlPoints(points, points_in, points_out);

                    vector_ChVectorD p = new vector_ChVectorD();
                    vector_ChVectorD p_in = new vector_ChVectorD();
                    vector_ChVectorD p_out = new vector_ChVectorD();
                    for (int i = 0; i < points.Count; i++)
                    {
                        p.Add(Utils.ToChronoFlip(points[i]));
                        p_in.Add(Utils.ToChronoFlip(points_in[i]));
                        p_out.Add(Utils.ToChronoFlip(points_out[i]));
                    }
                    return new ChBezierCurve(p, p_in, p_out);
                }
            case Type.File:
                {
                    List<Vector3> pU = new List<Vector3>();
                    List<Vector3> pU_in = new List<Vector3>();
                    List<Vector3> pU_out = new List<Vector3>();

                    vector_ChVectorD p = new vector_ChVectorD();
                    vector_ChVectorD p_in = new vector_ChVectorD();
                    vector_ChVectorD p_out = new vector_ChVectorD();

                    string bezierCurveFile_full = Application.dataPath + "/" + bezierCurveFile;
                    if (File.Exists(bezierCurveFile_full))
                    {
                        using (TextReader reader = File.OpenText(bezierCurveFile_full))
                        {
                            string line;
                            float x;
                            float y;
                            float z;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] entries = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                float.TryParse(entries[0], out x);
                                float.TryParse(entries[1], out y);
                                float.TryParse(entries[2], out z);
                                pU.Add(new Vector3(x, y, z));
                            }
                            reader.Close();
                        }
                    }
                    else
                    {
                        // No file specified (yet?)
                        pU.Add(new Vector3(0, 0, 0));
                        pU.Add(new Vector3(1, 0, 0));
                    }

                    ConstructControlPoints(pU, pU_in, pU_out);

                    for (int i = 0; i < pU.Count; i++)
                    {
                        p.Add(Utils.ToChronoFlip(pU[i]));
                        p_in.Add(Utils.ToChronoFlip(pU_in[i]));
                        p_out.Add(Utils.ToChronoFlip(pU_out[i]));
                    }
                    
                    return new ChBezierCurve(p, p_in, p_out);
                }
        }
    }

    private void ConstructControlPoints(List<Vector3> pU, List<Vector3> pU_in, List<Vector3> pU_out)
    {
        float alpha = 0.1f;
        pU_in.Add(pU[0]);
        for (int i = 0; i < pU.Count - 1; i++)
        {
            var d = pU[i+1] - pU[i];
            pU_out.Add(pU[i] + alpha * d);
            pU_in.Add(pU[i + 1] - alpha * d);
        }
        pU_out.Add(pU[pU.Count - 1]);

    }

    public void UpdateLine()
    {
        var lineRenderer = this.GetComponent<LineRenderer>();
        ChBezierCurve c = ConstructBezierCurve();

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

        string[] options = new string[] { "Straight Line", "Circle", "Double Lane Change", "Custom", "File" };
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
            case UChVehiclePath.Type.Custom:
                int size = EditorGUILayout.DelayedIntField("Number of Points", path.points.Count);
                while (size < path.points.Count)
                    path.points.RemoveAt(path.points.Count - 1);
                while (size > path.points.Count)
                    path.points.Add(new Vector3());

                for (int i = 0; i < size; i++)
                {
                    path.points[i] = EditorGUILayout.Vector3Field("Point " + (i + 1), path.points[i]);
                }
                break;
            case UChVehiclePath.Type.File:
                path.bezierCurveFile = EditorGUILayout.TextField("File Name", path.bezierCurveFile);
                break;
        }

        EditorGUI.indentLevel--;

        path.numRenderPoints = EditorGUILayout.IntField("Num. Render Points", path.numRenderPoints);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(path);
            path.UpdateLine();
        }
    }
}
