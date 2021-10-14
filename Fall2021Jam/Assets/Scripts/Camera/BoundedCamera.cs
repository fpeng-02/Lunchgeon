using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedCamera : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float yUpperBound;
    [SerializeField] private float yLowerBound;
    private float prevx;
    private float prevy;
    private float newx;
    private float newy;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        
        prevx = this.transform.position.x;
        prevy = this.transform.position.y;
        newx = this.transform.position.x;
        newy = this.transform.position.y;
        if (player.transform.position.x > prevx + xUpperBound) {
            newx = player.transform.position.x - xUpperBound;
        }
        if (player.transform.position.x < prevx + xLowerBound) {
            newx = player.transform.position.x - xLowerBound;
        }
        if (player.transform.position.y > prevy + yUpperBound) {
            newy = player.transform.position.y - yUpperBound;
        }
        if (player.transform.position.y < prevy + yLowerBound) {
            newy = player.transform.position.y - yLowerBound;
        }
        this.transform.position = Vector2.Lerp(this.transform.position, new Vector2(newx, newy), 0.1f);
    }
}
