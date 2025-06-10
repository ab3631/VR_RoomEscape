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

    [SerializeField] private TextMeshProUGUI middleTurmTextMessage;

    public List<GameObject> inventoryItems;

    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new List<GameObject>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(string text)
    {
        
        GameObject item = Instantiate(itemPrefab, itemTransform);
        inventoryItems.Add(item);

        item.GetComponentInChildren<TextMeshProUGUI>().text = text;

        if (++count >= 3)
        {

            door.GetComponent<GenerateOutline>().setIsActive(true);
            door.GetComponent<DoorOpen>().rightDoor.GetComponent<GenerateOutline>().setIsActive(true);

            middleTurmTextMessage.text = "문을 클릭하여 열어주세요.";

        }

    }

    public void ClearItem()
    {
        foreach (var item in inventoryItems)
        {
            Destroy(item);
        }
        inventoryItems.Clear();
    }


    public void SetDescription(string description)
    {
        middleTurmTextMessage.text = description;
    }

}
