using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyHeadCollider : MonoBehaviour
{
    private Enemy enemy;

    private EnemySideCollider[] sideColliders;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();

        sideColliders = enemy.GetComponentsInChildren<EnemySideCollider>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null && !player.IsGrounded())
        {
            Debug.Log("WAAAAAAA");
            enemy.TakeHit();

            player.BoostUp();

            if (enemy.HP <= 0)
            {
                sideColliders.ToList().ForEach(sc => sc.gameObject.SetActive(false));
                this.gameObject.SetActive(false);
            }
        }
    }
}
