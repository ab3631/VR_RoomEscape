using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemTransform;

    [SerializeField] private GameObject door;
    int count;

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

    public UnityEvent doorOpen;

    public void addItem(string text)
    {
        
        GameObject item = Instantiate(itemPrefab, itemTransform);
        inventoryItems.Add(item);

        item.GetComponentInChildren<TextMeshProUGUI>().text = text;

        if (++count >= 3)
        {

            door.GetComponent<GenerateOutline>().setIsActive(true);
            door.GetComponent<DoorOpen>().rightDoor.GetComponent<GenerateOutline>().setIsActive(true);

            doorOpen?.Invoke();

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



}
