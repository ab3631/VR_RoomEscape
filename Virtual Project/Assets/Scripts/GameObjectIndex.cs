using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameObjectIndex : MonoBehaviour
{
    public TextMeshPro textMeshPro;
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

}
