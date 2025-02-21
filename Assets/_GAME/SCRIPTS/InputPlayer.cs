using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    private Animator animator;
    private float xInput;
    private float yInput;
    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.zero;
    private float mouseX, mouseY;

    public event Action OnTorchPressedEvent;
    public event Action OnAttack;

    [SerializeField] private UnityEvent <Vector3> onMove = null;
    [SerializeField] private UnityEvent onJump = null;
    [SerializeField] private UnityEvent onTorch = null;
    [SerializeField] private UnityEvent <float,float> onMouseMove = null;
    [SerializeField] private float sensetivity = 200;
    [SerializeField] private float minMove = 0.1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;

        onMouseMove.Invoke(mouseX,mouseY);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
            OnAttack?.Invoke();

        }

        xInput = Input.GetAxis("Horizontal");        
        yInput = Input.GetAxis("Vertical");

        if(xInput != 0 || yInput != 0 )
        {
            isMoving = true; 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onJump.Invoke();
            isMoving = true;
            
            //animator.SetBool("Grounded",false);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            OnTorchPressedEvent?.Invoke();
            //onTorch?.Invoke();
        }


        if (isMoving)
        {
            moveDirection.x = MathF.Abs(xInput) > minMove? xInput : 0;
            moveDirection.z = MathF.Abs(yInput) > minMove ? yInput : 0;
            moveDirection.y = 0;

            //Debug.Log($"Player make a move!! {this}");
            onMove.Invoke(moveDirection); 
        }
        isMoving = false;

    }
}
