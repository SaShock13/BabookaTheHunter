using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingFire : MonoBehaviour
{
    // todo мерцание огня 
    [SerializeField] private Light campfire;
    private float initIntencity;
    private float minIntencity;
    [SerializeField] private float speed = 10;
    private bool isIncreasing = false;
    [SerializeField] private float increasingTime;
    [SerializeField] private float decreasingTime;

    void Start()
    {   
        initIntencity = campfire.intensity;
        minIntencity = initIntencity / 2;
    }

    private void Update()
    {
        if (isIncreasing)
        {
            campfire.intensity += Time.deltaTime * speed;
        }
        else
        {
            campfire.intensity -= Time.deltaTime * speed;
        }

        if (isIncreasing && campfire.intensity > initIntencity) 
        { 
            isIncreasing = false;
            speed = Random.Range(100, 1000)/100;
        }
        if (!isIncreasing && campfire.intensity < minIntencity) 
        { 
            isIncreasing = true;
            speed = Random.Range(100, 1000)/100;
        }
    }
}
