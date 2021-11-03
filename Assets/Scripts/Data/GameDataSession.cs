using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSession : MonoBehaviour
{
  private GameDataLoader _gameDataLoader;
  public GameDataLoader GameDataLoader { get => _gameDataLoader; }
  public static GameDataSession Instance { get => instance; }
  private static GameDataSession instance = null;

  void Awake()
  {
    if (instance == null)
    {
      _gameDataLoader = new GameDataLoader();
      DontDestroyOnLoad(gameObject);
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void SetFinalHighScore(int currentGameSessionScore)
  {
    if (currentGameSessionScore > _gameDataLoader.gameDataState.HighScore)
    {
      _gameDataLoader.gameDataState.SetHighScore(currentGameSessionScore);
    }
  }

  public void Save()
  {
    _gameDataLoader.Save();
  }
}
