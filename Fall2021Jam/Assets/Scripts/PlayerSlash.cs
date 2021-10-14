using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : Hitbox
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hit Created!");
        //Get all the colliders overlapping with the overlapbox
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, transform.localScale, transform.eulerAngles.z);
        int i = 0;
        //iterate through hit colliders and apply the hit to enemies
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                Vector3 knock = getKnock() * Vector3.Normalize(hitColliders[i].gameObject.transform.position - transform.parent.transform.position);
                //Debug.Log("Hit Sent!");
                hitColliders[i].gameObject.GetComponent<Entity>().ApplyHit(getDamage(), knock);
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //destroy this gameobject after duration time
        setDuration(getDuration() - Time.deltaTime);
        if (getDuration() < 0)
        {
            Destroy(this.gameObject);
        }
    }

}
