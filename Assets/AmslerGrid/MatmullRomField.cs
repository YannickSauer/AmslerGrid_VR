using UnityEngine;
using System.Collections.Generic;

public class CatmullRomField : IDeformationField
{
    GridModel grid;

    public CatmullRomField(GridModel model)
    {
        grid = model;
    }

    Vector2 Catmull(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * (
            (2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3
        );
    }

    public List<Vector3> SampleRow(int j, int samples)
    {
        List<Vector3> pts = new();

        for (int i = 0; i < grid.width - 1; i++)
        {
            Vector2 p0 = grid.deformedGrid[Mathf.Max(i - 1, 0), j];
            Vector2 p1 = grid.deformedGrid[i, j];
            Vector2 p2 = grid.deformedGrid[i + 1, j];
            Vector2 p3 = grid.deformedGrid[Mathf.Min(i + 2, grid.width - 1), j];

            for (int s = 0; s < samples; s++)
            {
                float t = s / (float)samples;
                pts.Add(Catmull(p0, p1, p2, p3, t));
            }
        }

        return pts;
    }

    public List<Vector3> SampleColumn(int i, int samples)
    {
        List<Vector3> pts = new();

        for (int j = 0; j < grid.height - 1; j++)
        {
            Vector2 p0 = grid.deformedGrid[i, Mathf.Max(j - 1, 0)];
            Vector2 p1 = grid.deformedGrid[i, j];
            Vector2 p2 = grid.deformedGrid[i, j + 1];
            Vector2 p3 = grid.deformedGrid[i, Mathf.Min(j + 2, grid.height - 1)];

            for (int s = 0; s < samples; s++)
            {
                float t = s / (float)samples;
                pts.Add(Catmull(p0, p1, p2, p3, t));
            }
        }

        return pts;
    }

    // Stub for future texture warp compatibility
    //public Vector2 Evaluate(float x, float y)
    //{
    //    // Later: bicubic field implementation
    //    return new Vector2(x, y);
    //}
}
