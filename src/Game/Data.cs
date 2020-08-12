using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TicTacToe
{
    public partial class Game : Share
    {

        private static nBoard board = new nBoard();
        private static char currentTurn = Players.NULL;
        private static char winner = Players.NULL;
        private static char loser = Players.NULL;



        private static bool IsCanNext(nPoint position)
        {
            if (board[position.x, position.y].player != Players.NULL) return false;

            if (currentTurn == Players.NULL) return false;

            return true;
        }


        private static void ChangeTurn()
        {
            if (currentTurn == Players.O)
            {
                currentTurn = Players.X;
            }
            else if (currentTurn == Players.X)
            {
                currentTurn = Players.O;
            }
        }

        private static bool SetWL()
        {
            var winnerLine = Tool.GetSameLine(board);
            if (winnerLine != null)
            {
                winner = winnerLine[0].player;

                if (winner == Players.O)
                {
                    loser = Players.X;
                }
                else if (winner == Players.X)
                {
                    loser = Players.O;
                }
                return true;
            }
            else
            {
                winner = Players.NULL;
                loser = Players.NULL;
            }
            return false;
        }





        private static bool IsAvailablePosition(nPoint position)
        {
            if (position == null) return false;
            if (0 <= position.x && position.x < 3)
            {
                if (0 <= position.y && position.y < 3)
                {
                    return true;
                }
            }
            return false;
        }




        // 라인들 전부 검사 -> 라인이 전부 똑같은 값 일때 그 것 반환

    }
}
