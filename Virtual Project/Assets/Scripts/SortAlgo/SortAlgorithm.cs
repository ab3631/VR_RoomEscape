using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

using UnityEngine;

public class SortAlgorithm : MonoBehaviour
{
    public int listNum;
    public GameObject boxPrefab;
    public ISort sort;
    private void Awake()
    {
        sort = GetComponentInChildren<ISort>();
        sort.SetObject(SetBlock());

    }
    [ContextMenu("SetBlockList")]
    public List<SortedObject> SetBlock()
    {
        var list = new List<SortedObject>();
        var numList = Enumerable.Range(1,listNum+1).ToList();
        for(int i = 0; i < listNum; i++)
        {
            var childObj = Instantiate(boxPrefab,transform);
            var index = childObj.GetComponent<SortedObject>();
            list.Add(index);
            var rand = UnityEngine.Random.Range(0, numList.Count - 1);
            index.Index = numList[rand];
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.localPosition = new Vector3((i-listNum/2f) * 1.2f, 0, 0);
        }
        return list;
    }

    
    public void Execute()
    {
        sort.SortSequence();
    }
    
}
