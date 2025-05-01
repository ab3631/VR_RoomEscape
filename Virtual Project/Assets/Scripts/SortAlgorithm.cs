using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

public class SortAlgorithm : MonoBehaviour
{
    List<GameObjectIndex> boxList;
    public int listNum;
    public GameObject boxPrefab;

    private void Start()
    {
        SetBlock();
        queue.Enqueue(new int2(0,boxList.Count-1));
    }
    [ContextMenu("SetBlockList")]
    public void SetBlock()
    {
        boxList = new List<GameObjectIndex>();
        for(int i = 0; i < listNum; i++)
        {
            var childObj = Instantiate(boxPrefab,transform);
            var index = childObj.GetComponent<GameObjectIndex>();
            boxList.Add(index);
            index.Index = i + 1;
        }
        // Shuffle
        for(int i = 0; i < boxList.Count; i++)
        {
            int rand = UnityEngine.Random.Range(0, boxList.Count);
            var t = boxList[i];
            boxList[i] = boxList[rand];
            boxList[rand] = t;
        }
        for (int i = 0; i < boxList.Count; i++)
        {
            boxList[i].transform.localPosition = new Vector3(i * 1.2f, 0, 0);
        }
    }

    Queue<int2> queue = new Queue<int2>();
    public void Sort()
    {
        var temp = new Queue<int2>(queue);
        queue.Clear();
        while (temp.Count > 0) 
        { 
            int2 sortIndex = temp.Dequeue();
            Debug.Log(sortIndex.ToString());
            StartCoroutine(QuickSort(sortIndex.x, sortIndex.y));
        }
    }
    
    public IEnumerator QuickSort(int start, int end)
    {
        if (start >= end) yield break;
        int s = start;
        int e = end;
        var standard = boxList[end];
        e--;
        standard.transform.DOLocalMove(new Vector3((s + e) / 2 * 1.2f, 3, 0), 1f);
        yield return new WaitForSeconds(1f);
        while (s <= e)
        {
            bool a = IsBigger(boxList[s].Index,standard.Index);
            bool b = IsBigger(standard.Index, boxList[e].Index);
            if (a && b)
            {
                yield return StartCoroutine(SwapObject(s, e));
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
        standard.transform.DOMove(boxList[s].transform.position, 1f);
        boxList[s].transform.DOLocalMove(new Vector3(end * 1.2f, 0, 0),1f);
        GameObjectIndex temp = boxList[s];
        boxList[s] = boxList[end];
        boxList[end] = temp;
        yield return new WaitForSeconds(1f);
        if (start < e)
        {
            queue.Enqueue(new int2(start, e));
        }
        if(s+1 < end)
        {
            queue.Enqueue(new int2(s + 1, end));
        }
    }
    public IEnumerator SwapObject(int a, int b)
    {
        
        Vector3 posA = boxList[a].transform.localPosition;
        Vector3 posB = boxList[b].transform.localPosition;
        Sequence swapSequence = DOTween.Sequence();
        DOTween.Sequence().Append(boxList[a].transform.DOLocalMove(posA + Vector3.up*2, 0.5f))
            .Append(boxList[a].transform.DOLocalMove(posB, 0.5f));
        DOTween.Sequence().Append(boxList[b].transform.DOLocalMove(posB + Vector3.up*2, 0.5f))
            .Append(boxList[b].transform.DOLocalMove(posA, 0.5f));
        yield return new WaitForSeconds(1.2f);
        GameObjectIndex temp = boxList[a];
        boxList[a] = boxList[b];
        boxList[b] = temp;
    }

    public bool IsBigger(int a, int b)
    {
        return a > b;
    }

    
}
