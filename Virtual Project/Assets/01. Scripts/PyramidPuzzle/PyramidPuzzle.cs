using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PyramidPuzzle : MonoBehaviour
{

    [SerializeField] GameObject[][] pyramid = new GameObject[5][];
    [SerializeField] GameObject block;
    [SerializeField] int[] score = new int[5];

    private static PyramidPuzzle instance = null;

    public static PyramidPuzzle Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake() { instance = this; }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 5; i++)
        {
         
            score[i] = 0;
            pyramid[i] = new GameObject[9];

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPyramid(int floor, int num)
    {

        score[floor - 1] = num;

        float offset = (num - 1) / 2f;

        for(int i = 0; i < num; i++)
        {

            GameObject obj = Instantiate(block, this.transform);
            obj.transform.localPosition = new Vector3(i - offset, floor - 0.5f, 3f);
            pyramid[floor - 1][i] = obj;

        }

        checkPuzzle();

    }

    public void destoryPyramid(int floor, int num)
    {

        score[floor - 1] = 0;
        
        for(int i = 0; i <  num; i++) Destroy(pyramid[floor - 1][i]);

    }

    private void checkPuzzle()
    {

        for(int i = 0; i < 5; i++)
        {
            Debug.Log("[Debug] Score Check : " + score[i] + " = " + (2 * i + 1));
            if (score[i] != (9 - i * 2)) return;

        }

        clearPuzzle();

    }

    public UnityEvent doorOpen;

    private void clearPuzzle()
    {
        doorOpen?.Invoke();
    }

}
