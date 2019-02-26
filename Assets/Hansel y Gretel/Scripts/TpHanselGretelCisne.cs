using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpHanselGretelCisne : MonoBehaviour
{
    public float delayStopMovmiento = 9;
    public float delayMovmiento = 2;

    public bool meMuevo = false;
    //PRIMERA FASE
    [Header("PRIMERA FASE")]
    public GameObject hansel;
    public GameObject gretel;
    public GameObject hanselPos1;
    public GameObject gretelPos1;
    public GameObject cisnePos1;
    public GameObject movedor;
    

    //SEGUNDA FASE
    [Header("SEGUNDA FASE")]
    public GameObject cam;
    public GameObject camPos;
    public GameObject hanselPos2;
    public GameObject gretelPos2;
    public GameObject father;
    public GameObject cisnePos2;

    public void Teleportation() {
        //hansel.transform.position = hanselPos1.transform.position;
        //hansel.transform.rotation = cisnePos1.transform.rotation;
        //gretel.transform.position = gretelPos1.transform.position;
        //gretel.transform.rotation = cisnePos1.transform.rotation;
     //   StartCoroutine("DelayMovimiento");
     //   StartCoroutine("StopMovimiento");
    }

    IEnumerator DelayMovimiento() {
        yield return new WaitForSeconds(delayMovmiento);
       // movedor.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        meMuevo = true;

    }

    IEnumerator StopMovimiento()
    {
     yield return new WaitForSeconds(delayStopMovmiento);     
        meMuevo = false;
        //FADE IN FADE OUT
        SegundaParte();


    }
    private void Update()
    {
        if (meMuevo == true)
        {
            movedor.transform.Translate(Vector3.back * Time.deltaTime);

        }
    }

    void SegundaParte() {
        cam.transform.position = camPos.transform.position;
        cam.transform.rotation = camPos.transform.rotation;
        movedor.transform.rotation *= Quaternion.Euler(0, -180f, 0);
        hansel.transform.position = hanselPos2.transform.position;
        hansel.transform.rotation = hanselPos2.transform.rotation;
        gretel.transform.position = gretelPos2.transform.position;
        gretel.transform.rotation = gretelPos2.transform.rotation;
        father.gameObject.SetActive(true);


    }
}

