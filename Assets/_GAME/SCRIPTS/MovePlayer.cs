using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    //Transform player;

    [SerializeField] private float speed = 2;
    [SerializeField] private float jumpHieght = 2;
    private Rigidbody rb;

    private void Start()
    {
        //transform.position = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        //transform.position += direction * speed * Time.deltaTime;
    }

    internal void Jump()
    {
        //rb.isKinematic = false;
        //rb.velocity = Vector3.up * jumpHieght;
    }
}
