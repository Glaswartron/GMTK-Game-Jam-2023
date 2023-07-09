using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : Item
{
    public float time = 10f;

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
        player.EnableDoubleJump(time);
        player.GainHP();
    }
}
