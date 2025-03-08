using System;
using System.Collections;
using System.Collections.Generic;
using Assets._GAME.SCRIPTS;
using UnityEngine;
using Zenject;

public class PushableStone : InteractableObject
{
    private Transform playerTransform;
    private LayerMask playerLayer;
    private LayerMask initLayerMask;

    [SerializeField] private bool isPlayerInTrigger = false;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float pushForce = 2;

    private void Start()
    {
        initLayerMask = rigidBody.excludeLayers;
        playerLayer = LayerMask.GetMask("Player");
    }


    protected override void AfterOnTriggerEnter()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        playerTransform = _interactor.transform;
        _interactor.interactableObject = this;
    }

    protected override void AfterOnTriggerExit()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
        playerTransform = null;
        _interactor.interactableObject = null;
    }

    override public void Interact()
    {
        StartCoroutine(ActivateStone());
    }

    private IEnumerator ActivateStone()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
        Debug.Log($"Activate Stone {this}");
        rigidBody.isKinematic = false;
        var pushDirection = transform.position - playerTransform.position;        
        rigidBody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        rigidBody.excludeLayers = playerLayer;
        yield return null;
        yield return null;
        rigidBody.excludeLayers = initLayerMask;
        yield return new WaitForSeconds(1f);
    }
}
