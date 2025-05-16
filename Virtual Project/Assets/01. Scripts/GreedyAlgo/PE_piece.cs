using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Puzzle8
{
    public class PE_piece : MonoBehaviour
    {
        public PuzzleEight puzzleEight;
        public SortedObject piece;
        public intVector2 pos;
        public bool isOccupied => piece.gameObject.activeSelf;


        public PE_piece Up, Down, Left, Right;

        private void Start()
        {
        }
        void InteractionSwap(SelectEnterEventArgs args)
        {
            Debug.Log($"Swap {piece.Index}");
            int i = puzzleEight.SwapPuzzle(this);
            switch (i)
            {
                case 1: Up.SetPiece(); break;
                case 2: Right.SetPiece(); break;
                case 3: Down.SetPiece(); break;
                case 4: Left.SetPiece(); break;
            }
            SetPiece();

            puzzleEight.CheckPuzzle();
        }
        public void SetPiece()
        {
            piece.GetComponent<XRSimpleInteractable>().firstSelectEntered.RemoveAllListeners();
            piece.GetComponent<XRSimpleInteractable>().firstSelectEntered.AddListener(InteractionSwap);
        }


    }
}

public struct intVector2
{
    public int x, y;
    public intVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
