using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public partial class Tool : Share
    {
        public delegate bool _XYDelegate(int x, int y);
        public static void DoTicTacToeArray(_XYDelegate Actor)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (Actor(x, y) == false)
                        return;
                }
            }
            return;
        }
        public static nRecord MakeSaveRuleAnyRecord()
        {
            Game.NewGame(Players.O);
            while (true)
            {
                nBoard board = Game.GetBoard();
                nPoint putPos = null;

                // 여기에서 규칙 우선순위별로 배열한다.
                putPos = GetCliffPos(board, Game.GetCurrentTurn());
                if(putPos == null)
                {
                    putPos = GetRandomVoidPos(board);
                }

                Game.PutNext(putPos, true);
                if (Game.IsGameEnd())
                {
                    break;
                }
            }
            var record = Game.GetRecord();

            return record;
        }
        public static nPiece[] GetSameLine(nBoard board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (IsSameLine(board, i, 'X'))
                {
                    return GetLine(board, i, 'X');
                }
                else if (IsSameLine(board, i, 'Y'))
                {
                    return GetLine(board, i, 'Y');
                }
                else if (i < 2)
                {
                    if (IsSameLine(board, i, 'D'))
                        return GetLine(board, i, 'D');
                }
            }
            return null;
        }

        public static bool IsFullBoard(nBoard board)
        {
            foreach (var piece in board.pieces)
            {
                if (piece.player == Players.NULL)
                    return false;
            }
            return true;
        }
    }


}
