using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SortedObject : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public Material sortedMaterial;
    public bool isFixed;
    public Action<SortedObject> OnClicked;
    private int _index;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
            //UpdateText();
        }
    }

    private void Start()
    {
        isFixed = false;
        var interact  = GetComponent<XRSimpleInteractable>();
        interact.selectEntered.AddListener((args) => OnObjectClick());
        //textMeshPro = GetComponentInChildren<TextMeshPro>();
    }
    public void UpdateText()
    {
        textMeshPro.text = _index.ToString();
    }
    public void OnObjectClick()
    {
        OnClicked?.Invoke(this);
    }

    public void SetFixed()
    {
        transform.DORotate(transform.eulerAngles + new Vector3(0,180,0), 0.5f);
        GetComponent<XRSimpleInteractable>().enabled = false;
        isFixed = true;
    }
}
