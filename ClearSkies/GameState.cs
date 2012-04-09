using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearSkies
{
    /// <summary>
    /// Represents the curret state of the game world
    /// </summary>
    enum GameState
    {
        Start, // Initial state of game, use update to check for start signal
        Pause, // Pause state of game, use update to check for unpause signal
        Play, // Play state of game, use update to move game objects and run scripts
        Win, // Win state of game, use update to listen for restart signal
        Lose, // Lose state of game, use update to listen for restart signal
        Quit // Quit state of game, exit game loop
    }
}
