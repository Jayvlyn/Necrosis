extends Node2D

var dungeon = {}
var roomScene = load("res://Jayden/Rooms/room_1.tscn") #change this later
var dungeonGeneration = load("res://Jayden/dungeon_generation.gd").new()

@onready var map_node = $MapNode

func _ready():
	randomize()
	dungeon = dungeonGeneration.generate(randi_range(-1000, 1000))
	load_map()

func load_map():
	for i in range(0, map_node.get_child_count()):
		map_node.get_child(i).queue_free()
		
	for i in dungeon.keys():
		var instance = roomScene.instantiate()
		map_node.add_child(instance)
		instance.position = i * 18 * 64 #this needs to be rewritten
