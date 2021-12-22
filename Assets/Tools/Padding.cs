using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Padding : MonoBehaviour
{
    public float sensisitivity;

    public float limitsRange;

    public float maxZoom;

    public float zoomSensisitity;

    private Vector2 baseScale;

    private Vector2 startPos;

    private Vector2 min2;

    private Vector2 max2;

    void Start()
    {
        baseScale = transform.localScale;

        startPos = new Vector2(transform.position.x, transform.position.y);
    }
    
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            transform.Translate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensisitivity);
        }
        
        transform.localScale += new Vector3(Input.mouseScrollDelta.y * zoomSensisitity, Input.mouseScrollDelta.y * zoomSensisitity, 0);

        float diff = transform.localScale.x / baseScale.x;
        

        min2 = startPos + new Vector2(-1,-1) * limitsRange * diff;
        
        max2 = startPos + new Vector2(1,1) * limitsRange * diff;
        
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min2.x, max2.x), Mathf.Clamp(transform.position.y, min2.y, max2.y), transform.position.z);

        transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x, 0.5f, maxZoom),
            Mathf.Clamp(transform.localScale.y, 0.5f, maxZoom), transform.localScale.z);
    }
}
