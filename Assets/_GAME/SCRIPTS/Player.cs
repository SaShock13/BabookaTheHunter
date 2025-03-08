using System;
using System.Collections;
using System.Collections.Generic;
using Assets._GAME.SCRIPTS;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputPlayer input; 
    private CharacterController characterController;
    private Animator animator;
    private Transform playerTransform;
    private Vector3 moveDirection;
    private Vector3 lookVector;
    private float verticalVelocity;
    private PlayerSounds sounds;
    private Hanging hanging;

    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float rotateSpeed = 2;
    [SerializeField] private float jumpHieght = 2;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpForceDelay = 0.1f; // для синхронизации движения с анимацией 
    [SerializeField] private float targetTurnSpeed = 10;
    [SerializeField] private GameObject torchObj;
    [SerializeField] private Transform cameraTransform;

    public  IInteractable interactableObject = null ;
    public bool isGravityActive = true;
    private bool isAdjusting = false ;
    private float adjustYValue = 0;
    private float adjustSpeed = 13f;
    private float playerHeight;

    public void GetTorch()
    {
        Debug.Log($"Torch in player {this}");
        if (torchObj.activeInHierarchy)
        { 
            torchObj.GetComponent<Torch>().SetUnLighted();
            torchObj.SetActive(false); 

        }
        else torchObj.SetActive(true);
    }

    public void LightTorch()
    {
        if (torchObj.activeInHierarchy)
        {
            torchObj.GetComponent<Torch>().SetLighted(); 
        }
    }

    //todo Поджигание факела от костра сделать?
    private void Awake()
    {
        input = GetComponent<InputPlayer>();
        sounds = GetComponent<PlayerSounds>();
    }

    private void Start()
    {
        //Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        hanging = GetComponent<Hanging>();
        animator = GetComponent<Animator>();
        playerTransform = transform;      
    }

    public void MovePlayer(Vector3 flatDirection)
    {
        moveDirection = cameraTransform.TransformDirection(flatDirection);        
        characterController.Move(moveDirection * runSpeed * Time.deltaTime);
        animator.SetFloat("Speed", (moveDirection.magnitude * 5)>0.01f? moveDirection.magnitude *5 : 0);
    }

    //public void Interact()
    //{
    //    if (interactableObject != null)
    //    {
    //        interactableObject.Interact();
    //    }
    //}

    public void Attack()
    {
        animator.SetTrigger("Attack");
        sounds.PlaySwish();
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            StartCoroutine(TryHang());
            sounds.PlayJumpInitSound();
            animator.SetTrigger("Jump 0");
            verticalVelocity = MathF.Sqrt(jumpHieght * gravity * -2);
        }
    }

    private IEnumerator TryHang()
    {
        hanging.StartTryHang();
        yield return new WaitForSeconds(1.5f);
        StopTryHang();
    }    

    public void StopTryHang()
    {
        hanging.StopTryHang();
    }

    private void Update()
    {
        UseGravity();
        if (characterController.isGrounded)
        {
            animator.SetBool("Grounded", true);
            verticalVelocity = -2f;
        }
        else
        {
            animator.SetBool("Grounded", false);
        }


        //For Degub
        if(Input.GetKey(KeyCode.T))
        {
            LightTorch();
        }

    }

    public void StartYAdjust(float adjustingYValue, Vector3 normal)
    {        
        StartCoroutine(AdjustionYPositionCoroutine(adjustingYValue));
        StartCoroutine(AdjustionRotationCoroutine(normal));

    }


    // todo подкорректировать положение , отдалить немного от уступа
    private IEnumerator AdjustionYPositionCoroutine(float adjustingYValue)
    {
        var targetY = transform.position.y + adjustingYValue;

        Debug.Log($"targetY {targetY}");
        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            var newPosition = transform.position;
            newPosition.y = Mathf.Lerp(newPosition.y, targetY, Time.deltaTime * adjustSpeed);
            transform.position = newPosition;
            yield return null;
        }        
    }
    private IEnumerator AdjustionRotationCoroutine(Vector3 normal)
    {        
        Quaternion targetRotation = Quaternion.LookRotation(-normal, Vector3.up);
        float angle = 1;        
        while (Mathf.Abs( angle ) > 0.05f)
        {
            angle = Quaternion.Angle(transform.rotation, targetRotation);
            Debug.Log($"rotate adjusting  {angle}");
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * adjustSpeed);
            yield return null;
        }        
    }

    public void Climb(Vector3 destination)
    {
        StartCoroutine(ClimbCoroutine(destination));
    }
    private IEnumerator ClimbCoroutine(Vector3 destination)
    {
        while (Mathf.Abs(Vector3.Distance(transform.position, destination)) > 0.1f)
        {
            var newPosition = transform.position;
            newPosition = Vector3.Lerp(newPosition, destination, Time.deltaTime * adjustSpeed);
            transform.position = newPosition;
            yield return null;
        }
        
    }

    public void LookCamera()
    {
        lookVector = cameraTransform.forward;
        lookVector.y = 0;
        playerTransform.rotation = 
            Quaternion.Lerp(playerTransform.rotation, 
            Quaternion.LookRotation(lookVector), 
            Time.deltaTime * targetTurnSpeed); // поворачивает игрока в соторону взгдяда камеры
    }

    private void UseGravity()
    {
        if (isGravityActive)
        {
            verticalVelocity += gravity * Time.deltaTime;
            characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime); 
        }
    }

    public void Death()
    {
        sounds.PlayDeath();
        animator.SetTrigger("Death");
    }

}

