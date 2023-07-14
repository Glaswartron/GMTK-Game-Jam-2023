using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCamPosition;

    [SerializeField]
    private Vector2 parallaxDelay;
    private float textureUnitSizeX;

    public bool moveConstantly;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCamPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D tex = sprite.texture;
        textureUnitSizeX = tex.width / sprite.pixelsPerUnit;
        Debug.Log("textureUnitSize: " + textureUnitSizeX);


    }

    private void LateUpdate()
    {
        if (!moveConstantly)
        {
            Vector3 deltaMovement = cameraTransform.position - lastCamPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxDelay.x, deltaMovement.y * parallaxDelay.y);
            lastCamPosition = cameraTransform.position;
        }
        else
        {
            transform.position += (Vector3.left * Time.deltaTime * parallaxDelay.x);
        }
        /*
        if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
        */
    }
}
