using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuickSort : MonoBehaviour, ISort
{
    

    public List<SortedObject> indexObjects;

    SortedObject standardObject;
    private void Start()
    {
        StartCoroutine(QuickSortCoroutine());
        Debug.Log(transform.position);
    }
    public void SetObject(List<SortedObject> list)
    {
        indexObjects = list;
        foreach (SortedObject obj in indexObjects)
        {
            obj.transform.SetParent(transform);
        }
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
        foreach (SortedObject obj in indexObjects)
        {
            obj.OnClicked = null;
        }

        Debug.Log("StandardObject is exist");
        indexObjects.Remove(standardObject);
        standardObject.SetFixed();
        standardObject.transform.DOLocalMove(Vector3.up*2, 1f);
        for (int i = 0; i < indexObjects.Count; i++)
        {
            indexObjects[i].transform.DOLocalMove(new Vector3((i + 0.5f - indexObjects.Count / 2f) * 1.2f, 0, 0), 1f);
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
                indexObjects[s].transform.DOLocalMove(indexObjects[s].transform.localPosition + Vector3.up, 0.5f);
                indexObjects[e].transform.DOLocalMove(indexObjects[e].transform.localPosition + Vector3.up, 0.5f);
                yield return new WaitForSeconds(0.5f);
                Vector3 leftPos = indexObjects[s].transform.position;
                Vector3 rightPos = indexObjects[e].transform.position;
                indexObjects[s].transform.DOMove(rightPos, 1f);
                indexObjects[e].transform.DOMove(leftPos, 1f);
                yield return new WaitForSeconds(1f);
                indexObjects[s].transform.DOLocalMove(indexObjects[s].transform.localPosition + Vector3.down, 0.5f);
                indexObjects[e].transform.DOLocalMove(indexObjects[e].transform.localPosition + Vector3.down, 0.5f);
                yield return new WaitForSeconds(0.5f);
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
            indexObjects[i].transform.DOLocalMove(new Vector3((i + 0.5f - indexObjects.Count / 2f) * 1.2f, 0, 0), 1f);
        }
        yield return new WaitForSeconds(1f);

        var leftList = indexObjects.Take(e + 1).ToList();
        var rightList = indexObjects.Skip(s + 1).ToList();
        if (leftList.Count > 1)
        {
            // 왼쪽 내부 정렬 생성
            GameObject obj = new GameObject("Left");
            CreateInternalSort(obj, leftList);
        }
        else
        {
            foreach (var item in leftList)
            {
                item.SetFixed();
            }
        }
        if (rightList.Count>1) {
            // 오른쪽 내부 정렬 생성
            GameObject obj = new GameObject("Right");
            CreateInternalSort(obj, rightList);
        }
        else
        {
            foreach (var item in rightList)
            {
                item.SetFixed();
            }
        }
    }
    QuickSort CreateInternalSort(GameObject obj, List<SortedObject> list)
    {
        obj.transform.SetParent(transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;
        var sort = obj.AddComponent<QuickSort>();
        // 위치 계산
        Debug.Log($"count {list.Count} + distance sum = {list.Sum((obj) => obj.transform.localPosition.x)}");
        var avg = list.Sum((obj) => obj.transform.localPosition.x) / (list.Count);
        Debug.Log($"avg = {avg}");
        obj.transform.localPosition = new Vector3(avg, 0, 0);
        sort.SetObject(list);
        return sort;
    }
}
