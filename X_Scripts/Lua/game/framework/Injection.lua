local Injection 			= {}
local Timerfunc 			= require("game.framework.func.Timerfunc")

Injection.add_timer 		= Timerfunc.add_timer
Injection.add_framer 		= Timerfunc.add_framer
Injection.wait_timer 		= Timerfunc.wait_timer
Injection.reset_timer 		= Timerfunc.reset_timer
Injection.del_timer 		= Timerfunc.del_timer
Injection.del_all_timer 	= Timerfunc.del_all_timer

function Injection:ctor(gameObject)
	assert( gameObject, 'View:ctor. gameObject is nil ' )
	self.inject 	= {}
	self.gameObject = gameObject
	self.transform  = gameObject.transform
	self.cInjection = self:get_inject()
	-- self:on_init()
end

function Injection:get_inject()
	if self.cInjection == nil then

        self.cInjection = self.gameObject:GetComponent(typeof(CS.Injection))

		if self.cInjection then
			self.cInjection:InitInject(self.inject)
		end
	end

	if not self.cInjection then print("<color=#red>View:get_cview. cInjection is nil</color>",debug.traceback(  )) end

	return self.cInjection
end

return Injection