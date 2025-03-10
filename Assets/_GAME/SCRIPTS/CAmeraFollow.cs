using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CAmeraFollow : MonoBehaviour
{
    public Transform targetTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float verticalLookOffset = 1f;
    
    [SerializeField] private float targetTurnSpeed = 2;
    private Transform lookTransform;
    private float rotationY,rotationX;

    private void OnEnable()
    {
        InputPlayer.OnMouseMoveEvent += OnMouseMove;
    }

    private void OnDisable()
    {
        InputPlayer.OnMouseMoveEvent -= OnMouseMove;
    }

    void Start()
    {
        lookTransform = transform;
        rotationY = 0;
        rotationX = lookTransform.rotation.x;
    }

    void Update()
    {
        lookTransform.position = targetTransform.position;
    }

    public void OnMouseMove(float mouseX, float mouseY)
    {
        rotationY -= mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -30, 50);
        lookTransform.localRotation = Quaternion.Euler(rotationX, rotationY * -1, 0);
    }

}
