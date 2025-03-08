using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2_5 : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private Transform camTransform;

    private void Start()
    {
        camTransform = GetComponent<Transform>();
    }

    void Update()
    {
        camTransform.position = target.position + offset;
    }
}
