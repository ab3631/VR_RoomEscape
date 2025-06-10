using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ClickMagicStone : MonoBehaviour
{

    [Header("XR Interaction")]
    [SerializeField] XRBaseInteractable interactable;

    [Header("UI Interaction")]
    [SerializeField] string text;

    bool isHover;

    // Start is called before the first frame update
    void Start()
    {
        isHover = false;
        
        interactable = GetComponent<XRBaseInteractable>();
        interactable.firstSelectEntered.AddListener(ButtonClick);

        if (interactable == null) Debug.Log("Can't Find Interactable");

        GetComponent<XRBaseInteractable>().firstHoverEntered.AddListener(OnHover);
        GetComponent<XRBaseInteractable>().lastHoverExited.AddListener(OffHover);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClick(InputAction.CallbackContext context)
    {

        if (!context.started)
        {
            Debug.Log("Error(ClickMagicStone<<ButtonClick)");
            return;
        }

        if (isHover)
        {

            Debug.Log("Rooting Item");

            UIManager.Instance.ClickObject(text);
            
            this.gameObject.SetActive(false);
            
        }

    }

    void ButtonClick(SelectEnterEventArgs args)
    {
        if (isHover)
        {

            Debug.Log("Rooting Item");

            UIManager.Instance.ClickObject(text);

            this.gameObject.SetActive(false);

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

}
