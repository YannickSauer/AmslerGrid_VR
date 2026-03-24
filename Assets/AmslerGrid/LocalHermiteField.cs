using UnityEngine;
using System.Collections.Generic;

public class LocalHermiteField : IDeformationField
{
    GridModel grid;
    float tangentScale;

    public LocalHermiteField(GridModel model, float tangentScale = 0.5f)
    {
        grid = model;
        this.tangentScale = tangentScale;
    }

    Vector2 Hermite(Vector2 p0, Vector2 p1, Vector2 t0, Vector2 t1, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float h00 =  2*t3 - 3*t2 + 1;
        float h10 =      t3 - 2*t2 + t;
        float h01 = -2*t3 + 3*t2;
        float h11 =      t3 - t2;

        return h00 * p0 + h10 * t0 + h01 * p1 + h11 * t1;
    }

    Vector2 LocalTangent(Vector2 p0, Vector2 p1)
    {
        return tangentScale * (p1 - p0);
    }

    public List<Vector3> SampleRow(int j, int samplesPerSegment)
    {
        var pts = new List<Vector3>();

        for (int i = 0; i < grid.width - 1; i++)
        {
            Vector2 p0 = grid.deformedGrid[i, j];
            Vector2 p1 = grid.deformedGrid[i + 1, j];

            Vector2 t0 = LocalTangent(p0, p1);
            Vector2 t1 = LocalTangent(p0, p1);

            for (int s = 0; s <= samplesPerSegment; s++)
            {
                float t = s / (float)samplesPerSegment;
                pts.Add(Hermite(p0, p1, t0, t1, t));
            }
        }

        return pts;
    }

    public List<Vector3> SampleColumn(int i, int samplesPerSegment)
    {
        var pts = new List<Vector3>();

        for (int j = 0; j < grid.height - 1; j++)
        {
            Vector2 p0 = grid.deformedGrid[i, j];
            Vector2 p1 = grid.deformedGrid[i, j + 1];

            Vector2 t0 = LocalTangent(p0, p1);
            Vector2 t1 = LocalTangent(p0, p1);

            for (int s = 0; s <= samplesPerSegment; s++)
            {
                float t = s / (float)samplesPerSegment;
                pts.Add(Hermite(p0, p1, t0, t1, t));
            }
        }

        return pts;
    }

    //public Vector2 Evaluate(float x, float y)
    //{
        // Placeholder: when you switch to bicubic 2D, this becomes real
    //    return new Vector2(x, y);
    //}

    public void OnGridChanged() { }
}
