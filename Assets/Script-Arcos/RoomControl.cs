using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    [SerializeField]List<Transform> doors; // DoorL, R, U, D
    [SerializeField]List<Transform> walls; // WallL, R, U, D

    [SerializeField]List<GameObject> otherRoom;


    void Start()
    {

    }

    public void PrepareRoom(bool L, bool U, bool D, bool R)
    {
        Debug.Log(this.name + " " + L + " " + U + " " + D + " " + R);
        doors[0].gameObject.SetActive(L);
        doors[1].gameObject.SetActive(U);
        doors[2].gameObject.SetActive(D);
        doors[3].gameObject.SetActive(R);

        walls[0].gameObject.SetActive(!L);
        walls[1].gameObject.SetActive(!U);
        walls[2].gameObject.SetActive(!D);
        walls[3].gameObject.SetActive(!R);
    }

    public void PassRooms(GameObject L, GameObject U, GameObject D, GameObject R)
    {
        otherRoom.Add(L);
        otherRoom.Add(U);
        otherRoom.Add(D);
        otherRoom.Add(R);
    }
    public void ChangePrefab(Transform newPrefab)
    {
        Destroy(gameObject); // Eliminar la sala actual
        Instantiate(newPrefab, transform.position, Quaternion.identity); // Instanciar la nueva sala
    }

    void Update()
    {

    }
}
