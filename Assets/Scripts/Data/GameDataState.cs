using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameDataState
{
  [SerializeField] private int totalRoundTimeInSeconds;
  [SerializeField] private int highScore;
  [SerializeField] private GameTitleData gameTitleData;
  [SerializeField] private PumpkinGameData[] pumpkinGameData;
  public GameTitleData GameTitleData { get => gameTitleData; }
  public int HighScore { get => highScore; }
  public PumpkinGameData[] PumpkinGameData { get => pumpkinGameData; }
  public int TotalRoundTimeInSeconds { get => totalRoundTimeInSeconds; }

  public void SetHighScore(int newHighScore)
  {
    highScore = newHighScore;
  }

  public GameDataState Default()
  {
    totalRoundTimeInSeconds = 15;
    highScore = 0;
    gameTitleData = new GameTitleData();
    gameTitleData.SetDefault();
    pumpkinGameData = new PumpkinGameData[] { new PumpkinGameData("purple"), new PumpkinGameData("purple"), new PumpkinGameData("purple") };
    return this;
  }
}

[Serializable]
public class PumpkinGameData
{
  public PumpkinGameData(string data)
  {
    pumpkinColor = data;
  }
  [SerializeField] private string pumpkinColor;
  public string PumpkinColor { get => pumpkinColor; }
}

[Serializable]
public class GameTitleData
{
  public void SetDefault()
  {
    titleName = "Default Title Name";
    version = ".9";
  }
  [SerializeField] private string titleName;
  [SerializeField] private string version;

  public string TitleName { get => titleName; }
  public string Version { get => version; }
}
