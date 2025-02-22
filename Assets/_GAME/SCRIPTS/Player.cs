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

    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float rotateSpeed = 2;
    [SerializeField] private float jumpHieght = 2;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpForceDelay = 0.1f; // для синхронизации движения с анимацией 
    [SerializeField] private float targetTurnSpeed = 10;
    [SerializeField] private GameObject torch;
    [SerializeField] private Transform cameraTransform;

    public  IInteractable interactableObject = null ;

    private void OnEnable()
    {
        InputPlayer.OnTorchPressedEvent += GetTorch;
        InputPlayer.OnAttackEvent += Attack;
        InputPlayer.OnJumpEvent += Jump;
        InputPlayer.OnMoveEvent += MovePlayer; 
        InputPlayer.OnInteractEvent += Interact; 
        
    }

    private void OnDisable()
    {
        InputPlayer.OnTorchPressedEvent -= GetTorch;
        InputPlayer.OnAttackEvent -= Attack;
        InputPlayer.OnJumpEvent -= Jump;
        InputPlayer.OnMoveEvent -= MovePlayer;
        InputPlayer.OnInteractEvent -= Interact;
    }

    public void GetTorch()
    {

        Debug.Log($"Torch in player {this}");
        if (torch.activeInHierarchy)
        { torch.SetActive(false); }
        else torch.SetActive(true);
    }

    //todo прыжок очень высокий Иногда??!!
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
        animator = GetComponent<Animator>();
        playerTransform = transform;      
    }

    public void MovePlayer(Vector3 flatDirection)
    {
        moveDirection = cameraTransform.TransformDirection(flatDirection);        
        characterController.Move(moveDirection * runSpeed * Time.deltaTime);
        animator.SetFloat("Speed", (moveDirection.magnitude * 5)>0.01f? moveDirection.magnitude *5 : 0);
    }

    private void Interact()
    {
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        sounds.PlaySwish();
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            sounds.PlayJumpInitSound();
            animator.SetTrigger("Jump 0");
            verticalVelocity = MathF.Sqrt(jumpHieght * gravity * -2);
        }
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
        lookVector = cameraTransform.forward;
        lookVector.y = 0;
        playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, Quaternion.LookRotation(lookVector), Time.deltaTime * targetTurnSpeed); // поворачивает игрока в соторону взгдяда камеры
    }

    private void UseGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    public void Death()
    {
        sounds.PlayDeath();
        animator.SetTrigger("Death");
    }
}
