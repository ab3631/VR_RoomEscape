using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrayPuzzle : MonoBehaviour
{

    [SerializeField] int count;
    [SerializeField] bool isClear;

    public void upCount() { if (!isClear && ++count == 4) clearPuzzle(); }
    public void downCount() { count--; }

    private static ArrayPuzzle instance = null;

    public static ArrayPuzzle Instance
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

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnityEvent doorOpen;

    void clearPuzzle()
    {
        doorOpen?.Invoke();
        isClear = true;
    }

}
