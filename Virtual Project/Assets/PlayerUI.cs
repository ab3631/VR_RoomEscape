using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemTransform;

    [SerializeField] private GameObject door;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(string text)
    {

        GameObject item = Instantiate(itemPrefab, itemTransform);

        item.GetComponentInChildren<TextMeshProUGUI>().text = text;

        if (++count >= 3)
        {

            door.GetComponent<GenerateOutline>().setIsActive(true);
            door.GetComponent<DoorOpen>().leftDoor.GetComponent<GenerateOutline>().setIsActive(true);

        }

    }


}
