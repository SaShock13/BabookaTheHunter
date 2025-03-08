using System.Collections;
using System.Collections.Generic;
using Assets._GAME.SCRIPTS;
using UnityEngine;
using Zenject;

public class CampFire : InteractableObject
{
    public override void Interact()
    {
        if(_interactor != null)
        {
            _interactor.LightTorch();
        }
    }
}
