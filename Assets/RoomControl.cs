using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    [SerializeField] List<GameObject> doors; // Door U, D, R, L


    void Start()
    {
    }

    public void PrepareRoom(bool L, bool U, bool D, bool R)
    {
        doors[0].SetActive(U); // Upper Door
        doors[1].SetActive(D); // Down Door
        doors[2].SetActive(R); // Right Door
        doors[3].SetActive(L); // Left Door
    }

    void Update()
    {
    }
}
