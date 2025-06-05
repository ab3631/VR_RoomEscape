using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PuzzleEightTree : MonoBehaviour
{
    public int[] numbers;
    int[,] nums;

    public GameObject prefab;

    public UnityEvent IsSolved;

    private void Start()
    {
        nums = new int[3, 3];
        for (int i = 0; i < 9; i++)
        {
            nums[i / 3, i % 3] = numbers[i];
        }
        SetTree(nums);
        
    }

    public void SetTree(int[,] nums)
    {
        // 오브젝트 제거
        for (int i = transform.childCount - 1; i >= 0; i--) {
            
            Destroy(transform.GetChild(i).gameObject);
        }
        
        var instance = CreatePuzzleViewer(nums,false);
        instance.transform.localPosition = Vector3.zero;
        CheckPuzzle(instance);
        SetLeaf(nums);
    }

    public void SetLeaf(int[,] nums)
    {
        List<PuzzleViewer> list = new List<PuzzleViewer>();
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (nums[i,j] == 9)
                {                    
                    if (i < 2)
                    {
                        var temp = nums.Clone() as int[,];
                        temp[i, j] = temp[i + 1, j];
                        temp[i + 1, j] = 9;
                        //temp을 그리는 퍼즐
                        list.Add(CreatePuzzleViewer(temp));
                    }
                    if(i > 0)
                    {
                        var temp = nums.Clone() as int[,];
                        temp[i, j] = temp[i - 1, j];
                        temp[i - 1, j] = 9;
                        //temp을 그리는 퍼즐
                        list.Add(CreatePuzzleViewer(temp));
                    }
                    if (j > 0)
                    {
                        var temp = nums.Clone() as int[,];
                        temp[i, j] = temp[i, j - 1];
                        temp[i, j - 1] = 9;
                        //temp을 그리는 퍼즐
                        list.Add(CreatePuzzleViewer(temp));
                    }
                    if(j < 2)
                    {
                        var temp = nums.Clone() as int[,];
                        temp[i, j] = temp[i, j + 1];
                        temp[i, j + 1] = 9;
                        //temp을 그리는 퍼즐
                        list.Add(CreatePuzzleViewer(temp));
                    }
                    for(int k = 0; k < list.Count; k++)
                    {
                        
                        list[k].transform.localPosition = new Vector3((k-((list.Count-1)/2f))*4, -4, 0);
                    }
                    return;
                }
            }
        }
    }

    PuzzleViewer CreatePuzzleViewer(int[,] nums,bool isClickable= true)
    {
        var instance = Instantiate(prefab, transform);
        instance.transform.localPosition = Vector3.zero;
        var viewer = instance.GetComponent<PuzzleViewer>();
        viewer.Init(nums);

        if (isClickable)
        {
            viewer.clickEvent.selectEntered.AddListener(_ =>
            {
                SetTree(nums);
            });
        }

        return viewer;
    }
    void CheckPuzzle(PuzzleViewer puzzle)
    {
        int count = puzzle.CheckCorrect();
        if (count == 9)
        {
            IsSolved?.Invoke();
        }
    }
}
