using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControler : MonoBehaviour
{
    #region Exposed
    [Header("Données pour l'explosion du cube")]
    [SerializeField]
    private GameObject _cubeExplode;

    [Header("Données pour le déplacement du cube")]
    [SerializeField]
    [Range(0.5f, 2f)]
    private float _speed;

    [SerializeField]
    private Transform[] _point;

    [SerializeField]
    [Range(0, 1)]
    private int _startPoint;

    [SerializeField]
    [Range(0.1f, 0.5f)]
    private float _distanceTolerance;

    #endregion


    #region Unity Lyfecycle

    void Start()
    {
        transform.position = _point[_targetPoint].position;
        _targetPoint = _startPoint + 1;
        if (_targetPoint > 1) _targetPoint = 0;
    }

    void Update()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, _point[_targetPoint].position, _speed * Time.deltaTime);

        transform.position = newPosition;

        if (Vector3.Distance(transform.position, _point[_targetPoint].position) < _distanceTolerance)
        {
            _targetPoint++;
            if (_targetPoint > 1) _targetPoint = 0;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_cubeExplode, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    private int _targetPoint;

    #endregion
}
