using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Room")]
    public int width = 10; // Ancho del mapa de mazmorras
    public int height = 10; // Alto del mapa de mazmorras

    [SerializeField] private Transform roomPrefab;
    [SerializeField] private Transform specialRoomPrefab; // Prefab para la sala especial
    private float offsetX = 0.5f;
    private float offsetY = 0.5f;
    private float tileSize;

    [SerializeField] private Transform world;
    public float roomProbability = 0.2f; // Probabilidad de que aparezca una sala en una dirección
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
        List<Vector2Int> aviableRooms = new List<Vector2Int>();
        List<Vector2Int> usedRooms = new List<Vector2Int>();
        aviableRooms.Add(new Vector2Int(0, 0));

        int remainingRooms = roomQuantity;

        while (remainingRooms > 0 && aviableRooms.Count > 0)
        {
            int actualIndex = Random.Range(0, aviableRooms.Count);
            Vector2Int actualPosition = aviableRooms[actualIndex];
            usedRooms.Add(actualPosition);
            aviableRooms.RemoveAt(actualIndex);

            RoomControl rc = GenerateRoom(new Vector2(actualPosition.x * width, actualPosition.y * height));
            roomAndPos.Add(actualPosition, rc);

            AddAdjacentRooms(actualPosition, aviableRooms, usedRooms);

            remainingRooms--;
        }

        if (usedRooms.Count > 1)
        {
            Vector2Int farthestRoomPosition = FindFarthestRoom(usedRooms);
            RemoveRoom(farthestRoomPosition);
            AddSpecialRoom(farthestRoomPosition);
        }

        foreach (Vector2Int position in usedRooms)
        {
            PrepareRoom(position);
        }
    }

    void AddAdjacentRooms(Vector2Int position, List<Vector2Int> aviableRooms, List<Vector2Int> usedRooms)
    {
        Vector2Int[] directions = { Vector2Int.left, Vector2Int.right, Vector2Int.up, Vector2Int.down };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int adjacentPosition = position + direction;

            if (!aviableRooms.Contains(adjacentPosition) && !usedRooms.Contains(adjacentPosition))
            {
                aviableRooms.Add(adjacentPosition);
            }
        }
    }

    Vector2Int FindFarthestRoom(List<Vector2Int> usedRooms)
    {
        Vector2Int startRoom = usedRooms[0];
        float maxDistance = 0;
        Vector2Int farthestRoom = startRoom;

        foreach (Vector2Int roomPosition in usedRooms)
        {
            float distance = Vector2Int.Distance(startRoom, roomPosition);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthestRoom = roomPosition;
            }
        }

        return farthestRoom;
    }

    void PrepareRoom(Vector2Int position)
    {
        RoomControl roomControl = roomAndPos[position];
        bool left = roomAndPos.ContainsKey(position + Vector2Int.left);
        bool right = roomAndPos.ContainsKey(position + Vector2Int.right);
        bool up = roomAndPos.ContainsKey(position + Vector2Int.up);
        bool down = roomAndPos.ContainsKey(position + Vector2Int.down);
        roomControl.PrepareRoom(left, up, down, right);
        GameObject goLeft = roomAndPos.ContainsKey(position + Vector2Int.left) ? roomAndPos[position + Vector2Int.left].gameObject : null;
        GameObject goRight = roomAndPos.ContainsKey(position + Vector2Int.right) ? roomAndPos[position + Vector2Int.right].gameObject : null;
        GameObject goUp = roomAndPos.ContainsKey(position + Vector2Int.up) ? roomAndPos[position + Vector2Int.up].gameObject : null;
        GameObject goDown = roomAndPos.ContainsKey(position + Vector2Int.down) ? roomAndPos[position + Vector2Int.down].gameObject : null;
        roomControl.PassRooms(goLeft, goRight, goUp, goDown);
        
    }

    void RemoveRoom(Vector2Int position)
    {
        RoomControl roomToRemove = roomAndPos[position];
        roomAndPos.Remove(position);
        Destroy(roomToRemove.gameObject);
    }

    void AddSpecialRoom(Vector2Int position)
    {
        RoomControl specialRoom = GenerateRoom(new Vector2(position.x * width, position.y * height));
        specialRoom.ChangePrefab(specialRoomPrefab);
        roomAndPos.Add(position, specialRoom);
    }

    RoomControl GenerateRoom(Vector2 roomOffset)
    {
        currentRoom += 1;
        Transform parentT = Instantiate(roomPrefab);
        parentT.name = "Room" + currentRoom;
        parentT.transform.parent = world;
        parentT.position = roomOffset;

        return parentT.GetComponent<RoomControl>();
    }
}