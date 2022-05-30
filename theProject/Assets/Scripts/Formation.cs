using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Formation : MonoBehaviour
{
        [SerializeField] Squad squad;

    void Awake()
    {
        squad = GetComponent<Squad>();
    }

    public Dictionary<Entity, Vector2Int> CalculatePositions(Vector2Int coordinates)
    {
        
        List<Entity> soldiers = squad.GetSoldiers();
        Dictionary<Entity, Vector2Int> soldiersNewCoordinates = new Dictionary<Entity, Vector2Int>();
        int soldierNumber = 0;
        int add = 1;
        foreach (Entity Entity in soldiers)
        {
            soldiersNewCoordinates.Add(Entity, CalculateSoldierCoordinates(soldierNumber, coordinates, add));
            soldierNumber++;
            add *= -1;
        }
        return soldiersNewCoordinates;
    }

    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier
    private Vector2Int CalculateSoldierCoordinates(int soldierNumber, in Vector2Int coordinates,in int add)
    {
    // Horizontal line we change x
        soldierNumber = add * soldierNumber;
        TilemapManager.TileState tileState = TilemapManager.GetTileState(coordinates.x + soldierNumber, coordinates.y);
        if ( tileState == TilemapManager.TileState.free)
        {
            Vector2Int soldierCoordinates = new Vector2Int(coordinates.x + soldierNumber,  coordinates.y);
            return soldierCoordinates;
        } else if (tileState == TilemapManager.TileState.taken)
        {
            Vector2Int soldierCoordinates = new Vector2Int(coordinates.x,  coordinates.y);
            return soldierCoordinates;
        } else 
        {
            Vector2Int soldierCoordinates = new Vector2Int(coordinates.x,  coordinates.y);
            return soldierCoordinates;
        }
    }



}
