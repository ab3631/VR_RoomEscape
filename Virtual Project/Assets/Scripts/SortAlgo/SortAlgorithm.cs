using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SortAlgorithm : MonoBehaviour
{
    int listNum;

    public GameObject Runes;
    public List<GameObject> objList;

    public GameObject Pillars;
    public List<Transform> posList;
    public ISort sort;

    
    private void Awake()
    {
        sort = GetComponentInChildren<ISort>();
        var list = SetBlock();
        sort.SetObject(list, posList,isSorted);
        
        
    }
    [ContextMenu("SetRune&Pillar")]
    public void SetLists()
    {
        objList = new List<GameObject>();
        for (int i = 0; i < Runes.transform.childCount; i++)
        {
            objList.Add(Runes.transform.GetChild(i).gameObject);
        }

        posList = new List<Transform>();
        for(int i = 0; i < Pillars.transform.childCount; i++)
        {
            posList.Add(Pillars.transform.GetChild(i));
        }
    }

    List<SortedObject> sortedObjectList;
    public List<SortedObject> SetBlock()
    {
        sortedObjectList = new List<SortedObject>();
        listNum = objList.Count;
        for(int i = 0; i < listNum; i++)
        {
            var index = objList[i].AddComponent<SortedObject>();
            sortedObjectList.Add(index);
            index.Index = i+1;
            index.OnClicked += AddAnswer;
        }
        for (int i = 0; i < listNum; i++)
        {
            var rand = Random.Range(0, sortedObjectList.Count-i);
            var temp = sortedObjectList[i];
            sortedObjectList[i] = sortedObjectList[rand];
            sortedObjectList[rand] = temp;
        }

        for (int i = 0; i < sortedObjectList.Count; i++)
        {
            sortedObjectList[i].transform.SetParent(posList[i]);
            sortedObjectList[i].transform.localPosition = new Vector3(0, 3, 1);
        }
        return sortedObjectList;
    }

    
    public void Execute()
    {
        sort.SortSequence();
    }

    public List<int> Answer;
    void AddAnswer(SortedObject obj)
    {
        if(Answer == null) Answer = new List<int>();
        Answer.Add(obj.Index);
        ShowAnswer();
    }
    public void ShowAnswer()
    {
        string s = "Answer : ";
        s += GetAnswer();
        Debug.Log(s);
        //GetComponentInChildren<TextMeshPro>().text = s;
    }
    public string GetAnswer()
    {
        string s = "";
        foreach (var item in Answer)
        {
            s += item.ToString();
        }
        return s;
    }
    void isSorted()
    {
        foreach (var item in sortedObjectList)
        {
            if (item.isFixed == false) return;
        }
        OnSorted?.Invoke();
    }
    public UnityEvent OnSorted;
}
