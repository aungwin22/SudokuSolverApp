using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SudokuSolverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverApp.Pages
{
    public class SudokuModel : PageModel
    {
        [BindProperty]
        public int[][] InputBoard { get; set; } = new int[9][];

        public int[,] Board { get; set; } = new int[9, 9];
        public bool Solved { get; set; } = false;

        public void OnGet()
        {
            // Initialize the board with default values
            for (int i = 0; i < 9; i++)
            {
                InputBoard[i] = new int[9];
            }

            // Pre-fill some cells with valid random numbers
            InitializeBoardWithRandomNumbers();
        }

        public void OnPost()
        {
            // Convert the jagged array to a 2D array
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Board[i, j] = InputBoard[i][j];
                }
            }

            var initialErrors = IsValidSudoku(Board);
            if (initialErrors.Any())
            {
                Solved = false;
                foreach (var error in initialErrors)
                {
                    ModelState.AddModelError("", error);
                }
                return;
            }

            var solver = new SudokuSolver();
            if (solver.SolveSudoku(Board))
            {
                Solved = true;
            }
            else
            {
                Solved = false;
                ModelState.AddModelError("", "Could not solve the Sudoku puzzle.");
            }
        }

        private void InitializeBoardWithRandomNumbers()
        {
            var random = new Random();
            int cellsToFill = 20; // Number of cells to pre-fill

            while (cellsToFill > 0)
            {
                int row = random.Next(0, 9);
                int col = random.Next(0, 9);
                int num = random.Next(1, 10);

                if (InputBoard[row][col] == 0 && IsValidPlacement(Board, row, col, num, out _))
                {
                    InputBoard[row][col] = num;
                    Board[row, col] = num;
                    cellsToFill--;
                }
            }
        }

        private List<string> IsValidSudoku(int[,] board)
        {
            var errors = new List<string>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != 0)
                    {
                        int num = board[i, j];
                        board[i, j] = 0; // Temporarily empty the cell to reuse the placement check

                        if (!IsValidPlacement(board, i, j, num, out string error))
                        {
                            errors.Add(error);
                        }

                        board[i, j] = num; // Restore the original number
                    }
                }
            }

            return errors;
        }

        private bool IsValidPlacement(int[,] board, int row, int col, int num, out string error)
        {
            error = null;

            if (!IsRowValid(board, row, num, out string rowError))
            {
                error = $"Invalid placement at row {row + 1}, column {col + 1}. {rowError}";
                return false;
            }

            if (!IsColumnValid(board, col, num, out string colError))
            {
                error = $"Invalid placement at row {row + 1}, column {col + 1}. {colError}";
                return false;
            }

            if (!IsSubgridValid(board, row, col, num, out string subgridError))
            {
                error = $"Invalid placement at row {row + 1}, column {col + 1}. {subgridError}";
                return false;
            }

            return true;
        }

        private bool IsRowValid(int[,] board, int row, int num, out string error)
        {
            error = null;
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == num)
                {
                    error = $"Row {row + 1} has duplicate number {num} in column {col + 1}";
                    return false;
                }
            }
            return true;
        }

        private bool IsColumnValid(int[,] board, int col, int num, out string error)
        {
            error = null;
            for (int row = 0; row < 9; row++)
            {
                if (board[row, col] == num)
                {
                    error = $"Column {col + 1} has duplicate number {num} in row {row + 1}";
                    return false;
                }
            }
            return true;
        }

        private bool IsSubgridValid(int[,] board, int row, int col, int num, out string error)
        {
            error = null;
            int startRow = row / 3 * 3;
            int startCol = col / 3 * 3;

            for (int r = startRow; r < startRow + 3; r++)
            {
                for (int c = startCol; c < startCol + 3; c++)
                {
                    if (board[r, c] == num)
                    {
                        error = $"3x3 subgrid starting at row {startRow + 1}, column {startCol + 1} has duplicate number {num} at row {r + 1}, column {c + 1}";
                        return false;
                    }
                }
            }

            return true;
        }

        // Helper method to determine the CSS class for coloring regions
        public string GetRegionClass(int row, int col)
        {
            if (row < 3)
            {
                if (col < 3) return "region-1";
                if (col < 6) return "region-2";
                return "region-3";
            }
            if (row < 6)
            {
                if (col < 3) return "region-4";
                if (col < 6) return "region-5";
                return "region-6";
            }
            if (col < 3) return "region-7";
            if (col < 6) return "region-8";
            return "region-9";
        }
    }
}
