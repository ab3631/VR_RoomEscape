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
    public List<SortedObject> sortedObjectList;

    public GameObject Pillars;
    public List<Transform> posList;
    public QuickSort sort;

    public TextMeshPro signBoard;
    public void Init()
    {
        var list = SetBlock();
        sort.algorithm = this;
        sort.SetObject(list, posList, isSorted);
        for(int i=sort.transform.childCount-1; i >= 0; i--)
        {
            Destroy(sort.transform.GetChild(i).gameObject);
        }
        foreach(SortedObject obj in sortedObjectList)
        {
            obj.SetFixed(false);
            obj.GetComponent<Collider>().enabled = true;
        }
        Answer.Clear();
        Complexity.Clear();
        ShowAnswer();
    }
    private void Awake()
    {
        sort = GetComponentInChildren<QuickSort>();
        Init();
    }
    [ContextMenu("SetRune&Pillar")]
    public void SetLists()
    {
        sortedObjectList = new List<SortedObject>();
        for (int i = 0; i < Runes.transform.childCount; i++)
        {
            var child = Runes.transform.GetChild(i);
            SortedObject temp;
            if (child.GetComponent<SortedObject>() == null)
            {
                 temp = child.AddComponent<SortedObject>();
            }
            else
            {
                temp = child.GetComponent<SortedObject>();
            }
            sortedObjectList.Add(temp);
            
        }

        posList = new List<Transform>();
        for(int i = 0; i < Pillars.transform.childCount; i++)
        {
            posList.Add(Pillars.transform.GetChild(i));
        }
    }

    public List<SortedObject> SetBlock()
    {
        listNum = sortedObjectList.Count;
        for(int i = 0; i < listNum; i++)
        {
            var index = sortedObjectList[i];
            index.Index = i+1;
            index.OnClicked = AddAnswer;
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
            sortedObjectList[i].transform.localPosition = new Vector3(0, 2, 1);
        }
        return sortedObjectList;
    }

    

    public List<int> Answer;
    void AddAnswer(SortedObject obj)
    {
        if(Answer == null) Answer = new List<int>();
        Answer.Add(obj.Index);
        
    }
    public void ShowAnswer()
    {
        //string s = "Count : ";
        //s += GetAnswer();
        signBoard.text = GetComplexity().ToString();
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
        if(GetComplexity() > 41)
        {
            Init();
        }
        else
        {
            OnSorted?.Invoke();
        }
    }
    public UnityEvent OnSorted;


    public List<int> Complexity = new List<int>();
    public void AddComplexity(int i)
    {
        Complexity.Add(i);
        Debug.Log(i);
        ShowAnswer();
    }
    public int GetComplexity()
    {
        int i = Complexity.Sum();
        Debug.Log(i);
        return i;
        
    }
}
