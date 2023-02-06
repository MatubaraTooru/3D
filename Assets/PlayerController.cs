using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 3;
    Rigidbody _rb;
    float _h;
    float _v;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 dir = Vector3.forward * _v + Vector3.right * _h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir != Vector3.zero) this.transform.forward = dir;
        dir = dir.normalized * _moveSpeed;
        float y = _rb.velocity.y;

        _rb.velocity = dir * _moveSpeed + Vector3.up * y;
    }
}
