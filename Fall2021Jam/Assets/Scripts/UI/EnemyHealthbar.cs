using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// To have healthbars over enemies, they need to use World Space, 
// which needs a camera; this subclass just sets that up.
// (By enemies, I mean small fry; boses might have their own screen-overlaying healthbar)
public class EnemyHealthbar : Healthbar
{
    public Canvas canvas;
    protected override void Start()
    {
        base.Start();
        canvas.worldCamera = Camera.main;
    }
}
