# AI Script in LUA

```lua
local function handle_player_hit(entity, engine, dammage, power)
	local physics = engine:Get_Component(entity, "Physics")
	local body = engine:Get_Component(entity, "Body")
	if physics and physics.Other then
			local other = physics.Other
			if other:Has_Tag("Player-Hit") then
				local o_body = engine:Get_Component(other, "Body")
				local health = engine:Get_Component(entity, "Health")
				if health then
					-- take dammage according to the weapons dammage
					health:Hurt(dammage)
					physics.Other:Destroy()

					local camera = engine:Get_Camera()
					camera:Shake(5, 0.1);

					local sprite = engine:Get_Component(entity, "Animation")
					sprite.Flash_Timer = 0.1

					local side = (o_body.X > body.X) and -1 or 1
					
					physics.Vel_X = (power or 10) * side 

					if health.Should_Die then
						entity:Destroy()
					end
				end

			end
		end
end
```
