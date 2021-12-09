using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPc : MonoBehaviour
{
    private float rotY = 0;
    public float velRot = 10;

    Quaternion rotOriginal;

    private Rigidbody rbd;
    public float velocidade = 10;
    private int municao = 0;
    private int pontos = 280;

    public AudioSource[] som = new AudioSource[2];
    public LayerMask mascara;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Point")
        {
            other.gameObject.SetActive(false);
            pontos++;
            ScriptControladorPlacar.adicionaPontos(1);
            som[1].Play();
        } else if (other.tag == "PowerUp")
        {
            som[1].Play();
            other.gameObject.SetActive(false);
            pontos++;
            ScriptControladorPlacar.adicionaPontos(1);
            municao += 4;
            ScriptControladorPlacar.atualizaMunicao(municao);
        } else if (other.tag == "Enemy"){
            som[2].Play();
            Invoke("carregaGameOver", 0.7f);
        }
    }

    private void carregaGameOver()
    {
        ScriptControladorCena.carregaGameOver();
        gameObject.SetActive(false);
    }

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        rotOriginal = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        movimentar();

        rotacionarComMouse();
        atirar();
        if(pontos == 318) {
            recomecar();
        }
    }

    private void movimentar()
    {
        float frente = Input.GetAxis("Vertical");
        float lado = Input.GetAxis("Horizontal");

        rbd.velocity = transform.TransformDirection(new Vector3(lado * velocidade,
                                                                rbd.velocity.y,
                                                           frente * velocidade));
    }

    private void rotacionarComMouse()
    {
        rotY += Input.GetAxisRaw("Mouse X") * velRot * Time.deltaTime;

        Quaternion rotLado = Quaternion.AngleAxis(rotY, Vector3.up);

        transform.localRotation = rotOriginal * rotLado;

    }

    private void atirar()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && municao > 0)
        {

            ScriptControladorPlacar.atualizaMunicao(--municao);
            som[0].Play();
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position,
                                Camera.main.transform.forward,
                                out hit,
                                100,
                                mascara))
            {
                hit.collider.transform.position = new Vector3(0, hit.transform.position.y, 0);
                hit.collider.gameObject.GetComponent<ScriptNPC>().liberaMovimentarApos5Segundos();
            }
        }
    }

    private void recomecar() {
        municao = 0;
        ScriptControladorPlacar.atualizaMunicao(municao);
        ScriptControladorPlacar.reiniciar();
        ScriptControladorCena.recarregar();
    }
}
