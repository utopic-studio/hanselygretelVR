using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpHanselGretelCisne : MonoBehaviour
{
    public GameObject hansel;
    public GameObject gretel;
    public GameObject Tp1;
    public GameObject Tp2;
    public GameObject cisne;
    public GameObject movedor;
    private bool meMuevo = false;

    public void Teleportation() {
        hansel.transform.position = Tp1.transform.position;
        hansel.transform.rotation = cisne.transform.rotation;
        gretel.transform.position = Tp2.transform.position;
        gretel.transform.rotation = cisne.transform.rotation;
        StartCoroutine("DelayMovimiento");

    }

    IEnumerator DelayMovimiento() {
        yield return new WaitForSeconds(2f);
        movedor.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        meMuevo = true;

    }
    private void Update()
    {
        if (meMuevo == true)
        {
            movedor.transform.Translate(Vector3.back * Time.deltaTime);

        }
    }
}

