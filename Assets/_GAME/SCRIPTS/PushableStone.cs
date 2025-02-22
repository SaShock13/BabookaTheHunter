using System;
using System.Collections;
using System.Collections.Generic;
using Assets._GAME.SCRIPTS;
using UnityEngine;
using Zenject;

public class PushableStone : MonoBehaviour , IInteractable
{
    private Transform playerTransform;
    private Player _player;
    private LayerMask playerLayer;
    private LayerMask initLayerMask;


    [SerializeField] private bool isPlayerInTrigger = false;
    [SerializeField] private bool isPushable = true;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float pushForce = 2;




    [Inject]

    public void Construct(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        initLayerMask = rigidBody.excludeLayers;
        playerLayer = LayerMask.GetMask("Player");
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInTrigger)
        {
            Debug.Log($"intrigger of stone {this}");
            playerTransform = other.transform;
            _player.interactableObject = this;
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&& isPlayerInTrigger)
        {
            Debug.Log($"OUT Trigger {other.name}");
            isPlayerInTrigger = false;
            playerTransform = null;
            _player.interactableObject = null;
        }
    }

    public void Interact()
    {
        //StartCoroutine(ActivateStone());
    }

    private IEnumerator ActivateStone()
    {
        Debug.Log($"Activate Stone {this}");
        rigidBody.isKinematic = false;
        var pushDirection = transform.position - playerTransform.position;        
        rigidBody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        isPushable = false;
        rigidBody.excludeLayers = playerLayer;
        yield return null;
        yield return null;
        rigidBody.excludeLayers = initLayerMask;
        yield return new WaitForSeconds(1f);
        isPushable = true;
    }
}
