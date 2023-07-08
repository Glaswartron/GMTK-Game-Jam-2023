using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.GetComponent<Player>().transform.position.y > transform.position.y)
            {
                collision.transform.GetComponent<Player>().transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (collision.transform.GetComponent<Player>().transform.parent == transform)
            {
                collision.transform.GetComponent<Player>().transform.SetParent(null);
            }
        }
    }
}
