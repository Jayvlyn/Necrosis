extends Node2D

#this stores all the ways a room can connect
var connected_rooms = {
	Vector2(1, 0): null, #right
	Vector2(-1, 0): null, #left
	Vector2(0, 1): null, #up
	Vector2(0, -1): null #down
}

var number_of_connections = 0
