using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapDemo : MonoBehaviour {

    public Animator spikeTrapAnim;
    public int dano = 20;
    public float impulso;
    public GameObject player;
    private bool podeCausarDano = false;

    void Awake()
    {
        spikeTrapAnim = GetComponent<Animator>();
        StartCoroutine(OpenCloseTrap());
    }

    IEnumerator OpenCloseTrap()
    {
        spikeTrapAnim.SetTrigger("open");
        podeCausarDano = true; // armadilha armada

        yield return new WaitForSeconds(2);

        spikeTrapAnim.SetTrigger("close");
        podeCausarDano = false; // desarmada

        yield return new WaitForSeconds(3);

        StartCoroutine(OpenCloseTrap());
    }

    // Essa função será chamada via Animation Event no momento certo da animação
    public void CausarDano()
    {
        if (!podeCausarDano) return;
        Debug.Log("Pôde causar dano!");
        Collider[] atingidos = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), Quaternion.identity);

        foreach (Collider col in atingidos)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log("Player encontrado!");
                Movimento_Player mp = col.GetComponent<Movimento_Player>();
                if (mp != null)
                {
                    Debug.Log("Tomou dano!");
                    mp.forcaY = 14;

                    mp.TomarDano(dano);
                }
            }
        }
    }
        
}