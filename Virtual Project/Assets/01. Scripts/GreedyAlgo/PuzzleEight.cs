using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle8
{
    public class PuzzleEight : MonoBehaviour
    {
        public TextMeshPro signBoard;

        [SerializeField]
        int[] values;

        public UnityEvent isSolved;
        SortedObject[,] puzzlePieces;

        List<Vector3> pos;
        PE_piece[,] piecesPos;

        int count = 0;
        public void Start()
        {
            puzzlePieces = new SortedObject[3, 3];
            piecesPos = new PE_piece[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector3 vec = new Vector3(j - 1, i - 1);
                    var piece = transform.GetChild(0).GetChild(j + i * 3).GetComponent<SortedObject>();
                    piece.Index = values[i * 3 + j];
                    var obj = transform.GetChild(1).GetChild(j + i * 3).GetComponent<PE_piece>();
                    piecesPos[i, j] = obj;
                    obj.transform.localPosition = vec;
                    obj.pos = new intVector2(j - 1, i - 1);
                    puzzlePieces[i, j] = piece;
                    puzzlePieces[i,j].transform.position = obj.transform.position;
                    obj.puzzleEight = this;
                    obj.piece = piece;
                }
            }
            foreach (var piece in piecesPos)
            {
                SetRelativePiece(piece);
                piece.SetPiece();
            }
            puzzlePieces[2, 2].gameObject.SetActive(false);

            count = 0;
        }
        private void Update()
        {
            signBoard.text = count.ToString();
        }

        public PE_piece GetPos(int x, int y)
        {
            if (x > 1 || y > 1) return null;
            if( x<-1 || y<-1) return null;
            return piecesPos[y+1, x+1];
        }

        public void SetRelativePiece(PE_piece piece)
        {
            Debug.Log($"{piece.pos.x}:{piece.pos.y}");
            piece.Left = GetPos(piece.pos.x-1, piece.pos.y);
            piece.Right = GetPos(piece.pos.x + 1, piece.pos.y);
            piece.Up = GetPos(piece.pos.x, piece.pos.y+1);
            piece.Down = GetPos(piece.pos.x, piece.pos.y-1);
        }

        public int SwapPuzzle(PE_piece piece)
        {
            if (!piece.isOccupied) return -1;
            if (piece.Left !=null && !piece.Left.isOccupied)
            {
                _Swap(piece, piece.Left);
                count++;
                return 4;
            }
            else if (piece.Right != null && !piece.Right.isOccupied)
            {
                _Swap(piece, piece.Right);
                count++;
                return 2;
            }
            else if (piece.Up != null && !piece.Up.isOccupied)
            {
                _Swap(piece, piece.Up);
                count++;
                return 1;
            }
            else if (piece.Down != null && !piece.Down.isOccupied)
            {
                _Swap(piece, piece.Down);
                count++;
                return 3;
            }
            else
            {
                Debug.Log("Cant move");
            }
            return -1;
        }
        void _Swap(PE_piece a, PE_piece b)
        {
            var t = a.piece;
            a.piece = b.piece;
            b.piece = t;
            // ÀÌµ¿
            a.piece.transform.DOMove(a.transform.position, 0.5f);
            b.piece.transform.DOMove(b.transform.position, 0.5f);
        }

        public void CheckPuzzle()
        {
            int i = 0;
            foreach (var item in piecesPos)
            {
                Debug.Log($"{item.piece.Index} : {values[i]}");
                if (item.piece.Index != values[i])
                {
                    return;
                }
                i++;
            }
            isSolved?.Invoke();
        }


        public void Reset()
        {
            count = 0;
            for(int i = 0; i < puzzlePieces.Length; i++)
            {
                piecesPos[i / 3, i % 3].piece = puzzlePieces[i / 3, i % 3];
                puzzlePieces[i/3, i % 3].transform.position = piecesPos[i/3,i % 3].transform.position;
                piecesPos[i / 3, i % 3].SetPiece();
            }


        }
    }
}