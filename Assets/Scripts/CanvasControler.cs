using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasControler : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private TextMeshProUGUI _textColor;

    [SerializeField]
    private float _scaleValue;

    #endregion


    #region Unity Lyfecycle

    void Start()
    {
        _canStartAnim = false;
        Debug.Log("COLOR = " + _textColor.color.a);
        _textColor.rectTransform.localScale = new Vector3(0, 0, 0);
        Invoke("StartAnim", 2);
    }

    void Update()
    {
        if (_canStartAnim)
        {

            if (_scaleText <=1)
            {
                _scaleText += _scaleValue;
            }


            _textColor.rectTransform.localScale = new Vector3(_scaleText, _scaleText, _scaleText);
        }

    }

    #endregion

    #region Methods

    private void StartAnim()
    {
        _canStartAnim = true;
    }

    #endregion

    #region Private & Protected

    private float _scaleText = 0;

    private bool _canStartAnim = false;

    #endregion
}
