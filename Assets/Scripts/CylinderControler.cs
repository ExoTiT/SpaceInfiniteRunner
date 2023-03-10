using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderControler : MonoBehaviour
{
    #region Exposed
    [SerializeField]
    private GameObject _explodePrefab;




    #endregion


    #region Unity Lyfecycle

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_explodePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    #endregion
}
