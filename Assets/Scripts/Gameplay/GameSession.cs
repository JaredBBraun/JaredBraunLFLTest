using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
  private const int SCORE_PER_PUMPKIN = 5;
  private int _currentGameSessionScore = 0;
  public Action OnSessionStart;
  public Action OnSessionEnd;
  [SerializeField] private EnemySpawner[] _enemySpawners;
  public Action<int> OnScoreIncreased;
  private Dictionary<string, Enemy> _pumpkinNameData;
  public float timeLeft = 0;

  public enum SessionState
  {
    Paused,
    Active,
    Finished
  }
  void Awake()
  {
    //TODO: make dedicated object for loaded art asste based on data
    Enemy[] enemyPrefabsToLoad;
    enemyPrefabsToLoad = Resources.LoadAll<Enemy>("Pumpkins");
    _pumpkinNameData = new Dictionary<string, Enemy>();
    foreach (var item in enemyPrefabsToLoad)
    {
      _pumpkinNameData.Add(item.name.Split('_')[1].ToLower(), item);
    }
  }

  private SessionState _state = SessionState.Paused;

  // Start is called before the first frame update
  void Start()
  {
    StartSession(true);
    foreach (EnemySpawner enemySpawner in _enemySpawners)
    {
      enemySpawner.AddOnRepsawnListener(() =>
      {
        OnScoreIncreased?.Invoke(_currentGameSessionScore += SCORE_PER_PUMPKIN);
      });
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (_state == SessionState.Active)
    {
      timeLeft -= Time.deltaTime;

      if (timeLeft <= 0)
      {
        timeLeft = 0;
        EndSession();
      }
    }
  }
  public void RestartGame()
  {
    _currentGameSessionScore = 0;
    StartSession(false);
  }

  public void GoBackToMainMenu()
  {
    SceneManager.LoadScene(0);
  }

  void StartSession(bool setPumpkins)
  {
    timeLeft = GameDataSession.Instance.GameDataLoader.gameDataState.TotalRoundTimeInSeconds;
    _state = SessionState.Active;
    if (setPumpkins)
    {
      SetPumpkinTypes(GameDataSession.Instance.GameDataLoader.gameDataState.PumpkinGameData);
    }
    if (OnSessionStart != null)
    {
      OnSessionStart();
    }
  }

  void SetPumpkinTypes(PumpkinGameData[] pumpkinGameData)
  {
    if (pumpkinGameData.Length != _enemySpawners.Length)
    {
      Debug.LogError("incomming pumpkin game data can't be loaded into pumpkin layout: array lengths don't match");
      return;
    }
    for (int i = 0; i < pumpkinGameData.Length; i++)
    {
      _enemySpawners[i].SetEnemyPrefab(GetEnemyPrefabFromColor(pumpkinGameData[i].PumpkinColor));
    }
  }

  Enemy GetEnemyPrefabFromColor(string pumpkinColor)
  {
    //TODO: this function should live in a data to enemy type loader class
    if (!_pumpkinNameData.Keys.ToArray().Contains(pumpkinColor))
    {
      Debug.LogError("Can not get pumpkin type from pumpkin: " + pumpkinColor);
      return null;
    }
    else
    {
      return _pumpkinNameData[pumpkinColor];
    }
  }

  void EndSession()
  {
    if (OnSessionEnd != null)
    {
      OnSessionEnd();
      SetFinalScore();
      GameDataSession.Instance.Save();
    }
  }

  void SetFinalScore()
  {
    GameDataSession.Instance.SetFinalHighScore(_currentGameSessionScore);
  }
}
