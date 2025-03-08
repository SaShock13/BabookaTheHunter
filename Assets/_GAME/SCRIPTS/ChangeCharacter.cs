using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    [SerializeField] private InputPlayer input1;
    [SerializeField] private InputPlayer input2;
    [SerializeField] private CharacterController characterController1;
    [SerializeField] private CharacterController characterController2;
    [SerializeField] private CAmeraFollow cameraFollow;

    private void Start()
    {
        input2.enabled = false;
        //camera2.enabled = false;
        input1.enabled = true;
        cameraFollow.targetTransform = input1.transform;
        characterController1.enabled = true;
        characterController2.enabled = false;
        //camera1.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Change();
        }
    }

    private void Change()
    {

        Debug.Log($"Changing the Character {this}");
        input2.enabled = true;
        //camera2.enabled = true;
        input1.enabled = false;
        characterController1.enabled = false;
        characterController2.enabled = true;
        cameraFollow.targetTransform = input2.transform;
        //camera1.enabled = false;
    }
}
