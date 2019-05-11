

-------------------------------------------------------------------------------
-- Created by: Ayran Olckers AKA The Geekiest One
-- -2019-
-- -Game Development Project-
-- This file is subject to the terms and conditions defined in
-- file 'LICENSE.txt', which is part of this source code package.
-------------------------------------------------------------------------------
 
 --Summary--
 --
 -- Create functions which will be used by other lua files. 
 --


_G.Eventing = {
    new = function(list)
        return {
            functions = list,
            pointer = 1,
            done = false,

			timer = 0,
			delaying = false,

			delayed_next = function(self, time)
				if not self.delaying then
					self.timer = time
					self.delaying = true
				end
			end,
            
            next = function(self)
                self.pointer = self.pointer + 1

                if self.pointer > #self.functions then
                    self.pointer = 1
                    self.done = true
                end
            end,

			goto_fn = function(self, num)
				self.pointer = num
			end,

            update = function(self, dt)
				if self.timer > 0 then
					self.timer = self.timer - dt
				else
					if self.delaying then
						self:next()
						self.delaying = false
					end
				end
                self.functions[self.pointer](self, dt)
            end
        }
    end
}