using UnityEngine;
using System.Collections.Generic;
public class GridRenderer : MonoBehaviour
{
    public LineRenderer rowPrefab;
    public LineRenderer colPrefab;
    public int samplesPerSegment = 10;

    GridModel model;
    IDeformationField field;

    List<LineRenderer> rows = new();
    List<LineRenderer> cols = new();

    public void Init(GridModel m, IDeformationField f)
    {
        model = m;
        field = f;

        for (int j = 0; j < model.height; j++)
        {
            rows.Add(Instantiate(rowPrefab, transform));
        }

        for (int i = 0; i < model.width; i++)
        {
            cols.Add(Instantiate(colPrefab, transform));
        }
    }

    void Update()
    {
        for (int j = 0; j < model.height; j++)
        {
            var pts = field.SampleRow(j, samplesPerSegment);
            rows[j].positionCount = pts.Count;
            rows[j].SetPositions(pts.ToArray());
        }

        for (int i = 0; i < model.width; i++)
        {
            var pts = field.SampleColumn(i, samplesPerSegment);
            cols[i].positionCount = pts.Count;
            cols[i].SetPositions(pts.ToArray());
        }
    }
}
