extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.

onready var _server_filter_ui = preload("res://mods/PotatoServerFilter/UI/ServerFilter.tscn").instance()

func _ready():
	get_tree().connect("node_added", self, "_node_added")


func ApplyFilters():
	if _server_filter_ui.is_dedicated_enabled():
		Steam.addRequestLobbyListStringFilter("lurefilter", "dedicated", Steam.LOBBY_COMPARISON_EQUAL)
	if _server_filter_ui.is_lure_enabled():
		Steam.addRequestLobbyListStringFilter("version", str(Globals.GAME_VERSION) + ".lure", Steam.LOBBY_COMPARISON_EQUAL)

func _node_added(node):
	if node.name == "lobby_browser":
		node.add_child(_server_filter_ui)
