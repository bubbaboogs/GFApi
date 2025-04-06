using UnityEngine.Tilemaps;
using GFApi;

namespace GFApi.Tiles{
public static class GameTiles
{
	public static TileBase FRIENDLY_FIELDS_GRASS => MainPlugin.tiles["Friendly-Fields-Tileset_0"];
	public static TileBase FRIENDLY_FIELDS_DIRT_BOTTOM => MainPlugin.tiles["Friendly-Fields-Tileset_1"];
	public static TileBase FRIENDLY_FIELDS_FILLER => MainPlugin.tiles["Friendly-Fields-Tileset_13"];
	public static TileBase FRIENDLY_FIELDS_GRASS_EDGE => MainPlugin.tiles["Friendly-Fields-Tileset_2"];
	public static TileBase FRIENDLY_FIELDS_FILLER_LEFT => MainPlugin.tiles["Friendly-Fields-Tileset_21"];
	public static TileBase FRIENDLY_FIELDS_FILLER_T => MainPlugin.tiles["Friendly-Fields-Tileset_23"];
	public static TileBase FRIENDLY_FIELDS_GRASS_LEFT_EDGE => MainPlugin.tiles["Friendly-Fields-Tileset_8"];
	public static TileBase ROCKY_RETREAT_DIRT_BOTTOM => MainPlugin.tiles["Rocky-Retreat-Tileset_12"];
	public static TileBase ROCKY_RETREAT_DIRT_MIDDLE => MainPlugin.tiles["Rocky-Retreat-Tileset_13"];
	public static TileBase ROCKY_RETREAT_DIRT_BOTTOM_LEFT_CORNER => MainPlugin.tiles["Rocky-Retreat-Tileset_18"];
	public static TileBase ROCKY_RETREAT_DIRT_BOTTOM_RIGHT_CORNER => MainPlugin.tiles["Rocky-Retreat-Tileset_20"];
	public static TileBase ROCKY_RETREAT_DIRT_LEFT => MainPlugin.tiles["Rocky-Retreat-Tileset_8"];
	public static TileBase FRIENDLY_FIELDS_FILLER_MIDDLE => MainPlugin.tiles["Friendly-Fields-Tileset_20"];
	public static TileBase FRIENDLY_FIELDS_FILLER_HORIZONTAL => MainPlugin.tiles["Friendly-Fields-Tileset_22"];
	public static TileBase ROCKY_RETREAT_STONE => MainPlugin.tiles["Rocky-Retreat-Tileset_0"];
	public static TileBase ROCKY_RETREAT_GRASS_STONE_BIT => MainPlugin.tiles["Rocky-Retreat-Tileset_1"];
	public static TileBase ROCKY_RETREAT_STONE_TOP_LEFT => MainPlugin.tiles["Rocky-Retreat-Tileset_10"];
	public static TileBase ROCKY_RETREAT_FILLER => MainPlugin.tiles["Rocky-Retreat-Tileset_11"];
	public static TileBase ROCKY_RETREAT_STONE_BOTTOM_LEFT => MainPlugin.tiles["Rocky-Retreat-Tileset_14"];
	public static TileBase ROCKY_RETREAT_STONE_BOTTOM_RIGHT => MainPlugin.tiles["Rocky-Retreat-Tileset_15"];
	public static TileBase ROCKY_RETREAT_STONE_CEILING_BIT => MainPlugin.tiles["Rocky-Retreat-Tileset_16"];
	public static TileBase ROCKY_RETREAT_STONE_CEILING => MainPlugin.tiles["Rocky-Retreat-Tileset_17"];
	public static TileBase ROCKY_RETREAT_DIRT_RIGHT => MainPlugin.tiles["Rocky-Retreat-Tileset_19"];
	public static TileBase ROCKY_RETREAT_GRASS => MainPlugin.tiles["Rocky-Retreat-Tileset_2"];
	public static TileBase ROCKY_RETREAT_DIRT_TOP => MainPlugin.tiles["Rocky-Retreat-Tileset_21"];
	public static TileBase ROCKY_RETREAT_DIRT_STONE_BOTTOM_RIGHT => MainPlugin.tiles["Rocky-Retreat-Tileset_22"];
	public static TileBase ROCKY_RETREAT_DIRT_STONE_BOTTOM_LEFT => MainPlugin.tiles["Rocky-Retreat-Tileset_23"];
	public static TileBase ROCKY_RETREAT_DIRT_TOP_LEFT_CORNER => MainPlugin.tiles["Rocky-Retreat-Tileset_24"];
	public static TileBase ROCKY_RETREAT_DIRT_TOP_RIGHT_CORNER => MainPlugin.tiles["Rocky-Retreat-Tileset_25"];
	public static TileBase ROCKY_RETREAT_STONE_BITS => MainPlugin.tiles["Rocky-Retreat-Tileset_3"];
	public static TileBase ROCKY_RETREAT_DIRT_STONE_TOP_RIGHT => MainPlugin.tiles["Rocky-Retreat-Tileset_4"];
	public static TileBase ROCKY_RETREAT_DIRT_STONE_TOP_LEFT => MainPlugin.tiles["Rocky-Retreat-Tileset_5"];
	public static TileBase ROCKY_RETREAT_STONE_RIGHT => MainPlugin.tiles["Rocky-Retreat-Tileset_6"];
	public static TileBase ROCKY_RETREAT_STONE_LEFT => MainPlugin.tiles["Rocky-Retreat-Tileset_7"];
	public static TileBase ROCKY_RETREAT_STONE_TOP_RIGHT => MainPlugin.tiles["Rocky-Retreat-Tileset_9"];
	public static TileBase FRIENDLY_FIELDS_DIRT_MIDDLE => MainPlugin.tiles["Friendly-Fields-Tileset_14"];
	public static TileBase FRIENDLY_FIELDS_AIR => MainPlugin.tiles["Friendly-Fields-Tileset_24"];
	public static TileBase FRIENDLY_FIELDS_DIRT_RIGHT_EDGE => MainPlugin.tiles["Friendly-Fields-Tileset_3"];
	public static TileBase FRIENDLY_FIELDS_DIRT_LEFT_EDGE => MainPlugin.tiles["Friendly-Fields-Tileset_7"];
	public static TileBase TRAIN_STATION_GRASS => MainPlugin.tiles["Train-Station-Tileset_0"];
	public static TileBase TRAIN_STATION_TILE => MainPlugin.tiles["Train-Station-Tileset_10"];
	public static TileBase TRAIN_STATION_GRASS_TILE_LEFT => MainPlugin.tiles["Train-Station-Tileset_11"];
	public static TileBase TRAIN_STATION_FILLER => MainPlugin.tiles["Train-Station-Tileset_12"];
	public static TileBase TRAIN_STATION_FILLER_MIDDLE => MainPlugin.tiles["Train-Station-Tileset_16"];
	public static TileBase TRAIN_STATION_FILLER_LEFT => MainPlugin.tiles["Train-Station-Tileset_17"];
	public static TileBase TRAIN_STATION_FILLER_HORIZONTAL => MainPlugin.tiles["Train-Station-Tileset_18"];
	public static TileBase TRAIN_STATION_FILLER_T => MainPlugin.tiles["Train-Station-Tileset_19"];
	public static TileBase TRAIN_STATION_GRASS_TILE_RIGHT => MainPlugin.tiles["Train-Station-Tileset_9"];
	public static TileBase FRIENDLY_FIELDS_STONE_2 => MainPlugin.tiles["Friendly-Fields-Tileset_10"];
	public static TileBase FRIENDLY_FIELDS_STONE_GRASS => MainPlugin.tiles["Friendly-Fields-Tileset_11"];
	public static TileBase FRIENDLY_FIELDS_GRASS_STONE => MainPlugin.tiles["Friendly-Fields-Tileset_12"];
	public static TileBase FRIENDLY_FIELDS_FENCE_1 => MainPlugin.tiles["Friendly-Fields-Tileset_15"];
	public static TileBase FRIENDLY_FIELDS_FENCE_2 => MainPlugin.tiles["Friendly-Fields-Tileset_16"];
	public static TileBase FRIENDLY_FIELDS_MUDDY_GRASS => MainPlugin.tiles["Friendly-Fields-Tileset_17"];
	public static TileBase FRIENDLY_FIELDS_MUD => MainPlugin.tiles["Friendly-Fields-Tileset_18"];
	public static TileBase FRIENDLY_FIELDS_MUDDY_GRASS_BOTTOM => MainPlugin.tiles["Friendly-Fields-Tileset_19"];
	public static TileBase FRIENDLY_FIELDS_DIRT_GRASS_CORNER => MainPlugin.tiles["Friendly-Fields-Tileset_4"];
	public static TileBase FRIENDLY_FIELDS_STONE_1 => MainPlugin.tiles["Friendly-Fields-Tileset_9"];
}
}
