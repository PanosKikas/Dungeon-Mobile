using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string Name;
    [TextAreaAttribute]
    public string Description;
    public Sprite Icon;
}
