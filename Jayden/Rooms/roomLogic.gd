extends Node2D

var completeRoom = false
@onready var map = $Tiles
@onready var roomPos = $RoomPos

# Called when the node enters the scene tree for the first time.
func _ready():
	map = get_node("Tiles")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_room_area_body_entered(body):
	if (body.is_in_group("Player") && !completeRoom):
		var position = map.local_to_map(to_local(roomPos.position))
		print(position)
		roomSetup()

func roomSetup():
	var position = map.local_to_map(to_local(roomPos.position))
	#map.set_cell(0, position, -1)
	
func enemySpawn():
	pass
