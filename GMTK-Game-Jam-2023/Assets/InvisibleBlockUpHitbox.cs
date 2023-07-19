using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBlockUpHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            transform.GetComponentInParent<Block>().BlockBlock();
        }
    }

}
