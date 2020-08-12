using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TicTacToe
{
    public partial class Game : Share
    {
        private partial class Recorder
        {
            private static nRecord record = new nRecord();

            public static void NewRecord()
            {
                record = new nRecord();
            }

            public static nRecord GetRecord()
            {
                return record;
            }

            public static void RecordNext(nBoard board)
            {
                record.AddRecord(board);
            }

            public static void RecordEndGame(char winner, char loser)
            {
                record.winner = winner;
                record.loser = loser;
                    record.IsGameEnd = true;
            }


        }
    }
}
