using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Room")]
    public int width = 10; // Ancho del mapa de mazmorras
    public int height = 10; // Alto del mapa de mazmorras

    [SerializeField] private Transform roomPrefab; // Prefab original
    [SerializeField] private List<Transform> roomPrefabs; // Lista de otros prefabs de salas
    private float offsetX = 0.5f;
    private float offsetY = 0.5f;

    [SerializeField] private Transform world;
    int currentRoom = 0;

    [Header("Dungeon")]
    [SerializeField] private int roomQuantity;
    Dictionary<Vector2Int, RoomControl> roomAndPos = new Dictionary<Vector2Int, RoomControl>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        List<Vector2Int> availableRooms = new List<Vector2Int>();
        List<Vector2Int> usedRooms = new List<Vector2Int>();
        availableRooms.Add(new Vector2Int(0, 0));

        int remainingRooms = roomQuantity;

        while (remainingRooms > 0 && availableRooms.Count > 0)
        {
            int actualIndex = Random.Range(0, availableRooms.Count);
            Vector2Int actualPosition = availableRooms[actualIndex];
            usedRooms.Add(actualPosition);
            availableRooms.RemoveAt(actualIndex);

            RoomControl rc;
            if (currentRoom == 0)
            {
                rc = GenerateRoom(roomPrefab, new Vector2(actualPosition.x * width, actualPosition.y * height));
            }
            else
            {
                Transform selectedRoomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
                rc = GenerateRoom(selectedRoomPrefab, new Vector2(actualPosition.x * width, actualPosition.y * height));
            }

            roomAndPos.Add(actualPosition, rc);

            AddAdjacentRooms(actualPosition, availableRooms, usedRooms);

            remainingRooms--;
            currentRoom++;
        }

        // Configurar puertas para cada sala
        foreach (Vector2Int position in usedRooms)
        {
            PrepareRoom(position);
        }
    }

    void AddAdjacentRooms(Vector2Int position, List<Vector2Int> availableRooms, List<Vector2Int> usedRooms)
    {
        Vector2Int[] directions = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int adjacentPosition = position + direction;

            if (!availableRooms.Contains(adjacentPosition) && !usedRooms.Contains(adjacentPosition))
            {
                availableRooms.Add(adjacentPosition);
            }
        }
    }

    void PrepareRoom(Vector2Int position)
    {
        RoomControl roomControl = roomAndPos[position];
        bool left = roomAndPos.ContainsKey(position + Vector2Int.left);
        bool right = roomAndPos.ContainsKey(position + Vector2Int.right);
        bool up = roomAndPos.ContainsKey(position + Vector2Int.up);
        bool down = roomAndPos.ContainsKey(position + Vector2Int.down);
        roomControl.PrepareRoom(left, up, down, right);
    }

    RoomControl GenerateRoom(Transform prefab, Vector2 roomOffset)
    {
        Transform parentT = Instantiate(prefab);
        parentT.name = "Room" + currentRoom;
        parentT.transform.parent = world;
        parentT.position = roomOffset;

        return parentT.GetComponent<RoomControl>();
    }
}
