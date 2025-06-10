using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class OnPillar : MonoBehaviour
{

    [SerializeField] private XRGrabInteractable grabObject;
    [SerializeField] private int floor;
    [SerializeField] private int line;
    [SerializeField] private bool isOn;

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
        
        if(grabObject != null && !grabObject.isSelected)
        {

            if (isOn) return;
            isOn = true;

            Debug.Log("[Debug] Object on Pillar : " + grabObject.name);

            Rigidbody rb = grabObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            grabObject.transform.SetParent(transform);
            grabObject.transform.localPosition = new Vector3(0f, 1.5f, 0f);
            grabObject.transform.rotation = Quaternion.Euler(90f, 90f, 0f);

            grabObject.selectExited.AddListener(OnGrabbed);

            line = int.Parse(other.GetComponentInChildren<TextMeshPro>().text);
            PyramidPuzzle.Instance.setPyramid(floor, line);
        }

    }

    private void OnGrabbed(SelectExitEventArgs args)
    {

        Debug.Log("Test Message");

        grabObject.transform.SetParent(null);
        grabObject.GetComponent<Rigidbody>().isKinematic = false;
        grabObject.selectExited.RemoveListener(OnGrabbed);
        PyramidPuzzle.Instance.destoryPyramid(floor, line); line = 0;
        isOn = false;
        grabObject = null;

    }

}
