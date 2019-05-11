return {
	["Player"] = {
		tags = {"Player"},
		persistant = true,
		
		components = {
			["Body"] = {10, 5},
			["Physics"] = {},
			["Animation"] = {
				"Charactors", 
				{"player-run", 0.08}, 
				{"player-idle", 0.1}, 
				{"player-attack", 0.08}, 
				{"player-die", 0.08}, 
				{"player-knife-run", 0.08}, 
				{"player-knife-idle", 0.1}, 
				{"player-knife-attack-1", 0.8},
				{"player-knife-attack-2", 0.8},
				--{"player-smoke", 0.8},
			},
			--["Inventory"] = {3, 3},
			["Player"] = {},
			["Light"] = {}, --Light emiter around character/player
			["Equipment"] = {}, --call Equipment with stats for attach - speed and damage
			["Health"] = {5}, -- how much health the player has, using harts.


			["Character"] = {
				name	= "player",
				age		= 34
			}
		},
	}
}