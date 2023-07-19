using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RDefine
{
    public const string TERRAIN_PREF_OCEAN = "Tile_Ocean";
    public const string TERRAIN_PREF_PLAIN = "Tile_Plain";

    public const string OBSTACLE_PREF_PLAIN_CASTLE = "Tile_Castle";

    public enum TileStatusColor
    {
        DEFAULT, SELECTED, SEARCH, INACTIVE
    }
}
