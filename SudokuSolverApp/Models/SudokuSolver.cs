using System;

namespace SudokuSolverApp.Models
{
    public class SudokuSolver
    {
        // Public method to solve the Sudoku puzzle
        public bool SolveSudoku(int[,] board)
        {
            if (!IsValidSudoku(board))
            {
                return false;
            }

            return Solve(board);
        }

        // Private method to solve the puzzle using backtracking
        private bool Solve(int[,] board)
        {
            int size = board.GetLength(0);

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // If the cell is empty, try filling it with a valid number
                    if (board[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsValidPlacement(board, row, col, num))
                            {
                                board[row, col] = num;

                                if (Solve(board))
                                {
                                    return true;
                                }

                                // Reset the cell if the number doesn't lead to a solution
                                board[row, col] = 0;
                            }
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        // Method to check if placing a number in a specific cell is valid
        private bool IsValidPlacement(int[,] board, int row, int col, int num)
        {
            return IsRowValid(board, row, num) &&
                   IsColumnValid(board, col, num) &&
                   IsSubgridValid(board, row, col, num);
        }

        // Method to check if the number is not repeated in the row
        private bool IsRowValid(int[,] board, int row, int num)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == num)
                {
                    return false;
                }
            }
            return true;
        }

        // Method to check if the number is not repeated in the column
        private bool IsColumnValid(int[,] board, int col, int num)
        {
            for (int row = 0; row < 9; row++)
            {
                if (board[row, col] == num)
                {
                    return false;
                }
            }
            return true;
        }

        // Method to check if the number is not repeated in the 3x3 subgrid
        private bool IsSubgridValid(int[,] board, int row, int col, int num)
        {
            int startRow = row / 3 * 3;
            int startCol = col / 3 * 3;

            for (int r = startRow; r < startRow + 3; r++)
            {
                for (int c = startCol; c < startCol + 3; c++)
                {
                    if (board[r, c] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Method to validate the initial state of the Sudoku puzzle
        private bool IsValidSudoku(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] != 0)
                    {
                        int num = board[row, col];
                        board[row, col] = 0; // Temporarily empty the cell to reuse the placement check

                        if (!IsValidPlacement(board, row, col, num))
                        {
                            return false;
                        }

                        board[row, col] = num; // Restore the original number
                    }
                }
            }

            return true;
        }
    }
}
