using System;
using System.Collections.Generic;
using UnityEngine;
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
    public bool hidePath;

    public UChVehiclePath()
    {
        type = Type.StraightLine;
        lineStart = Vector3.zero;
        lineEnd = Vector3.one;
        circleNumTurns = 1;
        numRenderPoints = 100;
        hidePath = false;
    }

    public ChBezierCurve GetChVehiclePath() { return curve; }

    private void Awake()
    {
        curve = ConstructBezierCurve();

        this.GetComponent<LineRenderer>().enabled = !hidePath;
    }

    private ChBezierCurve ConstructBezierCurve()
    {
        switch (type)
        {
            case Type.StraightLine:
            default:
                return chrono_vehicle.StraightLinePath(Utils.ToChrono(lineStart), Utils.ToChrono(lineEnd), lineNumPoints);
            case Type.Circle:
                return chrono_vehicle.CirclePath(Utils.ToChrono(circleStart), circleRadius, circleRun, circleLeftTurn, circleNumTurns);
            case Type.DoubleLaneChange:
                return chrono_vehicle.DoubleLaneChangePath(Utils.ToChrono(dlcStart), dlcRamp, dlcWidth, dlcLength, dlcRun, dlcLeftTurn);
            case Type.Custom:
                {
                    List<Vector3> points_in = new List<Vector3>();
                    List<Vector3> points_out = new List<Vector3>();
                    ConstructControlPoints(points, points_in, points_out);

                    vector_ChVector3d p = new vector_ChVector3d();
                    vector_ChVector3d p_in = new vector_ChVector3d();
                    vector_ChVector3d p_out = new vector_ChVector3d();
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

                    vector_ChVector3d p = new vector_ChVector3d();
                    vector_ChVector3d p_in = new vector_ChVector3d();
                    vector_ChVector3d p_out = new vector_ChVector3d();

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
        float alpha = 0.5f;
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
