using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : MonoBehaviour
{
    public bool instakill;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            var p = collision.GetComponent<Player>();
            p.TakeHit();
            if (instakill)
            {
                for (int i = 0; i < p.health; i++)
                {
                    p.TakeHit(true);
                }
            }
        }
    }

}
