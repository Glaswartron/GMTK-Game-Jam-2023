using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marty : DirectionJumpingEnemy
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        base.Start();
    }

    override protected void Update()
    {
        base.Update();
    }

}
