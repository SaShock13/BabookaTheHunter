using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Movement3_D : MonoBehaviour, IMovable
{

    private Vector3 moveDirection;
    [SerializeField] private Transform cameraTransform;
    private CharacterController characterController;
    [SerializeField] private float runSpeed = 2;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    public void Move(Vector3 direction)
    {
        moveDirection = cameraTransform.TransformDirection(direction);
        characterController.Move(moveDirection * runSpeed * Time.deltaTime);
        animator.SetFloat("Speed", (moveDirection.magnitude * 5) > 0.01f ? moveDirection.magnitude * 5 : 0);
    }
}
