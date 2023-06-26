using System;



class Program
{
    static void Main()
    {
        FillBoard();
        var result = CheckResult(board);
        DisplayGameResult(result);
    }

    public static char[,] board = new char[3, 3] {
        { '-', '-', '-' },
        { '-', '-', '-' },
        { '-', '-', '-' }
    };

    public static void FillBoard()
    {
        board[0, 0] = 'X';
        board[1, 1] = 'O';
        board[2, 2] = 'X';
        board[0, 1] = 'X';
        board[0, 2] = 'X';
    }

    public static void DisplayGameResult(char winner)
    {
        Console.WriteLine("Wynik gry: ");
        Console.WriteLine(winner == ' ' ? "Remis" : $"Gracz {winner} wygrał");
    }

    public static char CheckResult(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != '-' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }
        }

        for (int j = 0; j < 3; j++)
        {
            if (board[0, j] != '-' && board[0, j] == board[1, j] && board[1, j] == board[2, j])
            {
                return board[0, j];
            }
        }

        if (board[0, 0] != '-' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return board[0, 0];
        }
        if (board[0, 2] != '-' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[0, 2];
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == '-')
                {
                    return '-';
                }
            }
        }

        return ' ';
    }
}