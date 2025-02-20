using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBehaviour : MonoBehaviour
{
    private Animator animator;
    private Vector3 currentDestination;
    private int currentDestinationIndex = 0;
    [SerializeField] private Transform[] walkPoints;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float minDistance = 0.5f; // минимальное расстояние до точки

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("WalkForward", true);
        currentDestination = walkPoints[currentDestinationIndex].position;
    }

    private void Update()
    {
        currentDestination.y = transform.position.y;
        var distance = Vector3.Distance(currentDestination, transform.position);
        if ( distance < minDistance )
        {
            currentDestinationIndex++;
            if (currentDestinationIndex >= walkPoints.Length)
            {
                currentDestinationIndex = 0;
            }
            currentDestination = walkPoints[currentDestinationIndex].position;
        }       
        var direction = (currentDestination - transform.position).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),Time.deltaTime * rotationSpeed);
        transform.position += direction * walkSpeed * Time.deltaTime;
    }


}
