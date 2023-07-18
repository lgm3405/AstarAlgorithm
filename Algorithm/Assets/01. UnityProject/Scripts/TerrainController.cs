using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    private const string TILE_FRONT_RENDERER_OBJ_NAME = "FrontRenderer";

    private TerrainType terrainType = TerrainType.NONE;
    private MapBoard mapController = default;

    public bool IsPassable { get; private set; } = false;
    public int TileIdx1D { get; private set; } = -1;
    public Vector2Int TileIdx2D { get; private set; } = default;

    #region 길찾기 알고리즘을 위한 변수
    private SpriteRenderer frontRenderer = default;
    private Color defaultColor = default;
    private Color selectedColor = default;
    private Color searchColor = default;
    private Color inactiveColor = default;

    #endregion       // 길찾기 알고리즘을 위한 변수
    private void Awake()
    {
        frontRenderer = gameObject.FindChildComponent<SpriteRenderer>(TILE_FRONT_RENDERER_OBJ_NAME);

        defaultColor = new Color(1f, 1f, 1f, 1f);
        selectedColor = new Color(236f / 255f, 130f / 255f, 20f / 255f, 1f);
        searchColor = new Color(0f, 192f / 255f, 0f, 1f);
        inactiveColor = new Color(128f / 255f, 128f / 255f, 128f / 255f, 1f);
    }          // Awake()

    //! 지형 정보를 초기 설정한다.
    public void SetupTerrain(MapBoard mapController_, TerrainType type_, int tileIdx1D_)
    {
        terrainType = type_;
        mapController = mapController_;
        TileIdx1D = tileIdx1D_;
        TileIdx2D = mapController.GetTileIdx2D(TileIdx1D);

        string prefabName = string.Empty;
        switch (type_)
        {
            case TerrainType.PLAIN_PASS:
                prefabName = RDefine.TERRAIN_PREF_PLAIN;
                IsPassable = true;
                break;
            case TerrainType.OCEAN_N_PASS:
                prefabName = RDefine.TERRAIN_PREF_OCEAN;
                IsPassable = false;
                break;
            default:
                prefabName = "Tile_Default";
                IsPassable = false;
                break;
        }         // switch: 타일의 타입별로 다른 설정을 한다.

        this.name = string.Format("{0}_{1}", prefabName, TileIdx1D);
    }           // SetupTerrain()

    //! 지형의 Front 색상을 변경한다.
    public void SetTileActiveColor(RDefine.TileStatusColor tilestatus)
    {
        switch (tilestatus)
        {
            case RDefine.TileStatusColor.SELECTED:
                frontRenderer.color = selectedColor;
                break;
            case RDefine.TileStatusColor.SEARCH:
                frontRenderer.color = searchColor;
                break;
            case RDefine.TileStatusColor.INACTIVE:
                frontRenderer.color = inactiveColor;
                break;
            default:
                frontRenderer.color = defaultColor;
                break;
        }
    }          // SetTileActiveColor()
}
