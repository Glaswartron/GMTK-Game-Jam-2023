using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float BounceValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            collision.GetComponent<Player>().BoostUp(BounceValue);
            GetComponent<Animator>().Play("Bounce");
        }
    }

}
