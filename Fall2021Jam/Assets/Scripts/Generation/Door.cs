using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 GetCoord() { return this.coord; }
    public Vector2 GetDir() { return this.dir; }

    [SerializeField]
    private Vector2 coord;
    [SerializeField]
    private Vector2 dir;
}
