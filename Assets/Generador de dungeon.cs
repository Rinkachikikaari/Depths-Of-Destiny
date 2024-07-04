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
    [SerializeField] private Transform uniqueRoomPrefab; // Prefab de la sala única
    [SerializeField] private Transform bossRoomPrefab; // Prefab de la sala de jefe
    private float offsetX = 0.5f;
    private float offsetY = 0.5f;

    [SerializeField] private Transform world;
    int currentRoom = 0;
    int uniqueRoomsGenerated = 0;

    [Header("Dungeon")]
    [SerializeField] private int roomQuantity;
    [SerializeField] private int uniqueRoomQuantity = 1; // Cantidad de salas únicas
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
            else if (uniqueRoomsGenerated < uniqueRoomQuantity && Random.Range(0, remainingRooms) < uniqueRoomQuantity - uniqueRoomsGenerated)
            {
                rc = GenerateRoom(uniqueRoomPrefab, new Vector2(actualPosition.x * width, actualPosition.y * height));
                uniqueRoomsGenerated++;
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

        // Encontrar la sala más alejada
        Vector2Int farthestRoomPosition = FindFarthestRoom(usedRooms);
        // Reemplazar la sala más alejada con la sala de jefe
        ReplaceWithBossRoom(farthestRoomPosition);

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

    Vector2Int FindFarthestRoom(List<Vector2Int> usedRooms)
    {
        Vector2Int startRoom = new Vector2Int(0, 0);
        Vector2Int farthestRoom = startRoom;
        float maxDistance = 0;

        foreach (Vector2Int room in usedRooms)
        {
            float distance = Vector2Int.Distance(startRoom, room);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestRoom = room;
            }
        }

        return farthestRoom;
    }

    void ReplaceWithBossRoom(Vector2Int position)
    {
        // Destruir la sala original
        Destroy(roomAndPos[position].gameObject);
        // Generar la sala de jefe
        RoomControl bossRoom = GenerateRoom(bossRoomPrefab, new Vector2(position.x * width, position.y * height));
        // Reemplazar en el diccionario
        roomAndPos[position] = bossRoom;
    }
}
