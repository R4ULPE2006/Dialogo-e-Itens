
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class RotacaoCamera : MonoBehaviour
{

    [SerializeField] Transform corpoJogador;
    [SerializeField] float sensiMouse = 100f;
    [SerializeField] float delay = 5f;
    [SerializeField] float yOffSet, yOffSetAgachado;
    [SerializeField] float limite;
    //MovimentaJogador csMovimentaJogador;
    float alturaCamera;
    bool estaAgachado = false;
    private float rotacaoX;

    void Start()
    {
        //csMovimentaJogador = FindFirstObjectByType<MovimentaJogador>();
   
    }

    void Update()
    {
        RotacionarJogador();
        VerificarAgachamento();
    }

    private void VerificarAgachamento()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            estaAgachado = !estaAgachado;
        }
    }


    private void RotacionarJogador()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensiMouse * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensiMouse * Time.deltaTime;

        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -limite, limite);

        transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);
        corpoJogador.Rotate(Vector3.up * mouseX);
    }

    private void LateUpdate()
    {
        alturaCamera = estaAgachado ? yOffSetAgachado : yOffSet;
        Vector3 posDesejada = corpoJogador.position + Vector3.up * alturaCamera;
        transform.position = Vector3.Lerp(transform.position, posDesejada, delay * Time.deltaTime);
        
    }
}
