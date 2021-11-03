using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  private const string GAME_SCENE_NAME = "GameScene";
  [SerializeField] private Text _highScore = null;
  [SerializeField] private Text _titleText = null;
  [SerializeField] private Text _versionText = null;
  [SerializeField] private Button _startGameButton;

  void Start()
  {
    _highScore.text = GameDataSession.Instance.GameDataLoader.gameDataState.HighScore.ToString();// PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore").ToString() : 0.ToString();
    _titleText.text = GameDataSession.Instance.GameDataLoader.gameDataState.GameTitleData.TitleName;
    _versionText.text = GameDataSession.Instance.GameDataLoader.gameDataState.GameTitleData.Version;
    _startGameButton.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(GAME_SCENE_NAME));
  }
}
