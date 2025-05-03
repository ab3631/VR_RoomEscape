using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;

public class MergeSort : MonoBehaviour
{
    public MergeSort parent;
    public MergeSort left;
    public MergeSort right;

    public List<GameObjectIndex> indexObjects;
    private void Start()
    {
        isCompleted = false;

        StartCoroutine(Sequence());
        
    }
    IEnumerator Sequence()
    {
        yield return StartCoroutine(MergeSortCoroutine());
        isCompleted = true;
    }
    public bool isCompleted;
    Vector3 pos;
    public IEnumerator MergeSortCoroutine()
    {
        pos = Vector3.zero;
        if (indexObjects.Count > 1)
        {
            int mid = indexObjects.Count / 2;
            pos.x -= mid;
            GameObject leftObj = new GameObject("Left");
            leftObj.transform.SetParent(transform);
            leftObj.transform.localPosition = new Vector3(-0.7f*mid, -2,0);
            left = leftObj.AddComponent<MergeSort>();
            left.parent = this;
            left.indexObjects = indexObjects.Take(mid).ToList();

            GameObject rightObj = new GameObject("Right");
            rightObj.transform.SetParent(transform);
            rightObj.transform.localPosition = new Vector3(0.7f*mid, -2, 0);
            right = rightObj.AddComponent<MergeSort>();
            right.parent = this;
            right.indexObjects = indexObjects.Skip(mid).ToList();
            yield return new WaitUntil(() => left.isCompleted && right.isCompleted);
            var list = new List<GameObjectIndex>();
            
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
}
