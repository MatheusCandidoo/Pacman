using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptNPC : MonoBehaviour
{
    public float distancia = 10;
    public GameObject PC;
    public GameObject[] Waypoints = new GameObject[12];
    private int index;
    private bool perseguicao = false;
    private NavMeshAgent agente;
    private bool esperar = true;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        liberaMovimentarApos5Segundos();
        proximo();
    }

    public void liberaMovimentarApos5Segundos()
    {
        restringeMovimentacao();
        Invoke("liberaMovimentar", 5f);

    }

    private void liberaMovimentar()
    {
        esperar = false;
    }

    private void restringeMovimentacao()
    {
        esperar = true;
    }

    private void proximo()
    {
        bool flag = true;

        int newIndex;
        while (flag)
        {
            newIndex = (int)Random.Range(0, Waypoints.Length);
            if (index != newIndex) {
                index = newIndex;
                flag = false;
            }
        }
        Seguir(Waypoints[index].transform.position);

    }

    private void Seguir(Vector3 position)
    {
        if(esperar)
        {
            agente.SetDestination(transform.position);
        } else
        {
            agente.SetDestination(position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, PC.transform.position) <= distancia)
        {

            Seguir(PC.transform.position);
            perseguicao = true;
        }
        else
        {
            perseguicao = false;
            Seguir(Waypoints[index].transform.position);
        }
        if (Vector3.Distance(gameObject.transform.position, agente.destination) <= 2 && !perseguicao)
            proximo();

    }
}
