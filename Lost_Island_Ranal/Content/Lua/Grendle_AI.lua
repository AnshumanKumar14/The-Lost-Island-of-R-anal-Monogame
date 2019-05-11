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

assert(require "Content/Lua/AI")
assert(require "Content/Lua/Utilities")

local 
function Load_Grendle(entity, engine)

end

function Grendle_AI(entity, engine)
    local mx_dist           = 100
    local mx_attack_time    = 8
    local body              = entity:Get "Body"
    local anim              = entity:Get "Advanced_Animation"
    local physics           = entity:Get "Physics"
    local fn                = entity:Get "Lua_Function"
    
    -- face the current moving direction
    engine:Face_Move_Dir(entity)

end