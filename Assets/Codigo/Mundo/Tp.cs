using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour
{
    public GameObject animationExitRoom;
    bool Entro = false;
    [SerializeField] private Spawnpoint sp;
    [SerializeField] private Transform salida;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform locatePlayer;
    [SerializeField] private GameObject Camara;
    [SerializeField] private Transform CamaraTp;

    private void Start()
    {
        GetComponent<Spawnpoint>();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Camara = GameObject.FindGameObjectWithTag("MainCamera");
        locatePlayer = player.GetComponent<Transform>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                animationExitRoom.SetActive(true);
                Invoke("TurnOff", 1);
                player.transform.position = salida.transform.position;
                Camara.transform.position = CamaraTp.transform.position;
                print(this.transform.parent.parent.GetComponent<RoomControl>().name);
            }
        }
    }

    public void TurnOff()
    {
        animationExitRoom.SetActive(false);
    }
}