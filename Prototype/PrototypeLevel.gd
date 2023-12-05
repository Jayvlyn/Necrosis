extends Node2D

#var cursorTest = preload("res://Piper/assets/cursorTest.png")

var dungeon = {}
var room1 = load("res://Jayden/Rooms/room_1.tscn") #change this later
var room2 = load("res://Jayden/Rooms/room_2.tscn") #default
var dungeonGeneration = load("res://Jayden/dungeon_generation.gd").new()

var rng = RandomNumberGenerator.new()

@onready var map_node = $MapNode

func _ready():
	#Input.set_custom_mouse_cursor(cursorTest,Input.CURSOR_ARROW, Vector2(12,12))
	$Layer1.play()
	$Layer2.play()
	playLayerOne()
	
	randomize()
	dungeon = dungeonGeneration.generate(randi_range(-1000, 1000))
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
	
	for i in dungeon.keys():
		randomize()
		match(rng.randi_range(0,1)):
			0:
				instance = room1.instantiate()
				print("i am room 1. ill kill you!")
			1:
				instance = room2.instantiate()
				print("i am room 2. die.")
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
