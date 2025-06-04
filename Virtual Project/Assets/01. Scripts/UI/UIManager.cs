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

    [SerializeField] private Material[] mat = new Material[2];
                     public Material getMaterial(int i) { return mat[i]; }


    [SerializeField] public PlayerUI playerUI;

    bool check;

    // UIManager ΩÃ±€≈Ê º≥¡§
    private static UIManager instance = null;

    public static UIManager Instance
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

    void Start()
    {

        check = false;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        inputReference.action.started += OnOffButtonClick;
        playerUI = GameObject.Find("Canvas(PlayerUI)").GetComponent<PlayerUI>();

        if(playerUI != null) Debug.Log("Find PlayerUI");

    }

    void Update() { }

    void OnOffButtonClick(InputAction.CallbackContext context)
    {

        if (!context.started)
        {
            Debug.Log("MM");
            return;
        }

        check = !check;

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

    public void ClickObject(string text)
    {

        playerUI.addItem(text);

    }

}
