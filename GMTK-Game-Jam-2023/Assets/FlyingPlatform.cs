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

    public bool waitAtPoints;
    public float waitingTime;

    private Transform target;
    private bool waiting = false;
    private bool semaphora = false;

    // Start is called before the first frame update
    void Start()
    {
        platform.position = start.position;

        target = end;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!waiting)
            platform.position = Vector2.MoveTowards(platform.position, target.position, speed * Time.fixedDeltaTime);

        if (Vector2.SqrMagnitude(target.position - platform.position) < 0.05f)
        {
            if (waitAtPoints && !semaphora)
            {
                waiting = true;
                semaphora = true;
                StartCoroutine(WaitAtPoint());
            }
            if (!waiting)
            {
                target = target == end ? start : end;
                semaphora = false;
            }
        }
    }

    private IEnumerator WaitAtPoint()
    {
        yield return new WaitForSeconds(waitingTime);
        waiting = false;
    }
}
