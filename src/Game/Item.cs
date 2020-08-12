using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{

    public partial class Game : Share
    {
        /// <summary>
        /// 새로운 게임을 시작합니다.
        /// </summary>
        public static bool NewGame(char startPlayer)
        {
            
            if ((startPlayer != Players.O) && (startPlayer != Players.X))
                return false;

            currentTurn = startPlayer;
            board = new nBoard();

            Recorder.NewRecord();
            return true;
        }


        /// <summary>
        /// 다음 수를 둡니다.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool PutNext(nPoint position, bool withAIGame)
        {
            if (position == null) return false;
            if (IsAvailablePosition(position) == false) return false;


            if (IsCanNext(position) == true)
            {
                // 바뀌기 이전을 기록한다.
                board.lastPutPosition = position;

                board[position].player = currentTurn;

                Recorder.RecordNext(board);

                ChangeTurn();


                if (IsSameLineExist(board) || Tool.IsFullBoard(board))
                {
                    currentTurn = Players.NULL;
                    SetWL();
                    Recorder.RecordEndGame(winner, loser);

                    if(withAIGame == false)
                        AI.Learn(Recorder.GetRecord());
                }
            }
            else
            {
                return false;
            }
            return true;

        }


		/// <summary>
        /// 현재 게임의 보드를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static nBoard GetBoard()
        {
            return board;
        }

		/// <summary>
        /// 게임이 끝났는지 확인합니다.
        /// </summary>
        /// <returns></returns>
        public static bool IsGameEnd()
        {
            if (currentTurn == Players.NULL)
                return true;
            else
                return false;

        }

		/// <summary>
        /// 보드의 한 부분을 포지션으로 가져옵니다.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static nPiece GetPiece(nPoint position)
        {
            if (IsAvailablePosition(position) == false) return null;
            return board[position];
        }

		/// <summary>
        /// 현재 플레이어가 누군지 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static char GetCurrentTurn()
        {
            return currentTurn;
        }


		/// <summary>
        /// 게임이 끝났을 경우 승리자를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static char GetWinner()
        {
            if (IsGameEnd())
            {
                return winner;
            }
            else
                return Players.NULL;
        }

		/// <summary>
        /// 게임이 끝났을 경우 패배자를 가져옵니다.
        /// </summary>
        /// <returns></returns>
		public static char GetLoser()
        {
            if (IsGameEnd())
            {
                return loser;
            }
            else
                return Players.NULL;
        }

        /// <summary>
        /// 전체 기보를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static nRecord GetRecord()
        {
            return Recorder.GetRecord();
        }


        //public static GetSameLine(nBoard board)
        //{


        //}
    }


}
