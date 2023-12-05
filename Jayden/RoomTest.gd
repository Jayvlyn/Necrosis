extends Node2D

var dungeon = {}
var room1 = load("res://Jayden/Rooms/room_1.tscn") #default
var room2 = load("res://Jayden/Rooms/room_2.tscn") #default
var dungeonGeneration = preload("res://Jayden/dungeon_generation.gd").new()

var rng = RandomNumberGenerator.new()

@onready var map_node = $MapNode

func _ready():
	randomize()
	dungeon = dungeonGeneration.generate(randi_range(-1000, 1000))
	load_map()

func load_map():
	for i in range(0, map_node.get_child_count()):
		map_node.get_child(i).queue_free()
	
	var instance
	
	for i in dungeon.keys():
		randomize()
		match(rng.randi_range(0,1)):
			0:
				instance = room1.instantiate()
				print("i am room 1 being made")
			1:
				instance = room2.instantiate()
				print("i am room 2 being made")
		map_node.add_child(instance)
		instance.position = i * 18 * 64 #if enough time, rewrite this
