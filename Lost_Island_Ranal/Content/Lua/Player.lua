

-------------------------------------------------------------------------------
-- Created by: Ayran Olckers AKA The Geekiest One
-- -2019-
-- -Game Development Project-
-- This file is subject to the terms and conditions defined in
-- file 'LICENSE.txt', which is part of this source code package.
-------------------------------------------------------------------------------
 
 --Summary--
 --
 --create player animation parts // segment the player into parts
 -- this can hel;p with future plans for inventory and change weapons





return {
	["Player"] = {
		tags = {"Player"},
		persistant = true,
		
		components = {
			["Body"] = {10, 5},
			["Physics"] = {},
			["Animation"] = { --assign values to ami,ation parts
				"Charactors", 
				{"player-run", 0.08}, 
				{"player-idle", 0.1}, 
				{"player-attack", 0.08}, 
				{"player-die", 0.08}, 
				{"player-knife-run", 0.08}, 
				{"player-knife-idle", 0.1}, 
				{"player-knife-attack-1", 0.8},
				{"player-knife-attack-2", 0.8},
			},
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