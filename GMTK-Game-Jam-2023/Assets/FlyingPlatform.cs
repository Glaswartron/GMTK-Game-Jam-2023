using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform start;
    public Transform end;
    public float speed;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        platform.position = start.position;

        target = end;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        platform.position = Vector2.MoveTowards(platform.position, target.position, speed * Time.fixedDeltaTime);

        if (Vector2.SqrMagnitude(target.position - platform.position) < 0.05f)
        {
            target = target == end ? start : end;
        }
    }
}
