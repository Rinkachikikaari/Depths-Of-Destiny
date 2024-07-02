using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour
{
    public enum miDir { U,D,L,R}
    public miDir mine;
    public static List<string> used;
    bool Entro = false;
    [SerializeField] private Spawnpoint sp;
    [SerializeField] private Transform salida;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform locatePlayer;
    [SerializeField] private GameObject Camara;
    [SerializeField] private Transform CamaraTp;

    private void Start()
    {
        used = new List<string>();
        GetComponent<Spawnpoint>();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Camara = GameObject.FindGameObjectWithTag("MainCamera");
        locatePlayer = player.GetComponent<Transform>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") )
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                player.transform.position = salida.transform.position;
                Camara.transform.position = CamaraTp.transform.position;
                print(this.transform.parent.parent.GetComponent<RoomControl>().name);
                if (!Entro) // && !used.Contains(this.transform.parent.parent.GetComponent<RoomControl>().name)
                {
                    sp.RandomSpawn();
                    Entro = true;
                    used.Add(this.transform.parent.parent.name);
                }
            }
        }
    }
}