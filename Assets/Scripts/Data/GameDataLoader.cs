using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameDataLoader
{
  private string dataPath = Path.Combine(Application.persistentDataPath, "PumpkinGameData.json");
  public GameDataState gameDataState { get; private set; }

  public GameDataLoader()
  {
    gameDataState = GetGameState();
  }

  public void Save()
  {
    using (StreamWriter stream = new StreamWriter(dataPath))
    {
      stream.WriteLine(JsonUtility.ToJson(gameDataState));

    }
  }

  GameDataState GetGameState()
  {

    if (!File.Exists(dataPath))
    {
      using (FileStream file = File.Create(dataPath))
      {
        file.Dispose();
      }
      gameDataState = new GameDataState().Default();
      Save();
      return gameDataState;

    }
    using (StreamReader stream = new StreamReader(dataPath))
    {
      string rawJson = stream.ReadToEnd();
      return JsonUtility.FromJson<GameDataState>(rawJson);

    }
  }
}
