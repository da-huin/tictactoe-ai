using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace TicTacToe
{
    public class nBoard
    {
        public nPiece[,] pieces = new nPiece[3, 3];
        public int PuttingCount
        {
            get
            {
                int count = 0;
                Tool.DoTicTacToeArray((x, y) =>
                {
                    if ((pieces[x, y].player != Players.NULL))
                    {
                        count++;
                    }
                    return true;
                });
                return count;
            }
        }

        public nPoint lastPutPosition = null;


        public nBoard()
        {
            Tool.DoTicTacToeArray((x, y) =>
            {

                pieces[x, y] = new nPiece();
                pieces[x, y].position = new nPoint(x, y);
                pieces[x, y].player = Players.NULL;
                return true;
            });
        }

        public nBoard(nBoard board)
        {
            Tool.DoTicTacToeArray((x, y) =>
            {
                pieces[x, y] = new nPiece();
                pieces[x, y].position = new nPoint(x, y);
                pieces[x, y].player = board[x, y].player;


                lastPutPosition = new nPoint(board.lastPutPosition.x, board.lastPutPosition.y);
                return true;
            });
        }

        public nPiece this[int x, int y]
        {
            get { return pieces[x, y]; }
        }


        public nPiece this[nPoint position]
        {
            get { return pieces[position.x, position.y]; }
        }


        public static implicit operator nPiece[,] (nBoard board)
        {
            return board.pieces;
        }


    }


    public class nPiece
    {
        public nPoint position;
        public char player;
    }

    public class nPoint
    {
        public int x;
        public int y;


        public nPoint() { }
        public nPoint(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    public static class Players
    {
        public static char O
        {
            get
            {
                return 'O';
            }
        }
        public static char X
        {
            get
            {
                return 'X';
            }
        }
        public static char NULL
        {
            get
            {
                return 'N';
            }
        }
    }


    public partial class nRecord
    {
        private List<nBoard> recordList;
        private int mover = 0;
        private int Mover
        {
            get { return mover; }
            set
            {
                if (0 >= value)
                {
                    mover = 0;
                }
                else if (value >= recordList.Count)
                {
                    mover = recordList.Count - 1;
                }
                else
                {
                    mover = value;
                }

            }
        }

        public char winner;
        public char loser;
        public bool IsGameEnd = false;


        public void MoverFirst()
        {
            Mover = 0;
        }

        public bool MoveNext()
        {
            int prevMover = Mover;
            Mover++;
            if (prevMover == Mover)
            {
                return false;
            }
            return true;
        }
        public bool MoverPrev()
        {
            int prevMover = Mover;
            Mover--;

            if (prevMover == Mover)
            {
                return false;
            }
            return true;
        }

        public nBoard GetBoard()
        {
            return recordList[Mover];
        }

        public void AddRecord(nBoard board)
        {
            nBoard newBoard = new nBoard(board);
            recordList.Add(newBoard);
        }

        public bool RemoveRecordLast()
        {
            if (recordList.Count == 0)
                return false;
            else
            {
                recordList.RemoveAt(recordList.Count - 1);
                return true;
            }
        }

        public bool IsDraw()
        {
            if (winner == Players.NULL && loser == Players.NULL)
                return true;
            else
                return false;
        }

        public nRecord()
        {
            recordList = new List<nBoard>();
            winner = Players.NULL;
            loser = Players.NULL;
            Mover = 0;
        }

    }
}
