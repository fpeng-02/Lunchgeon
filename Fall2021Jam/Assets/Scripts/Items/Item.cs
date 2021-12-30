using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int stackCapacity;

    public int GetStackCapacity() { return stackCapacity; }
    public Sprite GetSprite() { return sprite; }
}
