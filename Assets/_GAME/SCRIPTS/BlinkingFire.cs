using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingFire : MonoBehaviour
{
    private float initIntencity;
    private float minIntencity;
    private float maxIntencity;
    private bool isIncreasing = false;

    [SerializeField] private Light campfire;
    [SerializeField] private float speed = 10;
    [SerializeField] private float increasingTime;
    [SerializeField] private float decreasingTime;



    void Start()
    {   
        initIntencity = campfire.intensity;
        maxIntencity = initIntencity;
        minIntencity = maxIntencity / 2;
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

        if (isIncreasing && campfire.intensity > maxIntencity) 
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

    public void SetIntensityCoef(float coef)
    {
        maxIntencity = initIntencity * coef;
        minIntencity = maxIntencity /2f;
    }
}
