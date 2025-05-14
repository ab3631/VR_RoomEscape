using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(string text)
    {

        GameObject item = Instantiate(itemPrefab, itemTransform);

        item.GetComponentInChildren<TextMeshProUGUI>().text = text;

    }


}
