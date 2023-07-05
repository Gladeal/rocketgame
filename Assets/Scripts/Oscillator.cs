using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;   

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period == Mathf.Epsilon) { return; }
        float loop = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(loop * tau); 

        movementFactor = (rawSinWave + 1f) / 2f; 
            
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
