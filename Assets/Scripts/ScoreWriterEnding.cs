using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreWriterEnding : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    #endregion


    #region Unity Lyfecycle

    private void Awake()
    {
        _gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    void Start()
    {
        _scoreText.text = _gameControl.Score.ToString();
    }

    void Update()
    {
        
    }

    #endregion

    #region Methods

    public void OnClickMenuBouton()
    {
        _gameControl.ChargeMenuScene();
    }

    #endregion

    #region Private & Protected

    private GameControl _gameControl;

    #endregion
}
