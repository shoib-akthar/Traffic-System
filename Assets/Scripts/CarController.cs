using UnityEngine;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public Transform randomPositions; // List of random positions to reset the car
    private Vector3 originalPosition; // Store the original position of the car

    void Start()
    {
        originalPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car")) 
        {
            if (other.gameObject.CompareTag("Car")) // Check if the collided object is a car
            {
                ResetCarPosition(other.gameObject);
            }
        }
    }

    void ResetCarPosition(GameObject car)
    {
        if (randomPositions!=null)
        {
            car.transform.position = randomPositions.position;
            car.GetComponent<Car_AI>().currentTrafficRoute = randomPositions.parent.gameObject;
            car.GetComponent<Car_AI>().currentWapointNumber = 5;
        }
        else
        {
            Debug.LogWarning("No random positions provided to reset the car.");
        }
    }

    public void ResetToOriginalPosition()
    {
        transform.position = originalPosition;
    }
}
