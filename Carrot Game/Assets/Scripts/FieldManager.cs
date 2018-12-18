using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    private Vector2Int fieldSize;
    [SerializeField]
    private Carrot carrotPrefab;
    [SerializeField]
    private Transform carrotParent;

    private Carrot[,] carrots;
    private Vector3 fieldOffset;

    private Vector2Int[] adjacentCoOrds = new Vector2Int[]
    {
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(0, 1),
    };


    private void Awake()
    {
        fieldOffset = new Vector3((fieldSize.x - 1) * -0.5f, (fieldSize.y - 1) * -0.5f, 0);
        GenerateCarrots();
    }


    void Update()
    {
        CheckForReproduction();
    }


    private void GenerateCarrots()
    {
        carrots = new Carrot[fieldSize.x, fieldSize.y];

        for (int x = 0; x < fieldSize.x; x++)
        {
            for (int y = 0; y < fieldSize.y; y++)
            {
                Carrot newCarrot = Instantiate(carrotPrefab, GetPositionFromCoOrds(new Vector2Int(x, y)), Quaternion.identity);
                newCarrot.transform.SetParent(carrotParent, true);
                newCarrot.InitializeFullyGrownPlant();
                carrots[x, y] = newCarrot;
            }
        }
    }


    private Vector3 GetPositionFromCoOrds(Vector2Int coOrds)
    {
        return new Vector3(coOrds.x, coOrds.y, 0) + fieldOffset;
    }


    public Carrot GetNearestFullGrownCarrot(Vector3 position)
    {
        Carrot nearestCarrot = null;
        float nearestCarrotDistance = 0;

        for (int x = 0; x < fieldSize.x; x++)
        {
            for (int y = 0; y < fieldSize.y; y++)
            {
                Carrot carrot = carrots[x, y];
                if (carrot != null && carrot.isGrown)
                {
                    float distanceToCarrot = Vector3.Distance(position, carrot.transform.position);
                    if (nearestCarrot == null || distanceToCarrot < nearestCarrotDistance)
                    {
                        nearestCarrot = carrot;
                        nearestCarrotDistance = distanceToCarrot;
                    }
                }
            }
        }

        return nearestCarrot;
    }


    private void CheckForReproduction()
    {
        for (int x = 0; x < fieldSize.x; x++)
        {
            for (int y = 0; y < fieldSize.y; y++)
            {
                Carrot carrot = carrots[x, y];
                if (carrot == null) continue;
                if (!carrot.IsReadyToReproduce()) continue;
                carrot.IncrementReproduceTimer();
                ReproduceInAdjacentTile(new Vector2Int(x, y));
            }
        }
    }


    private void ReproduceInAdjacentTile(Vector2Int coOrds)
    {
        List<Vector2Int> availableTiles = new List<Vector2Int>();
        for (int i = 0; i < adjacentCoOrds.Length; i++)
        {
            Vector2Int tile = coOrds + adjacentCoOrds[i];
            if (CoOrdsAreInField(tile) && carrots[tile.x, tile.y] == null)
            {
                availableTiles.Add(tile);
            }
        }
        if (availableTiles.Count == 0) return;
        Vector2Int selectedTile = availableTiles[Random.Range(0, availableTiles.Count)];

        // Very occasionally this line has thrown up an error, and I haven't the faintest clue why...
        //Carrot newCarrot = Instantiate(carrotPrefab, GetPositionFromCoOrds(selectedTile), Quaternion.identity);
        //newCarrot.transform.SetParent(carrotParent, true);
        //newCarrot.Grow();
        //carrots[selectedTile.x, selectedTile.y] = newCarrot;

        Carrot newCarrot;
        newCarrot = Instantiate(carrotPrefab);
        newCarrot.transform.position = GetPositionFromCoOrds(selectedTile);
        newCarrot.transform.SetParent(carrotParent, true);
        newCarrot.Grow();
        carrots[selectedTile.x, selectedTile.y] = newCarrot;

    }


    private bool CoOrdsAreInField(Vector2Int coOrds)
    {
        return (
            coOrds.x >= 0 &&
            coOrds.y >= 0 &&
            coOrds.x < fieldSize.x &&
            coOrds.y < fieldSize.y);
    }


    public int GetPlantCount()
    {
        int plantCount = 0;
        for (int x = 0; x < fieldSize.x; x++)
        {
            for (int y = 0; y < fieldSize.y; y++)
            {
                Carrot carrot = carrots[x, y];
                if (carrot != null) plantCount++;
            }
        }
        return plantCount;
    }
}
