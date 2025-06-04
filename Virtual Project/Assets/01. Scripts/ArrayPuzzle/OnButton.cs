using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnButton : MonoBehaviour
{

    [SerializeField] private XRGrabInteractable grabObject;
    [SerializeField] private bool isOn;
    [SerializeField] private bool isButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        grabObject = other.GetComponent<XRGrabInteractable>();

        if (grabObject != null && !grabObject.isSelected)
        {

            if (isOn) return;
            isOn = true;

            Debug.Log("[Debug] Object on Button : " + grabObject.name);

            Rigidbody rb = grabObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            grabObject.transform.SetParent(transform);
            grabObject.transform.localPosition = new Vector3(0f, 0.9f, 0f);
            grabObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

            grabObject.selectExited.AddListener(OnGrabbed);

            if (isButton) ArrayPuzzle.Instance.upCount();

        }

    }

    private void OnGrabbed(SelectExitEventArgs args)
    {

        Debug.Log("Test Message");

        grabObject.transform.SetParent(null);
        grabObject.GetComponent<Rigidbody>().isKinematic = false;
        grabObject.selectExited.RemoveListener(OnGrabbed);
        isOn = false;
        if (isButton) ArrayPuzzle.Instance.downCount();
        grabObject = null;

    }

}
