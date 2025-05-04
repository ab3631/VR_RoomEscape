using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;

public class MergeSort : MonoBehaviour, ISort
{
    public bool canMerge {  get; private set; }
    public MergeSort parent;
    public MergeSort left;
    public MergeSort right;

    public List<SortedObject> indexObjects;
    private void Start()
    {
        isCompleted = false;
        canMerge = false;
        StartCoroutine(Sequence());
        
    }
    public void SortSequence()
    {
        SetMerge();
    }
    IEnumerator Sequence()
    {
        yield return StartCoroutine(MergeSortCoroutine());
        isCompleted = true;
        canMerge = true;
    }
    public bool isCompleted;
    Vector3 pos;
    public IEnumerator MergeSortCoroutine()
    {
        pos = Vector3.zero;
        int mid = indexObjects.Count / 2;
        pos.x -= mid;
        foreach (var obj in indexObjects)
        {
            SetPos(obj.transform);
        }
        yield return new WaitForSeconds(1);
        if (indexObjects.Count > 1)
        {
            pos.x = 0.5f;
            pos.x -= mid;
            GameObject leftObj = new GameObject("Left");
            leftObj.transform.SetParent(transform);
            leftObj.transform.localPosition = new Vector3(-0.7f*mid, -2,0);
            left = leftObj.AddComponent<MergeSort>();
            left.parent = this;
            left.SetObject(indexObjects.Take(mid).ToList());

            GameObject rightObj = new GameObject("Right");
            rightObj.transform.SetParent(transform);
            rightObj.transform.localPosition = new Vector3(0.7f*mid, -2, 0);
            right = rightObj.AddComponent<MergeSort>();
            right.parent = this;
            right.SetObject(indexObjects.Skip(mid).ToList());

            Debug.Log(name + ":" + indexObjects.Count);
            yield return new WaitUntil(() => left.isCompleted && right.isCompleted);
            Debug.Log("delay");
            yield return new WaitUntil(() => canMerge);
            var list = new List<SortedObject>();
            
            while (left.indexObjects.Count > 0 && right.indexObjects.Count > 0)
            {
                if (left.indexObjects[0].Index > right.indexObjects[0].Index)
                {
                    list.Add(right.indexObjects[0]);
                    right.indexObjects.RemoveAt(0);
                    SetPos(list[list.Count - 1].transform);
                }
                else
                {
                    list.Add(left.indexObjects[0]);
                    left.indexObjects.RemoveAt(0);
                    SetPos(list[list.Count - 1].transform);
                }
                yield return new WaitForSeconds(1f);
            }
            while (left.indexObjects.Count > 0)
            {
                list.Add(left.indexObjects[0]);
                left.indexObjects.RemoveAt(0);
                SetPos(list[list.Count - 1].transform);
                yield return new WaitForSeconds(1f);
            }
            while (right.indexObjects.Count > 0)
            {
                list.Add(right.indexObjects[0]);
                right.indexObjects.RemoveAt(0);
                SetPos(list[list.Count - 1].transform);
                yield return new WaitForSeconds(1f);
            }
            indexObjects = list;
        }

    }

    public void SetPos(Transform obj)
    {
        obj.DOMove(transform.position+pos*1.2f,1f);
        pos.x++;
    }

    public void SetMerge()
    {        
        if (left != null && !left.canMerge)
        {
            left.SetMerge();
        }
        else if (right != null && !right.canMerge) 
        {
            right.SetMerge();
        }
        else
        {
            canMerge = true;
            Debug.Log(name + ":" + canMerge);
        }
    }

    public void SetObject(List<SortedObject> list)
    {
        indexObjects = list;
    }
}
