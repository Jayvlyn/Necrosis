extends Node2D

var redEnemy = preload("res://Jacob/RedEnemy.tscn")
var whiteEnemy = preload("res://Jacob/WhiteEnemy.tscn")

@onready var map
@onready var roomPos

var roomGenerated = false

var enemies = 0
var rng = RandomNumberGenerator.new()

# Called when the node enters the scene tree for the first time.
func _ready():
	map = $Tiles
	roomPos = $RoomPos

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _on_room_area_body_entered(body):
	if (body.is_in_group("Player") && !roomGenerated):
		roomSetup()
		print("start of spawn")
		for n in rng.randi_range(5, 10):
			enemySpawn()
		roomGenerated = true

func _on_room_area_body_exited(body):
	if (body.is_in_group("Enemy") && body.dead == true && roomGenerated):
		enemies -= 1
		print("enemies: " + str(enemies))
	if (enemies == 0 && roomGenerated):
		roomComplete()

func roomSetup():
	var roomOrigin = map.local_to_map(to_local(roomPos.position))
	for n in 4:
			map.set_cell(0, Vector2i(roomOrigin.x, roomOrigin.y + 7 + n), 0, Vector2i(0, 1)) #left wall
			map.set_cell(0, Vector2i(roomOrigin.x + 17, roomOrigin.y + 7 + n), 0, Vector2i(2, 1)) #right wall
			map.set_cell(0, Vector2i(roomOrigin.x + 7 + n, roomOrigin.y), 0, Vector2i(1, 0)) #top wall
			map.set_cell(0, Vector2i(roomOrigin.x + 7 + n, roomOrigin.y + 17), 0, Vector2i(1, 2)) #bottom wall
	pass

func enemySpawn():
	var roomOrigin = map.local_to_map(to_local(roomPos.position))
	print("roomOrigin: " + str(roomOrigin))
	var instance
	randomize()
	
	if (rng.randi_range(0,1) == 0):
		instance = whiteEnemy.instantiate()
	else:
		instance = redEnemy.instantiate()
	
	instance.position = Vector2((roomOrigin.x + rng.randi_range(3, 12)) * 64, (roomOrigin.y + rng.randi_range(3, 12)) * 64)
	print(instance.position / 64)
	
	get_parent().call_deferred("add_child", instance)
	
	enemies += 1

func roomComplete():
	var roomOrigin = map.local_to_map(to_local(roomPos.position))
	
	map.set_cell(0, Vector2i(roomOrigin.x, roomOrigin.y + 7), 0, Vector2i(3, 1)) #left tiles
	map.set_cell(0, Vector2i(roomOrigin.x, roomOrigin.y + 8), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x, roomOrigin.y + 9), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x, roomOrigin.y + 10), 0, Vector2i(3, 2))
	
	map.set_cell(0, Vector2i(roomOrigin.x + 17, roomOrigin.y + 7), 0, Vector2i(3, 1)) #right tiles
	map.set_cell(0, Vector2i(roomOrigin.x + 17, roomOrigin.y + 8), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x + 17, roomOrigin.y + 9), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x + 17, roomOrigin.y + 10), 0, Vector2i(3, 2))
	
	map.set_cell(0, Vector2i(roomOrigin.x + 7, roomOrigin.y), 0, Vector2i(2, 3)) #top tiles
	map.set_cell(0, Vector2i(roomOrigin.x + 8, roomOrigin.y), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x + 9, roomOrigin.y), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x + 10, roomOrigin.y), 0, Vector2i(0, 3))
	
	map.set_cell(0, Vector2i(roomOrigin.x + 7, roomOrigin.y + 17), 0, Vector2i(2, 3)) #bottom tiles
	map.set_cell(0, Vector2i(roomOrigin.x + 8, roomOrigin.y + 17), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x + 9, roomOrigin.y + 17), 0, Vector2i(1, 1))
	map.set_cell(0, Vector2i(roomOrigin.x + 10, roomOrigin.y + 17), 0, Vector2i(0, 3))
