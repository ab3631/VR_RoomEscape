using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;
using Unity.VisualScripting;

using UnityEngine;

public class SortAlgorithm : MonoBehaviour
{
    public List<GameObjectIndex> boxList;

    private void Start()
    {
        SetBlock();
        queue.Enqueue(new int2(0,boxList.Count-1));
    }
    [ContextMenu("SetBlockList")]
    public void SetBlock()
    {
        boxList = new List<GameObjectIndex>();
        for(int i = 0; i < transform.childCount; i++)
        {
            var childObj = transform.GetChild(i).gameObject;
            var index = childObj.AddComponent<GameObjectIndex>();
            boxList.Add(index);
            index.index = i + 1;
        }
        // Shuffle
        for(int i = 0; i < boxList.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, boxList.Count);
            var t = boxList[i];
            boxList[i] = boxList[rand];
            boxList[rand] = t;
        }
    }

    Queue<int2> queue = new Queue<int2>();
    public void Sort()
    {

        int2 i = queue.Dequeue();
        QuickSort(i.x, i.y);
        Debug.Log(queue.Count);

    }
    
    public void QuickSort(int start, int end)
    {
        if (start >= end) return;
        int s = start;
        int e = end;
        var standard = boxList[e];
        e--;
        while (s <= e)
        {
            bool a = IsBigger(boxList[s].index,standard.index);
            bool b = IsBigger(standard.index, boxList[e].index);
            if (a && b)
            {
                GameObjectIndex t = boxList[s];
                boxList[s] = boxList[e];
                boxList[e] = t;
            }
            else
            {
                if (!a)
                {
                    s++;
                }
                if (!b)
                {
                    e--;
                }
            }
        }
        GameObjectIndex temp = boxList[s];
        boxList[s] = boxList[end];
        boxList[end] = temp;
        queue.Enqueue(new int2(start, e));
        queue.Enqueue(new int2(s + 1, end));
    }

    public bool IsBigger(int a, int b)
    {
        return a > b;
    }

    
}
