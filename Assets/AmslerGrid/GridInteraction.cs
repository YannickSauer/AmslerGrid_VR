using UnityEngine;

public class GridInteraction : MonoBehaviour
{
    public Camera cam;
    public float pickRadius = 0.2f;

    GridModel model;
    int selI = -1, selJ = -1;

    public void Init(GridModel m)
    {
        model = m;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PickPoint();

        if (Input.GetMouseButton(0) && selI != -1)
            DragPoint();
    }

    void PickPoint()
    {
        Vector2 mouse = cam.ScreenToWorldPoint(Input.mousePosition);

        float best = float.MaxValue;

        for (int i = 0; i < model.width; i++)
        for (int j = 0; j < model.height; j++)
        {
            float d = Vector2.Distance(mouse, model.deformedGrid[i, j]);
            if (d < pickRadius && d < best)
            {
                best = d;
                selI = i;
                selJ = j;
            }
        }
    }

    void DragPoint()
    {
        Vector2 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 baseP = model.baseGrid[selI, selJ];
        model.deformedGrid[selI, selJ] = mouse;
    }
}
