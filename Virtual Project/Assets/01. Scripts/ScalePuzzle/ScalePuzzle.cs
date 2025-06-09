using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScalePuzzle : MonoBehaviour
{
    public List<Weight> LeftscaleWeights; // Array to hold the objects that will be scaled
    public List<Weight> RightscaleWeights; // Array to hold the objects that will be scaled
    [SerializeField] private LeftStand _leftStand; // Reference to the left stand
    [SerializeField] private RightStand _rightStand; // Reference to the right stand
    [SerializeField] private float _leftWeightSum; // Variable to hold the sum of weights on the left scale
    [SerializeField] private float _rightWeightSum; // Variable to hold the sum of weights on the right scale

    [SerializeField] int _puzzleTime = 0;
    [SerializeField] private DoorOpen _doorOpen; // Reference to the door open script
    public UnityEvent OnpuzzleSolved;

    private int _result;
    const int _left = 0;
    const int _right = 1;
    const int _same = 2;
    // Start is called before the first frame update
    void Start()
    {
        _result = _same; // Initialize the result to 'same' state
        _leftStand.MoveMiddle();
        _rightStand.MoveMiddle();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPuzzleTime();
    }
    private void FixedUpdate()
    {
        _leftWeightSum = 0f; // Reset the left weight sum
        _rightWeightSum = 0f; // Reset the right weight sum
        // Calculate the total weight on the left scale
        foreach (Weight weight in LeftscaleWeights)
        {
            _leftWeightSum += weight.WeightValue;
        }
        // Calculate the total weight on the right scale
        foreach (Weight weight in RightscaleWeights)
        {
            _rightWeightSum += weight.WeightValue;
        }
        if (_leftWeightSum > _rightWeightSum)
        {
            //if(_result != _left)
            {
                _result = _left; // Left scale is heavier
                _leftStand.MoveDown();
                _rightStand.MoveUp();
            }
        }
        else if (_leftWeightSum < _rightWeightSum)
        {
            //if (_result != _right)
            {
                _result = _right; // Right scale is heavier
                _leftStand.MoveUp();
                _rightStand.MoveDown();
            }
        }
        else
        {
            //if (_result != _same)
            {
                _result = _same; // Both scales are equal
                _leftStand.MoveMiddle();
                _rightStand.MoveMiddle();
            }
        }
    }
    private void CheckPuzzleTime()
    {
        if(_puzzleTime == 0)
        {
            if (_leftWeightSum < _rightWeightSum)
            {
                
                _puzzleTime = 1; // Move to the next puzzle time
            }
        }
        if(_puzzleTime == 1)
        {
            if (_leftWeightSum > _rightWeightSum)
            {
                
                _puzzleTime = 2; // Move to the next puzzle time
            }
        }
        if (_puzzleTime == 2)
        {
            if ((_leftWeightSum == _rightWeightSum) && _leftWeightSum != 0)
            {
                //OnpuzzleSolved?.Invoke();
                _doorOpen.Open(); // Open the door if the left scale is heavier
            }
        }
    }
}
