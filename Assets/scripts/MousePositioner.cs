using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MousePositioner : MonoBehaviour
{
    public static MousePositioner instance;
    [SerializeField] Camera CAMERA;
    Ray ray; RaycastHit castHit;

    void Start()
    {
        instance = this;    
    }

    void Update()
    {
        ray = CAMERA.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        Physics.Raycast(ray, out castHit);
        transform.position = castHit.point;
    }

    public static Vector3 GetPosition()
    {
        return instance.transform.position;
    }
}

