using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float sensivity;
    public float jumpHeight;
    public KeyCode jumpKey;

    private Rigidbody _rigidbody;
    private float _mouseInput;
    private float _fwdInput;
    private bool _isGrounded;
    private const float g = 9.8f;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _mouseInput = Input.GetAxis("Mouse X");
        _fwdInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * (_fwdInput * speed * Time.deltaTime), Space.Self);
        transform.Rotate(Vector3.up * (_mouseInput * sensivity * Time.deltaTime));

        if (Input.GetKey(jumpKey)) {
            if (_isGrounded)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * Mathf.Sqrt(g) *_rigidbody.mass * jumpHeight , ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Ground") {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = false;            
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }
}

