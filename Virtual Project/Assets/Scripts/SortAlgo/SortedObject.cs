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

    public Action OnClicked;
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
            UpdateText();
        }
    }

    private void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }
    public void UpdateText()
    {
        textMeshPro.text = _index.ToString();
    }
    public void OnObjectClick()
    {
        OnClicked?.Invoke();
    }

    public void SetFixed()
    {
        GetComponent<Renderer>().materials = new List<Material>() { sortedMaterial}.ToArray();
        OnClicked = null;
        GetComponent<XRSimpleInteractable>().enabled = false;
    }
}
