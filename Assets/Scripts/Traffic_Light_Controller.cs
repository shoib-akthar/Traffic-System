using UnityEngine;

public class Traffic_Light_Controller : MonoBehaviour
{
    public float timeTakenBetweenTrafficLight = 3f; // Time between switching lights
    private float nextSwitchTime = 0f;
    private int currentTrafficLightIndex = 0;

    void Start()
    {
        ActivateCurrentTrafficLight();
    }

    void Update()
    {
        if (Time.time >= nextSwitchTime)
        {
            SwitchTrafficLight();
            nextSwitchTime += timeTakenBetweenTrafficLight;
        }
    }

    void SwitchTrafficLight()
    {
        int previousTrafficLightIndex = currentTrafficLightIndex;
        currentTrafficLightIndex = (currentTrafficLightIndex + 1) % transform.childCount;

        // Deactivate the previous traffic light and activate the current one
        transform.GetChild(previousTrafficLightIndex).gameObject.SetActive(false);
        ActivateCurrentTrafficLight();
    }

    void ActivateCurrentTrafficLight()
    {
        transform.GetChild(currentTrafficLightIndex).gameObject.SetActive(true);
    }
}
