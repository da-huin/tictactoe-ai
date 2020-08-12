using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public partial class AI : Share
    {
        private static Dictionary<int, List<nAIBoard>> brain = new Dictionary<int, List<nAIBoard>>()
        {
            { 0, new List<nAIBoard>()},
            { 1, new List<nAIBoard>()} ,
            { 2, new List<nAIBoard>()} ,
            { 3, new List<nAIBoard>()} ,
            { 4, new List<nAIBoard>()} ,
            { 5, new List<nAIBoard>()} ,
            { 6, new List<nAIBoard>()} ,
            { 7, new List<nAIBoard>()} ,
            { 8, new List<nAIBoard>()} ,
            { 9, new List<nAIBoard>()} , // 버그제거테스트
        };

        private class nAIPiece
        {
            public nPiece piece;
            public bool IsAIPiece = false;
            public int winCount = 0;
            public int loseCount = 0;
            public int drawCount = 0;
            public int AllCount { get { return winCount + loseCount + drawCount; } }
            public float WinAverage
            {
                get
                {
                    if (AllCount == 0)
                        return 0;
                    return ((float)winCount / (float)AllCount) * 100;
                }
            }
        }

        private class nAIBoard
        {

            public nAIPiece[,] AIPieceArr = new nAIPiece[3, 3];
            public nAIBoard(nBoard board)
            {
                Tool.DoTicTacToeArray((x, y) =>
                {
                    nBoard newBoard = new nBoard(board);
                    AIPieceArr[x, y] = new nAIPiece();
                    AIPieceArr[x, y].piece = newBoard[x, y];
                    return true;
                });
            }

            public nAIPiece this[int x, int y]
            {
                get { return AIPieceArr[x, y]; }
            }

            public nAIPiece this[nPoint position]
            {
                get { return AIPieceArr[position.x, position.y]; }
            }


            public static implicit operator nAIPiece[,] (nAIBoard AIboard)
            {
                return AIboard.AIPieceArr;

            }

        }

        private static void AddKnowledge(nBoard board, nPoint lastPos, char WLD)
        {
            int listIndex = 0;
            var AIBoardList = brain[board.PuttingCount - 1];
            nBoard prevBoard = new nBoard(board);
            prevBoard[lastPos].player = Players.NULL;

            // 하나 뺀 보드를 주어야 한다.
            bool IsGetted = IsBrainGettedBoard(prevBoard, AIBoardList, out listIndex);


            // 같은 것이 있을 때
            if (IsGetted)
            {
                if (WLD == 'W')
                    AIBoardList[listIndex][lastPos].winCount++;
                else if (WLD == 'L')
                    AIBoardList[listIndex][lastPos].loseCount++;
                else if (WLD == 'D')
                    AIBoardList[listIndex][lastPos].drawCount++;

            }
            // 같은 것이 없을 때
            else
            {
                nAIBoard newAIBoard = new nAIBoard(prevBoard);
                if (WLD == 'W')
                    newAIBoard[lastPos].winCount++;
                else if (WLD == 'L')
                    newAIBoard[lastPos].loseCount++;
                else if (WLD == 'D')
                    newAIBoard[lastPos].drawCount++;

                AIBoardList.Add(newAIBoard);
            }

        }
        private static bool IsBrainGettedBoard(nBoard board, List<nAIBoard> AIBoardList, out int listIndex)
        {
            bool IsSame = false;
            listIndex = 0;
            // 보드의 O X 반전
            for (int reverse = 0; reverse < 2; reverse++)
            {
                for (int i = 0; i < AIBoardList.Count; i++)
                {
                    IsSame = true;
                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            char AIPlayer = AIBoardList[i][x, y].piece.player;

                            if (reverse == 1)
                                AIPlayer = GetOtherPlayer(AIPlayer);

                            if (AIPlayer != board[x, y].player)
                            {
                                IsSame = false;
                                goto BreakXY;
                            }
                        }
                    }
                    if (IsSame == true)
                    {

                        listIndex = i;
                        return true;
                    }
                    BreakXY:;
                }
            }


            return false;
        }


        private static nBoard AIBoardToBoard(nAIBoard aiBoard)
        {
            nBoard newBoard = new nBoard();
            Tool.DoTicTacToeArray((x, y) =>
            {
                newBoard.pieces[x, y] = aiBoard[x, y].piece;
                return true;
            });
            return newBoard;
        }

        private static nAIPiece[,] GetAIPieceArr(nBoard board)
        {
            int listIndex = 0;
            var AIBoardList = brain[board.PuttingCount];
            if (IsBrainGettedBoard(board, AIBoardList, out listIndex))
            {
                return AIBoardList[listIndex].AIPieceArr;
            }
            return null;
        }


    }
}



