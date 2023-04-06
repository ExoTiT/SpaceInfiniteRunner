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
        yield return new WaitForSeconds(3);
         StartMenuMethod();
    }

    void StartMenuMethod()
    {
        Debug.Log("Start Menu!!");
        SceneManager.LoadScene(1);
    }

    #endregion

    #region Private & Protected

    #endregion
}
