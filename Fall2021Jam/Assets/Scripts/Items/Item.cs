using UnityEngine;

public abstract class Item : ScriptableObject
{
    [field: SerializeField]
    public Sprite ItemSprite {get; set;}

    [field: SerializeField]
    public int StackCapacity {get; set;}
}
