using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool destructable;
    public bool isItemBox;
    public bool itemBoxSpawningItemBox;
    public GameObject item;
    public GameObject itemSpawnPoint;

    public void Break()
    {
        GetComponent<Animator>().Play("Break"); //Am Ende der Animation muss das GO deaktiviert werden
    }

    public void Deaktivate()
    {
        gameObject.SetActive(false);
    }

    public void HitItemBox()
    {
        isItemBox = false;
        destructable = false;
        GetComponent<Animator>().Play("Item");
    }

    public void SpawnItem()
    {
        var i = Instantiate<GameObject>(item, itemSpawnPoint.transform.position, itemSpawnPoint.transform.rotation);
        if(itemBoxSpawningItemBox)
        {
            i.GetComponent<Block>().isItemBox = true;
        }
    }
}
