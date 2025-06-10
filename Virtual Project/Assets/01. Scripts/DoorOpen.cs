using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorOpen : MonoBehaviour
{

    [Header("XR Interaction")]
    [SerializeField] XRBaseInteractable interactable;

    bool isHover;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public float leftDoorAngle;
    public float rightDoorAngle;

    public UnityEvent OnDoorOpen;

    private void Start()
    {
        //Debug.Log(rightDoor + " : " + leftDoor);
        if(rightDoor != null) rightDoor.GetComponent<GenerateOutline>().setIsActive(false);
        if(leftDoor != null) leftDoor.GetComponent<GenerateOutline>().setIsActive(false);

        isHover = false;
        //inputReference.action.started += ButtonClick;
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.firstSelectEntered.AddListener(ButtonClick);
        }


        if (interactable == null) Debug.Log("Can't Find Interactable");

        GetComponent<XRBaseInteractable>()?.firstHoverEntered.AddListener(OnHover);
        GetComponent<XRBaseInteractable>()?.lastHoverExited.AddListener(OffHover);

    }

    void ButtonClick(InputAction.CallbackContext context)
    {

        if (!context.started)
        {
            Debug.Log("Error(ClickMagicStone<<ButtonClick)");
            return;
        }

        if (isHover && GetComponent<GenerateOutline>().getIsActive())
        {

            Debug.Log("Test");
            Open();

        }

        Debug.Log("Tetasdfasdfasdf");

    }
    void ButtonClick(SelectEnterEventArgs args)
    {

        if (isHover && GetComponent<GenerateOutline>().getIsActive())
        {

            //Debug.Log("Test");
            Open();

        }

        //Debug.Log("Tetasdfasdfasdf");

    }
    public void OnHover(HoverEnterEventArgs args)
    {

        isHover = true;

    }

    public void OffHover(HoverExitEventArgs args)
    {

        isHover = false;

    }

    public void Open()
    {
        if (leftDoor != null)
        {

            Debug.Log("Log : leftDoor");
            leftDoor.transform.DOLocalRotate(new Vector3(0, leftDoorAngle, 0), 1f);

        }    
        if (rightDoor != null)
        {

            Debug.Log("Log : rightDoor");
            rightDoor.transform.DOLocalRotate(new Vector3(0, rightDoorAngle, 0), 1f);

        }
        OnDoorOpen?.Invoke();
    }
}
