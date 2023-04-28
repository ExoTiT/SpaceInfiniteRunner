using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    #region Exposed



    #endregion


    #region Unity Lyfecycle

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(StartMenu());
    }

    void Update()
    {
        
    }

    #endregion

    #region Methods

    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(6);
         StartMenuMethod();
    }

    void StartMenuMethod()
    {
        Debug.Log("Start Menu!!");
        SceneManager.LoadScene(1);
    }

    public void WinState()
    {



    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChargeMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    #endregion

    #region Private & Protected

    private int _score = 0;
    public int Score { get { return _score; } private set { } }

    #endregion
}
