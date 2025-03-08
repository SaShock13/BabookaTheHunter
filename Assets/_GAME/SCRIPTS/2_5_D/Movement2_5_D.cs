using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement2_5_D : MonoBehaviour, IMovable
{
    private CharacterController controller;
    private Animator animator;
    [SerializeField] private float runSpeed = 5f;
    private bool lookLeft = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction)
    {
        var x = direction.x *-1;
        //Vector3 newScale = Vector3.one;
        animator.SetFloat("Speed", Mathf.Abs (direction.x * 5) > 0.01f ? Mathf.Abs(direction.x * 5) : 0);
        //newScale.z = -1;
        if (x < 0 && lookLeft)
        {
            //controller.gameObject.transform.localScale = newScale;
            controller.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.back);
            lookLeft = false;
        }
        else if (x>0 && !lookLeft)
        {
            //controller.gameObject.transform.localScale = Vector3.one;
            controller.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward);
            lookLeft =true;
        }
        var newVector = new Vector3(0, 0, x);
        controller.Move(newVector * runSpeed * Time.deltaTime);
        Debug.Log($"2D movement {direction}");
    }
}
