using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GenerateOutline : MonoBehaviour
{

    [SerializeField] private Material[] mat = new Material[3];
    [SerializeField] private XRBaseInteractable interactable;

    [SerializeField] private bool isActive;


    public bool getIsActive() { return isActive; }
    public void setIsActive(bool active) { isActive = active; }

    void Start()
    {

        interactable = GetComponent<XRBaseInteractable>();

        if (interactable == null) Debug.Log("Can't Find Interactable");

        GetComponent<XRBaseInteractable>().firstHoverEntered.AddListener(HoverEnter);
        GetComponent<XRBaseInteractable>().lastHoverExited.AddListener(HoverExit);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverEnter(HoverEnterEventArgs args)
    {

        if (isActive) gameObject.GetComponent<MeshRenderer>().material = mat[1];
        else if (!isActive) gameObject.GetComponent<MeshRenderer>().material = mat[2];

    }

    public void HoverExit(HoverExitEventArgs args)
    {

        gameObject.GetComponent<MeshRenderer>().material = mat[0];

    }

}
