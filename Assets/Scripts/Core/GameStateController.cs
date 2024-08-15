using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class GameStateController : MonoBehaviour
    {
        public GameState CurrentGameState { get; private set; }
        public Action<GameState> GameStateChanged;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(5f);
            SetGameState(GameState.LevelStarted);
        }

        public void SetGameState(GameState newGameState)
        {
            CurrentGameState = newGameState;
            GameStateChanged?.Invoke(newGameState);
        }
    }

    public enum GameState { BoardCreation, TileCreation, LevelStarted, LevelFailed, LevelCompleted, Pause }
}

