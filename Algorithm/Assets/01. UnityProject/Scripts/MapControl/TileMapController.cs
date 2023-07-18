using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{
    protected string tilemapObjName = default;

    protected MapBoard mapController = default;
    protected Tilemap tileMap = default;
    protected List<GameObject> allTileobjs = default;

    //! Awake 타임에 초기화 할 내용을 상속받은 클래스별로
    public virtual void InitAwake(MapBoard mapController_)
    {
        mapController = mapController_;
        tileMap = gameObject.FindChildComponent<Tilemap>(tilemapObjName);

        // 직사각형 형태로 초기화 된 타일을 캐싱해서 가지고 있는다.
        allTileobjs = tileMap.gameObject.GetChildrenObjs();
        if (allTileobjs.IsValid())
        {
            allTileobjs.Sort(GFunc.CompareTileObjToLocalPos2D);
        }
        else { allTileobjs = new List<GameObject>(); }
    }       // InitAwake()
}