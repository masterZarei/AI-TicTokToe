# AI-TicTokToe
# 4x4 Tic-Tac-Toe Game

Welcome to the 4x4 Tic-Tac-Toe game! This project implements a classic game of Tic-Tac-Toe with a twist: the game board is 4x4, and it includes an AI opponent that uses the Alpha-Beta pruning algorithm to make optimal moves. Below is a comprehensive explanation of the code and how to use it.

## Table of Contents
1. [Overview](#overview)
2. [Game Features](#game-features)
3. [Code Explanation](#code-explanation)
   - [DisplayBoard](#displayboard)
   - [IsWinner](#iswinner)
   - [IsDraw](#isdraw)
   - [AlphaBeta](#alphabeta)
   - [GetBestMove](#getbestmove)
   - [ConvertNumberToPosition](#convertnumbertoposition)
   - [PlayGame](#playgame)
   - [Main](#main)
4. [How to Run the Game](#how-to-run-the-game)
5. [Contributing](#contributing)
6. [License](#license)

## Overview
This project is a console-based implementation of a 4x4 Tic-Tac-Toe game where two players can compete against each other, or one player can play against an AI. The AI uses the Alpha-Beta pruning algorithm to determine the best possible move.

## Game Features
- **4x4 Game Board**: A larger board for a more challenging experience.
- **Human vs. AI**: Play against a computer opponent that makes strategic moves.
- **User Input**: Players can input their moves by selecting a cell number.
- **Win and Draw Detection**: The game checks for winning conditions and draws.

## Code Explanation

### DisplayBoard
```csharp
static void DisplayBoard(string[,] gameBoard)
```
This function prints the current state of the game board to the console. It formats the board with rows and columns, separating cells with vertical bars and rows with horizontal lines.

### IsWinner
```csharp
static bool IsWinner(string[,] gameBoard, string playerSymbol)
```
This function checks if a player has won the game. It checks all rows, columns, and both diagonals for a complete line of the player's symbol.

### IsDraw
```csharp
static bool IsDraw(string[,] gameBoard)
```
This function checks if the game has ended in a draw by verifying if all cells on the board are filled without any winner.

### AlphaBeta
```csharp
static (int score, (int, int)? move) AlphaBeta(string[,] gameBoard, int depth, int alpha, int beta, bool isMaximizing, string player, string opponent, int depthLimit)
```
This function implements the Alpha-Beta pruning algorithm to evaluate the best move for the AI. It recursively explores possible moves and their outcomes, returning the best score and move for the current player.

### GetBestMove
```csharp
static (int, int)? GetBestMove(string[,] gameBoard, string player, string opponent, int depthLimit = 4)
```
This function finds the best move for the current player by calling the AlphaBeta function. It returns the coordinates of the best move.

### ConvertNumberToPosition
```csharp
static (int, int) ConvertNumberToPosition(int number, int size)
```
This function converts a user-input number (1 to 16) into a 2D array index (row, column) for the game board.

### PlayGame
```csharp
static void PlayGame()
```
This is the main game loop where players take turns making moves. It handles user input, displays the board, checks for wins or draws, and switches between players.

### Main
```csharp
static void Main(string[] args)
```
The entry point of the program that starts the game by calling the `PlayGame` function.

## How to Run the Game
1. Clone this repository to your local machine.
2. Open the project in your preferred C# IDE (e.g., Visual Studio).
3. Build the project.
4. Run the program. Follow the on-screen instructions to play the game.

## Contributing
Contributions are welcome! If you have suggestions for improvements or new features, feel free to open an issue or submit a pull request.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

---

Feel free to modify this README file as needed to better fit your project or personal style!
