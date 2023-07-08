using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool destructable;

    public void Break()
    {
        GetComponent<Animator>().Play("Break"); //Am Ende der Animation muss das GO deaktiviert werden
    }

    public void Deaktivate()
    {
        gameObject.SetActive(false);
    }
}
