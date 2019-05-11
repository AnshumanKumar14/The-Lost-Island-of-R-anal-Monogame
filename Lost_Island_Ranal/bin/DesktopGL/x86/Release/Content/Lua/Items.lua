
-------------------------------------------------------------------------------
-- Created by: Ayran Olckers AKA The Geekiest One
-- -2019-
-- -Game Development Project-
-- This file is subject to the terms and conditions defined in
-- file 'LICENSE.txt', which is part of this source code package.
-------------------------------------------------------------------------------
 
 --Summary--
 --
 --
 --
 --



function simple_item(name, quad)
	return {
		tags = {name, "Item", "Heal"},
		components = {
			["Body"] = {4, 2},
			["Physics"] = { blacklist = {"Item"} },
			["Sprite"] = {
				"items",
				quad[1], quad[2], quad[3], quad[4],
			},
			["Item"] = {}
		}
	}
end


return {

--Produces tokens once player gets a kill
	["Coin"] = {
		tags = {"Coin", "Item"},
		components = {
			["Body"] = {4, 2},
			["Physics"] = { blacklist = {"Item"} },
			["Animation"] = {
				"items",
				{"coin-flip", 0.08},
			},

			["Item"] = {}
		}
	},

}