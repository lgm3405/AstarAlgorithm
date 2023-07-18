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

    //! Awake Ÿ�ӿ� �ʱ�ȭ �� ������ ��ӹ��� Ŭ��������
    public virtual void InitAwake(MapBoard mapController_)
    {
        mapController = mapController_;
        tileMap = gameObject.FindChildComponent<Tilemap>(tilemapObjName);

        // ���簢�� ���·� �ʱ�ȭ �� Ÿ���� ĳ���ؼ� ������ �ִ´�.
        allTileobjs = tileMap.gameObject.GetChildrenObjs();
        if (allTileobjs.IsValid())
        {
            allTileobjs.Sort(GFunc.CompareTileObjToLocalPos2D);
        }
        else { allTileobjs = new List<GameObject>(); }
    }       // InitAwake()
}