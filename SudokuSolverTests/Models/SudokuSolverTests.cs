using SudokuSolverApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverTests.Models
{
    public class SudokuSolverTests
    {
        private readonly SudokuSolver _solver = new SudokuSolver();

        [Fact]
        public void Test_Sudoku_Solver_With_Valid_Puzzle()
        {
            int[,] board = new int[,]
            {
                { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
            };

            bool result = _solver.SolveSudoku(board);

            Assert.True(result, "The Sudoku puzzle should be solvable.");
        }

        [Fact]
        public void Test_Sudoku_Solver_With_Empty_Puzzle()
        {
            int[,] board = new int[9, 9];

            bool result = _solver.SolveSudoku(board);

            Assert.True(result, "The empty Sudoku puzzle should be solvable.");
        }

        [Fact]
        public void Test_Sudoku_Solver_With_Completed_Puzzle()
        {
            int[,] board = new int[,]
            {
                { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
                { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                { 3, 4, 5, 2, 8, 6, 1, 7, 9 }
            };

            bool result = _solver.SolveSudoku(board);

            Assert.True(result, "The completed Sudoku puzzle should be recognized as solved.");
        }

        [Fact]
        public void Test_Sudoku_Solver_With_Invalid_Puzzle_Duplicate_In_Row()
        {
            int[,] board = new int[,]
            {
                { 5, 3, 4, 6, 7, 8, 9, 1, 2 },
                { 6, 7, 2, 1, 9, 5, 3, 4, 8 },
                { 1, 9, 8, 3, 4, 2, 5, 6, 7 },
                { 8, 5, 9, 7, 6, 1, 4, 2, 3 },
                { 4, 2, 6, 8, 5, 3, 7, 9, 1 },
                { 7, 1, 3, 9, 2, 4, 8, 5, 6 },
                { 9, 6, 1, 5, 3, 7, 2, 8, 4 },
                { 2, 8, 7, 4, 1, 9, 6, 3, 5 },
                { 3, 4, 5, 2, 8, 6, 1, 7, 3 } // Invalid: Duplicate 3 in the last row
            };

            bool result = _solver.SolveSudoku(board);

            Assert.False(result, "The Sudoku puzzle with a duplicate in the row should be invalid.");
        }

        // Additional test cases can be added similarly...
    }
}
