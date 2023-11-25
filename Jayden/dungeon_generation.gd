extends Node

var room = preload("res://Jayden/Room.tscn")

var min_rooms = 6
var max_rooms = 10

var generation_chance = 20

func generate(room_seed):
	seed(room_seed)
	var dungeon = {}
	var size = randi_range(min_rooms, max_rooms)
	var instance = room.instantiate()
	dungeon[Vector2(0,0)] = instance
	size -= 1
	
	# most of this is from a tutorial so it's pretty messy and i had to frankenstein it to work. this will need to be redone in the future
	while(size > 0):
		for i in dungeon.keys():
			if (randi_range(0, 100) < generation_chance) and size > 0:
				var direction = randi_range(0,4)
				if(direction < 1):
					var new_room_position = i + Vector2(1, 0) #to the right
					if (!dungeon.has(new_room_position)):
						dungeon[new_room_position] = instance
						size -= 1
					if(dungeon.get(i).connected_rooms.get(Vector2(1, 0)) == null):
						connect_rooms(dungeon.get(i), dungeon.get(new_room_position), Vector2(1, 0))
				elif(direction < 2):
					var new_room_position = i + Vector2(-1, 0) #to the left
					if (!dungeon.has(new_room_position)):
						dungeon[new_room_position] = instance
						size -= 1
					if(dungeon.get(i).connected_rooms.get(Vector2(-1, 0)) == null):
						connect_rooms(dungeon.get(i), dungeon.get(new_room_position), Vector2(-1, 0))
				elif(direction < 3):
					var new_room_position = i + Vector2(0, 1) #to up
					if (!dungeon.has(new_room_position)):
						dungeon[new_room_position] = instance
						size -= 1
					if(dungeon.get(i).connected_rooms.get(Vector2(0, 1)) == null):
						connect_rooms(dungeon.get(i), dungeon.get(new_room_position), Vector2(0, 1))
				elif(direction < 4):
					var new_room_position = i + Vector2(0, -1) #to down
					if (!dungeon.has(new_room_position)):
						dungeon[new_room_position] = instance
						size -= 1
					if(dungeon.get(i).connected_rooms.get(Vector2(0, -1)) == null):
						connect_rooms(dungeon.get(i), dungeon.get(new_room_position), Vector2(0, -1))
	while(!is_interesting(dungeon)):
		dungeon = generate(room_seed * randi_range(-100, 100) + randi_range(-100, 100))
	return dungeon

func connect_rooms(room1, room2, direction): #room2 relative to room1
	room1.connected_rooms[direction] = room2
	room2.connected_rooms[-direction] = room1
	room1.number_of_connections += 1
	room2.number_of_connections += 1

func is_interesting(dungeon):
	var rooms_with_three = 0
	for i in dungeon.keys():
		if(dungeon.get(i).number_of_connections >= 3):
			rooms_with_three += 1
	return rooms_with_three >= 2
