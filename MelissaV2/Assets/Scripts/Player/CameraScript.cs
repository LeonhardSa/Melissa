using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    
    public Transform target, player;
    
    public Transform Obstruction;
        
    private Transform cam;

    float sensitivity = 16f;
    float minFov = 50;
    float maxFov = 100;
    float offsetY = 10f;
    float offsetX = 4f;
    float offsetZ = 0f;


    void Start()
    {
        Obstruction = player;
        cam = Camera.main.transform;
        cam.position = target.position + Vector3.up * offsetY + Vector3.back * offsetX + offsetZ * Vector3.left;
        
    }

    void FixedUpdate()
    {
        transform.LookAt(target);
        Zoom();
        //ViewObstructed();
    }


    void Zoom()
    {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
