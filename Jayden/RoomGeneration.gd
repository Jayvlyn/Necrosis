extends Node2D

var currentScene

var numRoom = 5

var width = 10
var height = 10

func _ready():
	generate_layout()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func generate_layout():
	#var tile_pos = local_to_map(position) #converts position passed into tilemap coords
	#randi_range(1, 1)
	var scene = load("res://Jayden/Rooms/room_1.tscn")
	var instance = scene.instantiate()
	add_child(instance)




#func add_my_node():


 #set position if needed
 #myNode_instance.global_transform = global_transform
