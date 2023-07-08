using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTom : DirectionJumpingEnemy
{
    public GameObject notFlyingTom;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void DamageBehaviour()
    {
        Debug.Log("Hi hi");
        GameObject nft = Instantiate<GameObject>(notFlyingTom, transform.position, transform.rotation, transform); //Echte NFTS! Echte NFTS! 
        nft.transform.parent = null;
        nft.GetComponent<Enemy>().SelfInvic(2f);
        gameObject.SetActive(false);
    }
}
