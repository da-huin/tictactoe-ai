using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public partial class Share
    {
        protected static nPoint GetAnyVoidPos(nBoard board)
        {
            return GetVoidPosList(board)[0];
        }

        protected static List<nPoint> GetVoidPosList(nBoard board)
        {
            List<nPoint> posList = new List<nPoint>();
            Tool.DoTicTacToeArray((x, y) =>
            {
                if (board[x, y].player == Players.NULL)
                {
                    posList.Add(new nPoint(x, y));
                }
                return true;
            });
            return posList;
        }

        protected static int randIntSeed;
        protected static nPoint GetRandomVoidPos(nBoard board)
        {
            randIntSeed++;
            Random randSeed = new Random();
            Random rand = new Random(randSeed.Next() * randIntSeed);


            var voidList = GetVoidPosList(board);
            return voidList[rand.Next(voidList.Count)];
        }


        protected static char GetOtherPlayer(char player)
        {
            if (player == Players.O)
                return Players.X;
            else if (player == Players.X)
                return Players.O;
            else
                return Players.NULL;

        }

        protected static nPiece[] GetLine(nBoard board, int line, char XYD)
        {
            nPiece[] retValue = new nPiece[3];
            if (XYD == 'X')
            {
                retValue[0] = board[0, line];
                retValue[1] = board[1, line];
                retValue[2] = board[2, line];
            }
            else if (XYD == 'Y')
            {
                retValue[0] = board[line, 0];
                retValue[1] = board[line, 1];
                retValue[2] = board[line, 2];
            }
            else if (XYD == 'D')
            {
                if (line == 0)
                {
                    retValue[0] = board[0, 0];
                    retValue[1] = board[1, 1];
                    retValue[2] = board[2, 2];
                }
                else if (line == 1)
                {
                    retValue[0] = board[2, 0];
                    retValue[1] = board[1, 1];
                    retValue[2] = board[0, 2];
                }
            }
            return retValue;
        }

        protected static List<nPiece[]> GetCliffLine(nBoard board)
        {
            List<nPiece[]> cliffLineArrList = new List<nPiece[]>();
            Tool.DoTicTacToeArray((x, y) =>
            {
                nPiece[] lineArr;
                for (int i = 0; i < 3; i++)
                {
                    lineArr = GetLine(board, i, 'X');
                    if (IsThisLineCliff(lineArr)) cliffLineArrList.Add(lineArr);
                    lineArr = GetLine(board, i, 'Y');
                    if (IsThisLineCliff(lineArr)) cliffLineArrList.Add(lineArr);
                    if (i < 2)
                    {
                        lineArr = GetLine(board, i, 'D');
                        if (IsThisLineCliff(lineArr)) cliffLineArrList.Add(lineArr);
                    }
                }
                return true;
            });

            return cliffLineArrList;
        }

        protected static bool IsThisLineCliff(nPiece[] lineArr)
        {
            int NULLPlayerCount = 0;
            for (int i = 0; i < 3; i++)
            {
                if (lineArr[i].player == Players.NULL)
                    NULLPlayerCount++;
            }

            if (NULLPlayerCount == 1)
            {
                if (lineArr[0].player == lineArr[1].player
                || lineArr[1].player == lineArr[2].player
                || lineArr[0].player == lineArr[2].player)
                {
                    return true;
                }
            }
            return false;
        }

        protected static nPoint GetCliffPos(nBoard board, char currentTurnPlayer)
        {
            nPoint retValue = null;
            var cliffArrList = GetCliffLine(board);



            if (cliffArrList.Count != 0)
            {
                foreach (var piece in cliffArrList)
                {
                    char cliffPlayer = Players.NULL;
                    nPoint voidPoint = null;
                    for (int i = 0; i < 3; i++)
                    {
                        if (piece[i].player != Players.NULL)
                        {
                            cliffPlayer = piece[i].player;
                        }
                        else
                            voidPoint = piece[i].position;
                    }

                    // 위험한 라인이 자신의 것 일때
                    if (currentTurnPlayer == cliffPlayer)
                    {
                        retValue = voidPoint;
                    }
                    // 위험한 라인이 상대의 것 일때
                    else
                    {
                        if (retValue == null)
                        {
                            retValue = voidPoint;
                        }
                    }
                }
            }
            return retValue;
        }

        protected static bool IsSameLineExist(nBoard board)
        {
            if (Tool.GetSameLine(board) == null)
                return false;
            else
                return true;
        }


        protected static bool IsSameLine(nBoard board, int line, char XYD)
        {
            var linePieces = GetLine(board, line, XYD);
            if (linePieces[0].player == linePieces[1].player && linePieces[1].player == linePieces[2].player)
            {
                if (linePieces[0].player != Players.NULL)
                {
                    return true;
                }
            }
            return false;
        }




    }
}