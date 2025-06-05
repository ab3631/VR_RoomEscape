using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RightStand : MonoBehaviour
{
    [SerializeField] private ScalePuzzle _scalePuzzle; // Reference to the ScalePuzzle script
    [SerializeField] private BoxCollider _rightStandCollider; // Reference to the BoxCollider for the right stand
    private Vector3 _initialPosition; // Store the initial position of the right stand
    private Rigidbody _rigidbody; // Reference to the Rigidbody component
    private void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out _rigidbody))
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>(); // Add Rigidbody if not present
        }
        _initialPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, _initialPosition + new Vector3(0, -0.5f, 0), 0.1f); // Move down towards the initial position - 2 units
        //_rigidbody.MovePosition(_initialPosition + new Vector3(0, -2, 0)); // Move down by 2 units
    }
    public void MoveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, _initialPosition + new Vector3(0, 0.5f, 0), 0.1f); // Move up towards the initial position + 2 units
        //_rigidbody.MovePosition(_initialPosition); // Reset to initial position
    }
    public void MoveMiddle()
    {
        transform.position = Vector3.MoveTowards(transform.position, _initialPosition, 0.1f); // Move towards the initial position
                                                                                              // _rigidbody.MovePosition(_initialPosition + new Vector3(0, -1, 0));
    }
}
