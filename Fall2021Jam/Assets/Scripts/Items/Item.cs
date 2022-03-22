using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
    [field: SerializeField]
    public string ItemName {get; set;}

    [field: SerializeField]
    public Sprite ItemSprite {get; set;}

    [field: SerializeField]
    public int StackCapacity {get; set;}
}
