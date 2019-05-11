


-------------------------------------------------------------------------------
-- Created by: Ayran Olckers AKA The Geekiest One
-- -2019-
-- -Game Development Project-
-- This file is subject to the terms and conditions defined in
-- file 'LICENSE.txt', which is part of this source code package.
-------------------------------------------------------------------------------
 
 --Summary--
 --
 --https://www.pixelcrushers.com/dialogue_system/manual2x/html/logic_and_lua.html
 --
 --No scripting is required in the Dialogue System. However, the Dialogue System does offer a general-purpose scripting language called Lua that provides a very powerful method of controlling the flow of conversations, checking and changing quest states, and more. In most cases, you can use simple point-and-click menus.

--The Dialogue System uses the data model established by Chat Mapper, another professional dialogue authoring tool commonly used in the industry.
--Information about all actors, items, locations, variables, and conversations is stored in Lua tables. You can control conversations by specifying Lua conditions and scripts, 
--typically by using point-and-click menus. For those who are interested, the Chat Mapper manual has more background info about Lua and the tables that the Dialogue System uses, in the section titled Scripting with Lua.




math.randomseed(os.time())

function string:split( inSplitPattern, outResults )
	if not outResults then
		outResults = { }
	end
	local theStart = 1
	local theSplitStart, theSplitEnd = string.find( self, inSplitPattern, theStart )
	while theSplitStart do
		table.insert( outResults, string.sub( self, theStart, theSplitStart-1 ) )
		theStart = theSplitEnd + 1
		theSplitStart, theSplitEnd = string.find( self, inSplitPattern, theStart )
	end
	table.insert( outResults, string.sub( self, theStart ) )
	return outResults
end

function npc_ai(entity, engine)
	local body = engine:Get_Component(entity, "Body") --Animation of body
	local physics = engine:Get_Component(entity, "Physics") -- Collison and
	local animation = engine:Get_Component(entity, "Animation") -- animation 

	--The Npc can wander around, have not implemented this with the enemies yet
	if body and physics then
		engine:Face_Move_Dir(entity)
		
		local anim_string = animation.Current_Animation_ID
		local tokens = anim_string:split "-"
		
		local anim_id = tokens[1] .. "-" .. tokens[2] .. "-"

		if physics.Current_Speed < 0.2 then
			anim_id = anim_id .. "idle"
		else
			anim_id = anim_id .. "walk"
		end
		animation.Current_Animation_ID = anim_id

		local lua = engine:Get_Component(entity, "Lua_Function")
		if lua then
			if lua.Table == nil then
				-- setup the table for timers and stuff
				lua.Table = {walk_timer = 0, target_x = body.X + -50 + math.random() * 100, target_y = body.Y + -50 + math.random() * 100}
			else
				local dist = 200
				local speed = 3
				local buffer = 10
				local time = 6
				
				local dot = body:Angle_To_Other(engine:Vec2(lua.Table.target_x, lua.Table.target_y))
				physics:Apply_Force(speed, dot)

				local function retarget()
					lua.Table.target_x = body.X + (-(dist/2) + math.random() * dist)
					lua.Table.target_y = body.Y + (-(dist/2) + math.random() * dist)
				end

				if engine:Dist(body.X, body.Y, lua.Table.target_x, lua.Table.target_y) < 20 then
					retarget()
				end

				if (lua.Table.walk_timer <= 0) then
					retarget()
					lua.Table.walk_timer = time
				end
				lua.Table.walk_timer = lua.Table.walk_timer - engine:Get_DT()
			end
		end
	end
end

return {


  -- [[ This npc transforms into a wolf when the player talks to them ]]
	["npc-2"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-2-walk", 0.8},{"npc-2-idle", 0.8}},
			["Physics"]={},
			["Npc"] = {"wolf_gui"},
			["Ai"] = {"Function", npc_ai},
		}
	},


}
