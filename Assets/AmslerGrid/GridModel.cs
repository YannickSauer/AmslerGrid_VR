using UnityEngine;
public class GridModel
{
    public int width;
    public int height;

    public Vector2[,] baseGrid;
    public Vector2[,] deformedGrid;

    public GridModel(int w, int h, float spacing)
    {
        width = w;
        height = h;

        baseGrid = new Vector2[w, h];
        deformedGrid = new Vector2[w, h];

        float halfWidth = (w - 1) * spacing * 0.5f;
        float halfHeight = (h - 1) * spacing * 0.5f;

        for (int i = 0; i < w; i++)
        for (int j = 0; j < h; j++)
        {
            Vector2 p = new Vector2(
                i * spacing - halfWidth,
                j * spacing - halfHeight
            );

            baseGrid[i, j] = p;
            deformedGrid[i, j] = p;
        }
    }

    public void SetDisplacement(int i, int j, Vector2 d)
    {
        deformedGrid[i, j] = baseGrid[i, j] + d;
    }
}
