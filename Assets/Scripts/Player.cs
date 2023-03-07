using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public Camera Camera;
    public float Velocidad, sensibilidadH, sensibilidadV;
    private float rotateHorizontal, rotateVertical;
    public int VidaMaxima = 100, VidaMinima = 0;
    public int _vida;
    public int vida
    {
        get { return _vida; }
        set
        {
            _vida = Mathf.Clamp(value, VidaMinima, VidaMaxima);
            if (_vida <= 0)
            {
                Debug.Log("Has muerto");
            }
        }
    }
    public bool recibeDmg;
    public GameObject target, ejeTarget;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        vida = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CameraMove();

        if (recibeDmg)
        {
            vida = vida - 10;
            recibeDmg = false;
        }
    }

    public void Move() 
    {
        transform.position += transform.forward * Input.GetAxis("Vertical")
        * Velocidad * Time.deltaTime;
        transform.position += transform.right * Input.GetAxis("Horizontal")
        * Velocidad * Time.deltaTime;

        Vector3 cameraForward = Camera.transform.forward;
        cameraForward.y = 0f;
        transform.forward = Vector3.Slerp(transform.forward, cameraForward,
        0.05f);
    }

    public void CameraMove()
    {
        Camera.transform.position =
        Vector3.Lerp(Camera.transform.position, target.transform.position,
        Mathf.SmoothStep(0.0f, 1.0f, t));
        rotateVertical -= Input.GetAxis("Mouse Y") * sensibilidadV;
        rotateHorizontal += Input.GetAxis("Mouse X") *
        sensibilidadH;
        ejeTarget.transform.eulerAngles = new Vector3(0f,
        rotateHorizontal, 0.0f);
        Camera.transform.LookAt(transform.position);
        
    }
}
