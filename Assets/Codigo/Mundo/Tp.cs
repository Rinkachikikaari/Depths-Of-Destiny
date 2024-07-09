using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour
{
    public GameObject animationExitRoom;
    bool Entro = false;
    public PlayerMovement PlayerMovement;
    [SerializeField] private Transform salida;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform locatePlayer;
    [SerializeField] private GameObject Camara;
    [SerializeField] private Transform CamaraTp;

    [SerializeField] bool isActive = true; 

    private void Start()
    {
        PlayerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Camara = GameObject.FindGameObjectWithTag("MainCamera");
        locatePlayer = player.GetComponent<Transform>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isActive)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f)
            {
                if (PlayerMovement.canTp)
                {
                    animationExitRoom.SetActive(true);
                    PlayerMovement.enabled = false;
                    Invoke("TurnOff", 1);
                    player.transform.position = salida.transform.position;
                    Camara.transform.position = CamaraTp.transform.position;
                    PlayerMovement.canTp = false;
                    Invoke("CanTp", 2);
                    print(this.transform.parent.parent.GetComponent<RoomControl>().name);
                }
            }
        }
    }

    public void TurnOff()
    {
        PlayerMovement.enabled = true;
        animationExitRoom.SetActive(false);
    }
    public void CanTp()
    {
        PlayerMovement.canTp = true;
    }

    public void CloseDoor()
    {
        isActive = false;
        // puertaVisual.gameObject.SetActive(true);
    }

    public void OpenDoor()
    {
        isActive = true;
        // puertaVisual.gameObject.SetActive(false);
    }
}