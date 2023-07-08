using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bongo : Enemy
{
    public float angerSpeedIncreasement;
    protected override void DamageBehaviour()
    {
        GetComponent<Animator>().Play("BongoHit");
        invictus = true;
        doesDamage = false;
        movement = Vector2.zero;
    }

    public void AngerBongo()
    {
        GetComponent<Animator>().Play("BongoWalkAngry");
        speed *= angerSpeedIncreasement;
        invictus = false;
        doesDamage = true;
    }

    public override void ChangeDirection()
    {
        base.ChangeDirection();
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        foreach(Transform child in transform)
        {
            float y = (child.rotation.y == 0) ? 180 : 0;
            double z = (child.GetComponent<CapsuleCollider2D>() != null)? -57.722 : child.rotation.z;

            child.eulerAngles = new Vector3(child.rotation.x, y, (float)z);

        }
    }


}
