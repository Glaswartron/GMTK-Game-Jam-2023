using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : Item
{
    public float speedMultiplier = 1.5f;
    public float time = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ApplyEffect(Player player)
    {
        player.SpeedUp(speedMultiplier, time);
    }

}
