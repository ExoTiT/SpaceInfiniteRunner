using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasControl : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private GameObject _titreText;

    [SerializeField]
    private float _textTranslationSpeed;

    [SerializeField]
    private Transform _stopPosition;

    [SerializeField]
    private GameObject _menuText;

    [SerializeField]
    private GameObject _playButton;

    [SerializeField]
    private GameObject _controlButton;

    [SerializeField]
    private GameObject _generalPanel;

    [SerializeField]
    private GameObject _controlPanel;
    #endregion


    #region Unity Lyfecycle

    private void Awake()
    {
        _gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    void Start()
    {
        //Disabled Object
        _menuText.SetActive(false);
        _playButton.SetActive(false);
        _controlButton.SetActive(false);
        _controlPanel.SetActive(false);

        //Active Object
        _generalPanel.SetActive(true);
    }

    void Update()
    {
        _titreText.transform.Translate(0f, _textTranslationSpeed * Time.deltaTime, 0f);
        if (_titreText.transform.position.y >= _stopPosition.position.y )
        {
            _textTranslationSpeed = 0f;
            _menuText.SetActive(true);
            _playButton.SetActive(true);
            _controlButton.SetActive(true);
        }
    }

    #endregion

    #region Methods

    public void OpenControle()
    {
        _generalPanel.SetActive(false);
        _controlPanel.SetActive(true);
    }

    public void CloseControle() 
    {
        _generalPanel.SetActive(true);
        _controlPanel.SetActive(false);
    }

    public void StartGame()
    {
        _gameControl.ChangeScene();
    }

    #endregion

    #region Private & Protected

    private GameControl _gameControl;

    #endregion
}
