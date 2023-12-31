extends Node2D

#var cursorTest = preload("res://Piper/assets/cursorTest.png")

var dungeon = {}
var room1 = preload("res://Jayden/Rooms/room_1.tscn")
var room2 = preload("res://Jayden/Rooms/room_2.tscn")
var room3 = preload("res://Jayden/Rooms/room_3.tscn")
var final_room = preload("res://Jayden/Rooms/final_room.tscn")

var dungeonGeneration = load("res://Jayden/dungeon_generation.gd").new()
var dungeonSize

var rng = RandomNumberGenerator.new()

@onready var map_node = $MapNode

func _ready():
	#Input.set_custom_mouse_cursor(cursorTest,Input.CURSOR_ARROW, Vector2(12,12))
	$Layer1.play()
	$Layer2.play()
	
	randomize()
	dungeon = dungeonGeneration.generate(randi_range(-1000, 1000))
	dungeonSize = dungeonGeneration.get_dungeonSize()
	load_map()
	
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func playLayerOne():
	$Layer1.volume_db = 0;
	$Layer2.volume_db = -80;
	pass
	
func playLayerTwo():
	$Layer2.volume_db = 0;
	$Layer1.volume_db = -80;
	pass

func load_map():
	for i in range(0, map_node.get_child_count()):
		map_node.get_child(i).queue_free()
	
	var instance
	var lastroom = 0
	
	for i in dungeon.keys():
		lastroom += 1
		randomize()
		if (lastroom == dungeonSize):
			instance = final_room.instantiate()
			print("i am the final room. The boss room!")
		else:
			match(rng.randi_range(0,2)):
				0:
					instance = room1.instantiate()
					print("i am room 1. kinniku buster!")
				1:
					instance = room2.instantiate()
					print("i am room 2. rider kick!")
				2:
					instance = room3.instantiate()
					print("i am room 3. shoryuken!")
		map_node.add_child(instance)
		instance.position = i * 18 * 64 #if enough time, rewrite this

func _on_area_2d_body_entered(body):
	if (body.is_in_group("Player")):
		playLayerTwo()
	pass # Replace with function body.


func _on_area_2d_body_exited(body):
	if (body.is_in_group("Player")):
		playLayerOne()
	pass # Replace with function body.
