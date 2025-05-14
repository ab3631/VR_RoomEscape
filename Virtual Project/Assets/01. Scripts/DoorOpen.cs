using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorOpen : MonoBehaviour
{

    [Header("XR Interaction")]
    [SerializeField] InputActionReference inputReference;
    [SerializeField] XRBaseInteractable interactable;

    bool isHover;

    public GameObject leftDoor;
    public GameObject rightDoor;

    private void Start()
    {
        rightDoor.GetComponent<GenerateOutline>().setIsActive(false);
        leftDoor.GetComponent<GenerateOutline>().setIsActive(false);

        isHover = false;
        inputReference.action.started += ButtonClick;
        interactable = GetComponent<XRBaseInteractable>();

        if (interactable == null) Debug.Log("Can't Find Interactable");

        GetComponent<XRBaseInteractable>().firstHoverEntered.AddListener(OnHover);
        GetComponent<XRBaseInteractable>().lastHoverExited.AddListener(OffHover);

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
        leftDoor.transform.DOLocalRotate(new Vector3(0, -90, 0), 1f);
        rightDoor.transform.DOLocalRotate(new Vector3(0, 90, 0), 1f);
    }
}
