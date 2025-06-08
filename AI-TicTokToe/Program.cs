namespace AI_TicTokToe.App
{

    public class Program
    {
        // تابع برای چاپ تخته بازی
        static void DisplayBoard(string[,] gameBoard)
        {
            int size = gameBoard.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                // ایجاد یک آرایه موقت برای سطر فعلی
                string[] row = new string[size];
                for (int j = 0; j < size; j++)
                {
                    row[j] = gameBoard[i, j];
                }
                Console.WriteLine(string.Join(" | ", row));
                Console.WriteLine(new string('-', size * 4 - 2));
            }
        }

        // تابع برای بررسی برنده شدن بازیکن
        static bool IsWinner(string[,] gameBoard, string playerSymbol)
        {
            int size = gameBoard.GetLength(0);

            // بررسی برنده شدن در سطرها و ستون‌ها
            for (int i = 0; i < size; i++)
            {
                if (gameBoard[i, 0] == playerSymbol && Enumerable.Range(1, size - 1).All(j => gameBoard[i, j] == playerSymbol))
                    return true;
                if (gameBoard[0, i] == playerSymbol && Enumerable.Range(1, size - 1).All(j => gameBoard[j, i] == playerSymbol))
                    return true;
            }

            // بررسی قطر اصلی
            if (Enumerable.Range(0, size).All(i => gameBoard[i, i] == playerSymbol))
                return true;

            // بررسی قطر فرعی
            if (Enumerable.Range(0, size).All(i => gameBoard[i, size - 1 - i] == playerSymbol))
                return true;

            return false;
        }

        // تابع برای بررسی اتمام بازی بدون برنده
        static bool IsDraw(string[,] gameBoard)
        {
            return gameBoard.Cast<string>().All(cell => cell != " ");
        }

        // الگوریتم آلفا-بتا برای تصمیم‌گیری بهینه
        static (int score, (int, int)? move) AlphaBeta(string[,] gameBoard, int depth, int alpha, int beta, bool isMaximizing, string player, string opponent, int depthLimit)
        {
            if (IsWinner(gameBoard, player))
                return (10 - depth, null);
            if (IsWinner(gameBoard, opponent))
                return (depth - 10, null);
            if (IsDraw(gameBoard))
                return (0, null);
            if (depth >= depthLimit)
                return (0, null);

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                (int, int)? bestMove = null;

                for (int i = 0; i < gameBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < gameBoard.GetLength(1); j++)
                    {
                        if (gameBoard[i, j] == " ")
                        {
                            gameBoard[i, j] = player;
                            var (score, _) = AlphaBeta(gameBoard, depth + 1, alpha, beta, false, player, opponent, depthLimit);
                            gameBoard[i, j] = " ";
                            if (score > bestScore)
                            {
                                bestScore = score;
                                bestMove = (i, j);
                            }
                            alpha = Math.Max(alpha, bestScore);
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return (bestScore, bestMove);
            }
            else
            {
                int bestScore = int.MaxValue;
                (int, int)? bestMove = null;

                for (int i = 0; i < gameBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < gameBoard.GetLength(1); j++)
                    {
                        if (gameBoard[i, j] == " ")
                        {
                            gameBoard[i, j] = opponent;
                            var (score, _) = AlphaBeta(gameBoard, depth + 1, alpha, beta, true, player, opponent, depthLimit);
                            gameBoard[i, j] = " ";
                            if (score < bestScore)
                            {
                                bestScore = score;
                                bestMove = (i, j);
                            }
                            beta = Math.Min(beta, bestScore);
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return (bestScore, bestMove);
            }
        }

        // تابع برای یافتن بهترین حرکت
        static (int, int)? GetBestMove(string[,] gameBoard, string player, string opponent, int depthLimit = 4)
        {
            var (_, bestMove) = AlphaBeta(gameBoard, 0, int.MinValue, int.MaxValue, true, player, opponent, depthLimit);
            return bestMove;
        }

        // تبدیل شماره ورودی به موقعیت در تخته
        static (int, int) ConvertNumberToPosition(int number, int size)
        {
            int row = (number - 1) / size;
            int col = (number - 1) % size;
            return (row, col);
        }

        // تابع اصلی بازی
        static void PlayGame()
        {
            Console.WriteLine("Welcome to 4x4 Integgigent Tic-Tac-Toe!");
            int size = 3; // اندازه تخته ثابت 4x4 است
            string[,] gameBoard = new string[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    gameBoard[i, j] = " ";

            string player1 = "x";
            string player2 = "o";

            Console.WriteLine("Player x is human");

            string currentPlayer = player1;

            while (true)
            {
                DisplayBoard(gameBoard);

                (int, int)? move;

                if (currentPlayer == player2)
                {
                    Console.WriteLine($"AI ({player2}) is making a move...");
                    move = GetBestMove(gameBoard, player2, player1);
                }
                else
                {
                    Console.WriteLine($"{currentPlayer}'s turn:");
                    (int, int)? suggestedMove = GetBestMove(gameBoard, player1, player2);
                    if (suggestedMove.HasValue)
                    {
                        int suggestedNumber = suggestedMove.Value.Item1 * size + suggestedMove.Value.Item2 + 1;
                        Console.WriteLine($"Suggested move: {suggestedNumber}");
                    }

                    while (true)
                    {
                        try
                        {
                            Console.Write($"Enter a number (1-{size * size}): ");
                            int number = int.Parse(Console.ReadLine());
                            if (number < 1 || number > size * size)
                                throw new ArgumentOutOfRangeException("Invalid number! Out of range.");

                            move = ConvertNumberToPosition(number, size);
                            if (gameBoard[move.Value.Item1, move.Value.Item2] == " ")
                                break;

                            else
                                Console.WriteLine("Cell is already occupied. Try again.");

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error: {e.Message}");
                        }
                    }
                }

                gameBoard[move.Value.Item1, move.Value.Item2] = currentPlayer;

                if (IsWinner(gameBoard, currentPlayer))
                {
                    DisplayBoard(gameBoard);
                    Console.WriteLine($"Player {currentPlayer} wins!");
                    break;
                }

                if (IsDraw(gameBoard))
                {
                    DisplayBoard(gameBoard);
                    Console.WriteLine("It's a draw!");
                    break;
                }

                currentPlayer = currentPlayer == player1 ? player2 : player1;
            }
        }

        // اجرای بازی
        static void Main(string[] args)
        {
            PlayGame();
        }
    }
}
