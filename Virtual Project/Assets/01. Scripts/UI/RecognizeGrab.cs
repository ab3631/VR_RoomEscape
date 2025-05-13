using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecognizeGrab : MonoBehaviour
{

    [SerializeField] private Material[] mat = new Material[3];

    bool isActive;

    // Start is called before the first frame update
    void Start()
    {

        isActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverEnter()
    {

        if (isActive) gameObject.GetComponent<MeshRenderer>().material = mat[1];
        else if (!isActive) gameObject.GetComponent<MeshRenderer>().material = mat[2];

    }

    public void HoverExit()
    {

        gameObject.GetComponent<MeshRenderer>().material = mat[0];

    }

}
