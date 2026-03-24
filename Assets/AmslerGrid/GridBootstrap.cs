using UnityEngine;

public class GridBootstrap : MonoBehaviour
{
    public int nRows = 10;
    public int nCols = 10;
    public float spacing = 1.0f;
    public GridRenderer renderer;
    public GridInteraction interaction;
    public enum FieldType { CatmullRom, LocalHermite }
    public FieldType fieldType;

    void Start()
    {
        var model = new GridModel(nCols, nRows, spacing);
        IDeformationField field = fieldType switch
        {
            FieldType.CatmullRom => new CatmullRomField(model),
            FieldType.LocalHermite => new LocalHermiteField(model, 0.5f),
            _ => throw new System.Exception("Unknown field type")
        };

        renderer.Init(model, field);
        interaction.Init(model);
    }
}
