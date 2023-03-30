using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    
    #region Unity Lyfecycle

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            _gameManager.GameOver();
        }
    }

    #endregion


    #region Private & Protected

    private GameManager _gameManager;

  

    #endregion
}
