using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerControler : MonoBehaviour
{
    [Header("Données pour la destruction")]
    [SerializeField]
    private GameObject _burstPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_burstPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
