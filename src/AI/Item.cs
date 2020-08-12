using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public partial class AI : Share
    {
        private static int leaningCount;
        public static int LeaningCount
        {
            get { return leaningCount; }
        }

        public static void LearnEnd()
        {
            Game.NewGame('O');
        }

        /// <summary>
        /// 다음 수를 AI가 선택해서 포지션을 반환합니다.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static nPoint GetPutNextPosition(nBoard board)
        {
            if (IsSameLineExist(board)) return null;
            if (Tool.IsFullBoard(board)) return null;
            // board에서 게임 끝날 라인이 있는지 확인한다.

            var AIBoardList = brain[board.PuttingCount];
            int listIndex = 0;

            nPoint putPos;
            putPos = GetCliffPos(board, Game.GetCurrentTurn());

            if (putPos == null)
            {    // 이 보드를 가지고 있다면?
                if (IsBrainGettedBoard(board, AIBoardList, out listIndex))
                {
                    // 최고의 승률 포지션 구하기
                    float bestWinAverage = 0;
                    nPoint bestWinAveragePos = new nPoint();
                    Tool.DoTicTacToeArray((x, y) =>
                    {
                        if (board[x, y].player != Players.NULL) return true;

                        float nowWinAverage = AIBoardList[listIndex][x, y].WinAverage;
                        if (nowWinAverage >= bestWinAverage)
                        {
                            bestWinAverage = nowWinAverage;
                            bestWinAveragePos.x = x;
                            bestWinAveragePos.y = y;


                        }
                        return true;
                    });

                    putPos = bestWinAveragePos;
                }
                else
                    putPos = GetAnyVoidPos(board);
            }

            return putPos;
        }


        /// <summary>
        /// AI가 완료된 기보로 배웁니다.
        /// </summary>
        /// <param name="record"></param>
        public static void Learn(nRecord record)
        {
            if (record.IsGameEnd == false) return;
            leaningCount++;
            record.MoverFirst();

            do
            {
                var board = record.GetBoard();
                if (board.lastPutPosition != null)
                {
                    char lastPosPlayer = board[board.lastPutPosition].player;

                    // 무승부가 아닐 때
                    if (record.IsDraw() == false)
                    {
                        if (lastPosPlayer == record.winner)
                        {
                            AddKnowledge(board, board.lastPutPosition, 'W');
                        }
                        else if (lastPosPlayer == record.loser)
                        {
                            AddKnowledge(board, board.lastPutPosition, 'L');
                        }
                    }
                    // 무승부일 때
                    else
                    {
                        AddKnowledge(board, board.lastPutPosition, 'D');
                    }
                }
            }
            while (record.MoveNext());
        }

        /// <summary>
        /// 스스로 한 번 습득합니다.
        /// </summary>
        public static void LearnSolo()
        {
            Learn(Tool.MakeSaveRuleAnyRecord());
            //Test.Program.WriteAllRecord(Game.GetRecord());
        }

        public static float GetWinAverage(nBoard board, nPoint position)
        {
            if (GetAIPieceArr(board) != null)
                return GetAIPieceArr(board)[position.x, position.y].WinAverage;
            else
                return 0;
        }

        public static int GetBrainBoardCount(int index)
        {
            return brain[index].Count;
        }

        public static int[] GetWDLCount(nBoard board, nPoint position)
        {
            int[] WDL = new int[3];
            if (GetAIPieceArr(board) != null)
            {
                WDL[0] = GetAIPieceArr(board)[position.x, position.y].winCount;
                WDL[1] = GetAIPieceArr(board)[position.x, position.y].drawCount;
                WDL[2] = GetAIPieceArr(board)[position.x, position.y].loseCount;
            }
            return WDL;
        }

        public static void BrainInitalize()
        {
            brain = new Dictionary<int, List<nAIBoard>>()
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
            leaningCount = 0;
        }

    }
}
