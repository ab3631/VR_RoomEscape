using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuickSort : MonoBehaviour
{
    public SortAlgorithm algorithm;

    public event Action OnSortEnd;

    public List<SortedObject> indexObjects;
    public List<Transform> posList;

    SortedObject standardObject;
    private void Start()
    {
    }
    public void SetObject(List<SortedObject> list, List<Transform> positions, Action callback)
    {
        indexObjects = list;
        posList = positions;
        OnSortEnd = callback;
        standardObject = null;
        StartCoroutine(QuickSortCoroutine());
        
    }

    void Setstandard(SortedObject obj)
    {
        Debug.Log("SetStandard");
        standardObject = obj;
    }

    IEnumerator QuickSortCoroutine()
    {
        yield return null;
        foreach (SortedObject obj in indexObjects)
        {
            obj.OnClicked += Setstandard;
            obj.GetComponent<Collider>().enabled = true;
        }
        yield return new WaitUntil(()=>  standardObject != null);

        algorithm.AddComplexity(indexObjects.Count);

        foreach (SortedObject obj in indexObjects)
        {
            obj.OnClicked -= Setstandard;
            obj.GetComponent<Collider>().enabled = false;
        }

        

        indexObjects.Remove(standardObject);
        standardObject.transform.SetParent(standardObject.transform.parent);
        
        standardObject.transform.DOLocalMove(Vector3.up*4, 1f);
        
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
                // up
                indexObjects[s].transform.DOLocalMove(indexObjects[s].transform.localPosition + Vector3.up, 0.5f);
                indexObjects[e].transform.DOLocalMove(indexObjects[e].transform.localPosition + Vector3.up, 0.5f);
                yield return new WaitForSeconds(0.5f);
                // switch
                Transform leftP = indexObjects[s].transform.parent;
                Transform rightP = indexObjects[e].transform.parent;
                indexObjects[s].transform.SetParent(rightP);
                indexObjects[e].transform.SetParent(leftP);
                indexObjects[s].transform.DOLocalMove(new Vector3(0, 3, 1), 1f);
                indexObjects[e].transform.DOLocalMove(new Vector3(0, 3, 1), 1f);
                yield return new WaitForSeconds(1f);
                // down
                indexObjects[s].transform.DOLocalMove(new Vector3(0,2,1), 0.5f);
                indexObjects[e].transform.DOLocalMove(new Vector3(0,2,1), 0.5f);
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
            indexObjects[i].transform.SetParent(posList[i]);
            indexObjects[i].transform.DOLocalMove(new Vector3(0, 2, 1), 1f);
        }
        yield return new WaitForSeconds(1f);

        var leftList = indexObjects.Take(e + 1).ToList();
        var leftPoses = posList.Take(e + 1).ToList();
        var rightList = indexObjects.Skip(s + 1).ToList();
        var rightPoses = posList.Skip(s + 1).ToList();
        if (leftList.Count > 1)
        {
            // 왼쪽 내부 정렬 생성
            GameObject obj = new GameObject("Left");
            CreateInternalSort(obj, leftList,leftPoses);
        }
        else
        {
            foreach (var item in leftList)
            {
                item.SetFixed(true);
            }
        }
        if (rightList.Count>1) {
            // 오른쪽 내부 정렬 생성
            GameObject obj = new GameObject("Right");
            CreateInternalSort(obj, rightList,rightPoses);
        }
        else
        {
            foreach (var item in rightList)
            {
                item.SetFixed(true);
            }
        }
        standardObject.SetFixed(true);
        OnSortEnd?.Invoke();
    }
    QuickSort CreateInternalSort(GameObject obj, List<SortedObject> list, List<Transform> positions)
    {
        obj.transform.SetParent(transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;
        var sort = obj.AddComponent<QuickSort>();
        sort.algorithm = algorithm;
        // 위치 계산
        var avg = list.Sum((obj) => obj.transform.localPosition.x) / (list.Count);
        obj.transform.localPosition = new Vector3(avg, 0, 0);
        sort.SetObject(list,positions,OnSortEnd);
        return sort;
    }
}
