using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public Player p;

    private float offset;
    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float s = p.GetCurrentVelo().x / p.speed;
        //s = 1;
        offset += (Time.deltaTime * s) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2(offset,0));

        transform.position = new Vector3(p.transform.position.x, transform.position.z);
    }
}
