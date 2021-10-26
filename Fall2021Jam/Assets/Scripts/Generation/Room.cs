using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private List<Door> doors;

    [SerializeField]
    private Vector2 size;  // size.x = width, size.y = height (in meta-coords)
}
