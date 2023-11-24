extends TileMap

var width = 10
var height = 10

func _ready():
	generate_layout()


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func generate_layout():
	var tile_pos = local_to_map(position) #converts position passed into tilemap coords
	
