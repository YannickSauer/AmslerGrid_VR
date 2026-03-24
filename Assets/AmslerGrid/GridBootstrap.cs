using UnityEngine;

public class GridBootstrap : MonoBehaviour
{
    public GridRenderer renderer;
    public GridInteraction interaction;
    public enum FieldType { CatmullRom, LocalHermite }
    public FieldType fieldType;

    void Start()
    {
        var model = new GridModel(10, 10, 1.0f);
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
