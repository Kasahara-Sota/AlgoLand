using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player") && other.gameObject != transform.parent.gameObject)
        {
            SoundManager.Instance.PlaySE("Conflict");
            Destroy(transform.parent.gameObject);
        }
    }
}
