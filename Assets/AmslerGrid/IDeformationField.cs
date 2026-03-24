using UnityEngine;
using System.Collections.Generic;
public interface IDeformationField
{
    //Vector2 Evaluate(float x, float y);   // future: for texture warp
    List<Vector3> SampleRow(int j, int samplesPerSegment);
    List<Vector3> SampleColumn(int i, int samplesPerSegment);
}
