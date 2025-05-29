using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftStandCollider : MonoBehaviour
{
    [SerializeField] private ScalePuzzle scalePuzzle; // Reference to the ScalePuzzle script
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Weight"))
        {
            // Handle collision with weight objects
            Weight weight;
            if (!trigger.gameObject.TryGetComponent<Weight>(out weight))
            {
                Debug.Log("Weight collided with left stand: " + weight.WeightValue);
            }
            scalePuzzle.LeftscaleWeights.Add(weight); // Add the weight to the left scale weights list
        }
    }
    private void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Weight"))
        {
            // Handle collision exit with weight objects
            Weight weight;
            if (!trigger.gameObject.TryGetComponent<Weight>(out weight))
            {
                Debug.Log("Weight exited collision with left stand: " + weight.WeightValue);
            }
            scalePuzzle.LeftscaleWeights.Remove(weight); // Remove the weight from the left scale weights list
        }
    }
}

