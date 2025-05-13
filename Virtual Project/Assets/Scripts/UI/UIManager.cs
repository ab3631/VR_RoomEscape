using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] InputActionReference inputReference;

    bool check;

    // Start is called before the first frame update
    void Start()
    {

        check = false;

        UIOnOff();
        inputReference.action.started += OnOffButtonClick;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnOffButtonClick(InputAction.CallbackContext context)
    {

        if (!context.started)
        {
            Debug.Log("MM");
            return;
        }

        check = !check;
        Debug.Log("TestMessage");


        UIOnOff();

    }

    void UIOnOff()
    {

        if (check)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

    }

}
