using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovments : MonoBehaviour
{
    public Player player;
    
    public float cameraSpeed;

    public float lastgroundY;

    public float distanceGround;

    public float maxDistanceGround;

    public float minDistanceGround;
    
    RaycastHit hit;
    
    void Start()
    {
        Gears.gears.mainCam = GetComponent<Camera>();
    }


    void Update()
    {
        Ray ray = new Ray(gameObject.transform.position, Vector3.down);//Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, Gears.gears.groundLayer))
        {
            lastgroundY = hit.point.y;
        }
        
        distanceGround -= Input.mouseScrollDelta.y;
            
        distanceGround = Mathf.Clamp(distanceGround, minDistanceGround, maxDistanceGround);
            
        transform.position = new Vector3(transform.position.x, lastgroundY + distanceGround, transform.position.z); //put in lateUpdate?
        
        if (Input.GetButton("Jump"))
        {
            transform.position = new Vector3(player.gameObject.transform.position.x, transform.position.y, 
                player.gameObject.transform.position.z - 25);
        }
        
        
        if (Input.GetButton("Q"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * cameraSpeed, Space.World);
        }
        if (Input.GetButton("D"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * cameraSpeed, Space.World);
        }
        if (Input.GetButton("Z"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * cameraSpeed, Space.World);
        }
        if (Input.GetButton("S"))
        {
            transform.Translate(Vector3.back * Time.deltaTime * cameraSpeed, Space.World);
        }
    }
}
