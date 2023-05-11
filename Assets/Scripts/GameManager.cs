using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    Canvas gameScreen;
    [SerializeField] 
    Canvas startScreen;
    [SerializeField] 
    Canvas gameOverScreen;
    [SerializeField]
    private Image HealthBarRed;
    [SerializeField]
    private Image HealthBarGreen;
    [SerializeField]
    private ShipControl _shipControl;
    [SerializeField]
    private GameObject[] _modulaEnergieBars;

    #endregion

    #region Private, Protected & Accessor

    public int Score;
    public int BestScore;
    private Text ScoreTxt;
    private Text ScoreGameOver;
    public bool isInGame = false;
    private bool onGameOverScreen = true;
    private float _health;
    public float Health { get { return _health; } private set { }} 
    public float LostHealth {  get { return _health; }  set { _health = value; } }
    public float WinHealth { get { return _health; } set { _health = value; } }
    private GameControl _gameControl;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        ScoreTxt = gameScreen.GetComponent<Text>();
        ScoreGameOver =gameOverScreen.GetComponent<Text>();
        for (int i = 0; i < _modulaEnergieBars.Length; i++)
        {
            //_modulaEnergieBars[i].SetActive(false);
            _modulaEnergieBars[i].GetComponent<Image>().enabled = false;
        }
        //_gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    void Start()
    {
        Debug.Log("START!!");
        _health = 100;
        gameScreen.enabled = true;
        gameOverScreen.enabled = false;
        isInGame = true;
        onGameOverScreen = false;
        BroadcastMessage("OnStartGame");
        gameScreen.enabled = true;
        Score = 0;
    }

    
    void Update()
    {
        Debug.Log("My Health = " + _health);
        if (Input.GetButtonDown("Jump") && !isInGame && onGameOverScreen)
        {
            StartGame();
            
        }

        if (_health <= 0 && isInGame)
        {
            GameOver();
           
            if (Input.GetButtonDown("Jump"))
            {
                StartGame();
            }
        }

        //ADAPT THE SIZE OF THE HEALTH BAR
        float coef = Health / 100.0f;
        HealthBarGreen.rectTransform.sizeDelta = new Vector2(HealthBarRed.rectTransform.sizeDelta.x * coef, HealthBarRed.rectTransform.sizeDelta.y);

        ScoreTxt.text = "Score : " + Score;
        ScoreGameOver.text = "SCORE : " + Score + "\n" + "BEST SCORE : " + BestScore + "\n" + "\n" + "RETRY PRESS SPACE";

        //Display the Modula Energy bars 
        int numbBarsToDisplay = _shipControl.ModulaEnergieCount;
        for (int i = 0; i < _modulaEnergieBars.Length; i++)
        {
            if (i < numbBarsToDisplay)
            {
                //_modulaEnergieBars[i].SetActive(true);
                _modulaEnergieBars[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                //_modulaEnergieBars[i].SetActive(false);
                _modulaEnergieBars[i].GetComponent<Image>().enabled = false;
            }
        }

    }

    #endregion

    #region Methods

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }

    public void GameOver()
    {
        isInGame = false;
        BroadcastMessage("OnGameOver");
        Invoke("TranslateScreen", 2f);
        if (Score > BestScore)
        {
            BestScore = Score;
        }
        
    }

    private void TranslateScreen()
    {
        gameScreen.enabled = false;
        gameOverScreen.enabled = true;
        onGameOverScreen = true;
    }

    public void WinLevel()
    {
        isInGame = false;
        Debug.Log("FINISH LINE !!!");
        Invoke("ChangeScene", 2);
    }

    private void ChangeScene()
    {
        _gameControl.ChangeScene();
    }

    #endregion
}
