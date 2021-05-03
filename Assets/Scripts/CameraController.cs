using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity;

    private float _vertical;
    private float _horizontal;

    private void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        _vertical = Input.GetAxis("Mouse Y");
        transform.Rotate(- Vector3.right * (_vertical * sensitivity));
    }
}
