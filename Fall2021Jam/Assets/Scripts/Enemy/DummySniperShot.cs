using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySniperShot: Hitbox
{
    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void FixedUpdate()
    {
        //destroy this gameobject after duration time
        setDuration(getDuration() - Time.deltaTime);
        if (getDuration() < 0)
        {
            Destroy(this.gameObject);
        }


        //Debug.Log("Hit Created!");
        //Get all the colliders overlapping with the overlapbox
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, transform.localScale, transform.eulerAngles.z);
        int i = 0;
        //iterate through hit colliders and apply the hit to enemies
        while (i < hitColliders.Length)
        {
            //Add hit detection for walls etc.
            if (hitColliders[i].gameObject.tag == "Player")
            {
                
                Vector3 knock = getKnock() * new Vector3(this.transform.up.x, this.transform.up.y, 0);
                Debug.Log("Hit Sent!");
                hitColliders[i].gameObject.GetComponent<Entity>().ApplyHit(getDamage(), knock);

                Destroy(this.gameObject);
            }
            i++;
        }
    }

}