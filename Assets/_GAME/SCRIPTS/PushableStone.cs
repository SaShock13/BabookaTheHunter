using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableStone : MonoBehaviour
{
    [SerializeField] private bool isPlayerInTrigger = false;
    [SerializeField] private bool isPushable = true;
    [SerializeField] private Rigidbody stoneRigidBody;
    private Transform playerTransform;
    [SerializeField] private float pushForce = 2;

    // todo сделать систему толкания, ятобы только по нажатию клавиши можно толкать!
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isPushable)
        {
            stoneRigidBody.angularDrag = 1000;
            Debug.Log($"intrigger of stone {this}");
            playerTransform = other.transform;
            Debug.Log($"In Trigger {other.name}");
            isPlayerInTrigger=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            stoneRigidBody.angularDrag = 1;
            Debug.Log($"OUT Trigger {other.name}");
            isPlayerInTrigger = false;
            playerTransform = null;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine( ActivateStone());
            }
        }
    }

    private IEnumerator ActivateStone()
    {
        Debug.Log($"Activate Stone {this}");
        stoneRigidBody.angularDrag = 1;
        var pushDirection = transform.position - new Vector3( playerTransform.position.x, transform.position.y, playerTransform.position.z);
        
        stoneRigidBody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        isPushable = false;
        yield return new WaitForSeconds(1f);
        isPushable = true;
    }
}
