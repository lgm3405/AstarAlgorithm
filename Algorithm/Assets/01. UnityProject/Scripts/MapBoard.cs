using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    private const string TERRAIN_MAP_OBJ_NAME = "TerrainGrid";

    public Vector2Int MapCellSize { get; private set; } = default;
    public Vector2 MapCellGap { get; private set; } = default;

    private TerrainMap terrainMap = default;

    private void Awake()
    {
        // { 매니저 스크립트를 초기화한다.
        ResManager.Instance.Create();
        // } 매니저 스크립트를 초기화한다.

        // { 맵에 지형을 초기화하여 배치한다.
        terrainMap = gameObject.FindChildComponent<TerrainMap>(TERRAIN_MAP_OBJ_NAME);
        terrainMap.InitAwake(this);
        MapCellSize = terrainMap.GetCellSize();
        MapCellGap = terrainMap.GetCellGap();
        // } 맵에 지형을 초기화하여 배치한다.
    }

    //! 타일 인덱스를 받아서 해당 타일을 리턴하는 함수
    public TerrainController GetTerrain(int idx1D)
    {
        return terrainMap.GetTile(idx1D);
    }          // GetTerrain()

    //! 맵의 x 좌표를 받아서 해당 열의 타일을 리스트로 가져오는 함수
    public List<TerrainController> GetTerrains_Colum(int xIdx2D)
    {
        return GetTerrains_Colum(xIdx2D, false);
    }

    public List<TerrainController> GetTerrains_Colum(int xIdx2D, bool isSortReverse)
    {
        List<TerrainController> terrains = new List<TerrainController>();
        TerrainController tempTile = default;
        int tileIdx1D = 0;
        for (int y = 0; y < MapCellSize.y; y++)
        {
            tileIdx1D = y * MapCellSize.x + xIdx2D;

            tempTile = terrainMap.GetTile(tileIdx1D);
            terrains.Add(tempTile);
        }         // loop: y 열의 크기만큼 순회하는 루프

        if (terrains.IsValid())
        {
            if (isSortReverse) { terrains.Reverse(); }
            else { /* Do nothing */ }

            return terrains;
        }
        else { return default; }
                  // GetTerrains_Colum()
    }

    //! 지형의 인덱스를 2D 좌표로 리턴하는 함수
    public Vector2Int GetTileIdx2D(int idx1D)
    {
        Vector2Int tileIdx2D = Vector2Int.zero;
        tileIdx2D.x = idx1D % MapCellSize.x;
        tileIdx2D.y = idx1D / MapCellSize.x;

        return tileIdx2D;
    }         // GetTileIdx2D()

    //! 지형의 2D 좌표를 인덱스로 리턴하는 함수
    public int GetTileIdx1D(Vector2Int idx2D)
    {
        int tileIdx1D = (idx2D.y * MapCellSize.x) + idx2D.x;
        return tileIdx1D;
    }         // GetTileIdx1D()

    //! 두 지형 사이의 타일 거리를 리턴하는 함수
    public Vector2Int GetDistance2D(GameObject targetTerrainobj, GameObject destTerrainobj)
    {
        Vector2 localDistance = destTerrainobj.transform.localPosition - targetTerrainobj.transform.localPosition;

        Vector2Int distance2D = Vector2Int.zero;
        distance2D.x = Mathf.RoundToInt(localDistance.x / MapCellGap.x);
        distance2D.y = Mathf.RoundToInt(localDistance.y / MapCellGap.y);
        distance2D = GFunc.Abs(distance2D);

        return distance2D;
    }         // GetDistance2D()

    //! 2D 좌표를 기준으로 주변 4방향 타일의 인덱스를 리턴하는 함수
    public List<int> GetTileIdx2D_Around4ways(Vector2Int targetIdx2D)
    {
        List<int> idx1D_around4ways = new List<int>();
        List<Vector2Int> idx2D_around4ways = new List<Vector2Int>();
        idx2D_around4ways.Add(new Vector2Int(targetIdx2D.x - 1, targetIdx2D.y));
        idx2D_around4ways.Add(new Vector2Int(targetIdx2D.x + 1, targetIdx2D.y));
        idx2D_around4ways.Add(new Vector2Int(targetIdx2D.x, targetIdx2D.y - 1));
        idx2D_around4ways.Add(new Vector2Int(targetIdx2D.x, targetIdx2D.y + 1));

        foreach(var idx2D in idx2D_around4ways)
        {
            // 2D 좌표가 유효한지 검사한다.
            if (idx2D.x.IsInRange(0, MapCellSize.x) == false) { continue; }
            if (idx2D.y.IsInRange(0, MapCellSize.y) == false) { continue; }

            idx1D_around4ways.Add(GetTileIdx1D(idx2D));
        }

        return idx1D_around4ways;
    }       // GetTileIdx2D_Around4ways()
}
