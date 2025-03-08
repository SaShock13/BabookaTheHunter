using System.Collections;
using System.Collections.Generic;
using Assets._GAME.SCRIPTS;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableObject : MonoBehaviour,IInteractable
{
    protected PlayerInteractor _interactor;

    public virtual void Interact()
    {
        Debug.Log($"Interact {this}");
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerInteractor>(out _interactor))
        {
            _interactor.interactableObject = this;
            Debug.Log($"interactableObject {_interactor.interactableObject}");
            AfterOnTriggerEnter();
        }
    }

    protected virtual void AfterOnTriggerEnter()
    {

    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerInteractor>(out var leaveInteractor))
        {
            _interactor.interactableObject = null;
            Debug.Log($"interactableObject {_interactor.interactableObject}");
            AfterOnTriggerExit();
        }
    }
    protected virtual void AfterOnTriggerExit()
    {

    }
}
