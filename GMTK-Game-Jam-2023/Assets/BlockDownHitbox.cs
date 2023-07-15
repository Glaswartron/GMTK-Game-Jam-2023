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
                Debug.Log("Players y: " + Mathf.Abs(collision.transform.position.y));
                Debug.Log("Eigenes global y: " + Mathf.Abs(this.transform.parent.position.y));
                Debug.Log("Eigenes lokales y: " + Mathf.Abs(this.transform.localPosition.y));

                if ((collision.transform.position.y) > (this.transform.parent.position.y))
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

}
