using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour, ISort
{
    

    public List<SortedObject> indexObjects;

    SortedObject standardObject;
    private void Start()
    {
        StartCoroutine(QuickSortCoroutine());
    }
    public void SetObject(List<SortedObject> list)
    {
        indexObjects = list;
    }

    public void SortSequence()
    {
    }

    IEnumerator QuickSortCoroutine()
    {
        yield return null;
        foreach (SortedObject obj in indexObjects)
        {
            obj.OnClicked = () => standardObject = obj;
        }
        yield return new WaitUntil(()=>  standardObject != null);

        indexObjects.Remove(standardObject);
        standardObject.SetFixed();
        standardObject.transform.DOLocalMove(Vector3.up*2, 1f);
        for (int i = 0; i < indexObjects.Count; i++)
        {
            indexObjects[i].transform.DOLocalMove(new Vector3((i -indexObjects.Count/2f) * 1.2f, 0, 0),1f);
        }
        yield return new WaitForSeconds(1f);
        int s = 0;
        int e = indexObjects.Count - 1;
        while (s <= e)
        {
            bool left = indexObjects[s].Index > standardObject.Index;
            bool right = standardObject.Index > indexObjects[e].Index;
            if (left && right)
            {
                // 시각화 정렬
                Vector3 leftPos = indexObjects[s].transform.position;
                Vector3 rightPos = indexObjects[e].transform.position;
                indexObjects[s].transform.DOMove(rightPos, 1f);
                indexObjects[e].transform.DOMove(leftPos, 1f);
                yield return new WaitForSeconds(1f);
                // 리스트 내부 정렬
                var temp = indexObjects[s];
                indexObjects[s] = indexObjects[e];
                indexObjects[e] = temp;
                s++;
                e--;
            }
            else
            {
                if (!left)
                {
                    s++;
                }
                if (!right)
                {
                    e--;
                }
            }
        }
        indexObjects.Insert(s, standardObject);
        for (int i = 0; i < indexObjects.Count; i++)
        {
            indexObjects[i].transform.DOLocalMove(new Vector3((i - indexObjects.Count / 2f) * 1.2f, 0, 0), 1f);
        }

    }

}
