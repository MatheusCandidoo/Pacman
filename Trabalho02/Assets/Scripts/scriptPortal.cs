using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPortal : MonoBehaviour
{
    public int novaPosicaoDeX;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = new Vector3(novaPosicaoDeX,
            other.gameObject.transform.position.y, other.gameObject.transform.position.z);
    }
}
