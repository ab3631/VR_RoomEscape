using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightStandCollider : MonoBehaviour
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
            //trigger.gameObject.transform.SetParent(this.transform); // Set the parent of the weight to this stand
            // Handle collision with weight objects
            Weight weight;
            if (!trigger.gameObject.TryGetComponent<Weight>(out weight))
            {
                Debug.Log("Weight collided with right stand: " + weight.WeightValue);
            }
            scalePuzzle.RightscaleWeights.Add(weight); // Add the weight to the right scale weights list
        }
    }
    private void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Weight"))
        {
            trigger.gameObject.transform.SetParent(transform.parent.parent); // Remove the parent of the weight when it exits the collider
            // Handle collision exit with weight objects
            Weight weight;
            if (!trigger.gameObject.TryGetComponent<Weight>(out weight))
            {
                Debug.Log("Weight exited collision with right stand: " + weight.WeightValue);
            }
            scalePuzzle.RightscaleWeights.Remove(weight); // Remove the weight from the right scale weights list
        }
    }
}
