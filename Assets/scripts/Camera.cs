using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    void Update()
    {
        transform.parent.Translate(Input.GetAxisRaw("Horizontal")*Time.deltaTime*moveSpeed, 0, Input.GetAxisRaw("Vertical")*Time.deltaTime*moveSpeed);
    }
}
