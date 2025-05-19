using System.Collections;
using System.Collections.Generic;
using System.Threading;

using TMPro;

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleViewer : MonoBehaviour
{
    public TextMeshPro signboard;
    public GameObject pieces;
    List<SortedObject> list;
    public XRSimpleInteractable clickEvent;

    // Start is called before the first frame update
    List<int> nums;
    public void Init(int[,] numbers)
    {
        nums = new();
        list = new List<SortedObject>();
        for (int i = 0; i < pieces.transform.childCount; i++)
        {
            var piece = pieces.transform.GetChild(i).GetComponent<SortedObject>();
            if (piece != null)
            {
                list.Add(piece);
                
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            list[i].Index = numbers[i/3,i%3];
            if (numbers[i / 3, i%3] == 9) list[i].gameObject.SetActive(false);
            nums.Add(numbers[i/3,i%3]);
        }

        nums.Sort();
        int count = CheckCorrect();
        
        signboard.text = "Correct : " + count;
    }

    public int CheckCorrect()
    {
        int count = 0;
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log($"{list[i].Index} + {nums[i]}");
            if (list[i].Index == nums[i])
            {
                count++;
            }
        }
        return count;
    }

}
