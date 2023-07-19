using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDownHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BlockBreaker")
        {

            if (collision.GetComponentInParent<Player>() != null)
            {
                if (transform.GetComponentInParent<Block>().IsBlocked())
                {
                    Debug.Log("ALARM!!!");
                    return;
                }

            }
            var block = GetComponentInParent<Block>();
            if (block.destructable)
            {
                GetComponentInParent<Block>().Break();
            }
            else if(block.isItemBox)
            {
                block.HitItemBox();
                //this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() != null)
        {
            if (transform.GetComponentInParent<Block>().IsBlocked())
            {
                transform.GetComponentInParent<Block>().UnBlockBlock();
            }
        }
    }

}
