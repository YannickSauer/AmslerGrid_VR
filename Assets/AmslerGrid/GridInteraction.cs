using UnityEngine;

public class GridInteraction : MonoBehaviour
{
    public Camera cam;
    public float pickRadius = 0.2f;
    public float cursorGain= 1.0f;
    GridModel model;
    int selI = -1, selJ = -1;
    public GameObject cursor;

    public void Init(GridModel m)
    {
        model = m;
    }

    void Update()
    {

        // Draw cursor point on plane
        if(cursor != null)
        {
            
            Vector3 pos = GetPointerPlanePosition();
            Debug.Log($"Cursor plane pos: {pos}");
            pos.z = transform.localPosition.z;
            cursor.transform.localPosition = pos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse down, picking point...");
            PickPoint();
        }
            

        if (Input.GetMouseButton(0) && selI != -1)
            DragPoint();

        if(Input.GetMouseButtonUp(0))
        {
            selI = -1;
            selJ = -1;
        }
    }

    void PickPoint()
    {
        Vector2 mouse = GetPointerPlanePosition();

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
        Vector2 mouse = GetPointerPlanePosition();
        Vector2 baseP = model.baseGrid[selI, selJ];
        model.deformedGrid[selI, selJ] = mouse;
    }

     Vector2 GetPointerPlanePosition()
    {

        Ray worldRay = cam.ScreenPointToRay(Input.mousePosition);
        
        Debug.DrawRay(worldRay.origin, worldRay.direction * 100, Color.green);
        // Convert ray to camera-local space
        Vector3 localOrigin = cam.transform.InverseTransformPoint(worldRay.origin);
        Vector3 localDir = cam.transform.InverseTransformDirection(worldRay.direction);

        Ray localRay = new Ray(Vector3.zero, localDir);
        // Assume grid lies in XY plane with z equal to z of this GameObject
        Plane plane = new Plane(Vector3.forward, new Vector3(0,0,6)); // plane in local coordinates

        if (plane.Raycast(localRay, out float enter))
        {
            Vector3 hit = localRay.GetPoint(enter);
            return cursorGain * new Vector2(hit.x, hit.y);
        }
        
    
        return Vector2.zero;
    }


}
