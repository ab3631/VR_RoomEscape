using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextSetter : MonoBehaviour
{
    [Multiline]
    public string text;

    public void SetUIText()
    {
        // UIManager.Instance.playerUI.SetDescription(text);
    }
}
