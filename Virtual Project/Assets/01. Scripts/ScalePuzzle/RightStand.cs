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
        _rigidbody.MovePosition(_initialPosition + new Vector3(0, -2, 0)); // Example movement down
    }
    public void MoveUp()
    {
        _rigidbody.MovePosition(_initialPosition);
    }
    public void MoveMiddle()
    {
        _rigidbody.MovePosition(_initialPosition + new Vector3(0, -1, 0)); // Example movement to middle
    }
}
