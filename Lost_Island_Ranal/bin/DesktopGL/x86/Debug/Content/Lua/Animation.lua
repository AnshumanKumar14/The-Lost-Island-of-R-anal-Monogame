-------------------------------------------------------------------------------
-- Created by: Ayran Olckers AKA The Geekiest One
-- -2019-
-- -Game Development Project-
-- This file is subject to the terms and conditions defined in
-- file 'LICENSE.txt', which is part of this source code package.
-------------------------------------------------------------------------------
 
 --Summary--
 --
 -- assign math values to entities
 --
 --

return {
	generate = {
		-- [name] = {sx, sy, w, h, num, ?offset_x, ?offset_y, ?speed, ?left_offset_x, ?right_offset_x}
		--wold // melee
		["wolf-run" ]				= {192, 0, 32, 16, 6, offset_x = 0, offset_y = 0},
		["wolf-idle"]				= {192 + 32, 0, 32, 16, 1},
		--cant interact with
		["bird-idle"]				= {192, 16, 8, 8, 1},
		["bird-fly"]				= {192+8*2, 16, 8, 8, 3},
		--demon/mellee
		["demon-idle"]				= {0, 464, 48, 48, 1},
		["demon-attack"]				= {0, 464, 48, 48, 8},
		--
		["player-idle"]				= {0, 69, 16, 26, 1},
		["player-run"]				= {34, 69, 18, 26, 8},
		["player-attack"]			= {37, 121, 32, 26, 4},
		["player-knife-run"]		= {0, 146, 36, 26, 8,	left_offset_x = 10, right_offset_x = -8},
		["player-knife-idle"]	= {0, 380, 34, 26, 4,	left_offset_x = 10, right_offset_x = -8},
		["player-knife-attack-1"]	= { 0, 988, 72, 40, 13,	right_offset_x = 10, right_offset_y = 14, left_offset_x = -8, left_offset_y = 14, speed = 0.08},
		["player-knife-attack-2"]	= {-8 + 72*6, 988, 72, 40, 7,	right_offset_x = 10, right_offset_y = 14, left_offset_x = -8, left_offset_y = 14, speed = 0.08},
		["player-die"]					= {0, 440, 16, 26, 13, speed = 0.08},

		--range class
		["ranged-attack"]			= {0, 236, 32, 32, 16},
		["ranged-idle"]				= {0, 236, 32, 32, 1},

		--have coin rotate
		["coin-flip"] = {0, 0, 8, 8, 4},

		["flag"] = {0, 268, 24, 24, 4, offset_x = 10, speed = 0.15},

		["blue-flame"] = {0, 164, 16, 16, 13},
		["ranged-bullet"] = {0, 228, 8, 8, 4},

		-- npcs
		["npc-2-idle"] = {0, 224, 16, 26, 1}, --Npc turns into wolf then uses the wold animiation and AI
		["npc-2-walk"] = {0, 224, 16, 26, 9},

	},
	-- TODO: implement explicit frame creation, with seperate speeds and offsets
	
}