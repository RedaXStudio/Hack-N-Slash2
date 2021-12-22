using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraBeh : MonoBehaviour
{
    public Player player;

    public bool turnWithThePlayer;

    public float maxZoom;
    public float minZoom;

    void Start()
    {
        
    }

   
    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            if (turnWithThePlayer)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
            }
        }
    }

    public void Zoom(float zoomValue)
    {
        GetComponent<Camera>().orthographicSize += zoomValue;

        GetComponent<Camera>().orthographicSize =
            Mathf.Clamp(GetComponent<Camera>().orthographicSize, minZoom, maxZoom);
    }
}
