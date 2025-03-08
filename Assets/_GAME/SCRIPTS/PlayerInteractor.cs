using System.Collections;
using System.Collections.Generic;
using Assets._GAME.SCRIPTS;
using UnityEngine;
using Zenject;

public class PlayerInteractor : MonoBehaviour
{
    public IInteractable interactableObject = null;
    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }


    public void Interact()
    {
        if (interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    public void LightTorch()
    {
        _player.LightTorch();
    }

}
