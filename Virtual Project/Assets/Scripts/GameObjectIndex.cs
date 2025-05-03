using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameObjectIndex : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public Material sortedMaterial;
    
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


    public void SetBlue()
    {
        GetComponent<Renderer>().materials[0] = sortedMaterial;
    }
}
