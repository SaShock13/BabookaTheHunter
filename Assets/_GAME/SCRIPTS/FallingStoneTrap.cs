using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneTrap : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    //todo согласовать с толканием или сделать падающий но не толкаемый

    private void Start()
    {
        rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rigidbody.isKinematic = false;
        }
    }
}
