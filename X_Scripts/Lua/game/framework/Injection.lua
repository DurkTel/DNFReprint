local Injection 			= {}
local Timerfunc 			= require("game.framework.func.Timerfunc")

Injection.add_timer 		= Timerfunc.add_timer
Injection.add_framer 		= Timerfunc.add_framer
Injection.wait_timer 		= Timerfunc.wait_timer
Injection.reset_timer 		= Timerfunc.reset_timer
Injection.del_timer 		= Timerfunc.del_timer
Injection.del_all_timer 	= Timerfunc.del_all_timer

local function auto_inject(self)
	return setmetatable({}, 
	{
		__index = function (table, key)
			local value = self:get_component(key)
			if not value and not value:IsNull() then 
				print(string.format( '<color=red>viewName="%s". createAutoInject component is nil name="%s"  预置件身上没有此名字的对象! </color> \n%s',self.name,key,debug.traceback()))
				return
			end

			rawset(table, key, value)
			return value
		end
	})
end


function Injection:ctor(gameObject)
	assert( gameObject, 'View:ctor. gameObject is nil ' )
	self.gameObject = gameObject
	self.transform  = gameObject.transform
	self.inject 	= auto_inject(self)
	self:on_init()
end

function Injection:on_init()

end

function Injection:get_component(name)
	local cinject = self:get_inject()
	return cinject and cinject:Get(name) or false
end

function Injection:get_inject()
	if self.cInjection == nil then

		if self.mainObject then
			self.cInjection = self.mainObject:GetComponent(typeof(CS.Injection))
		else
			self.cInjection = self.gameObject:GetComponent(typeof(CS.Injection))
		end


		if self.cInjection then
			self.cInjection:InitInject(self.inject)
		end

	end

	if not self.cInjection then print("<color=#red>View:get_cview. cInjection is nil</color>",debug.traceback(  )) end

	return self.cInjection
end

function Injection:on_dispose()
	self:del_all_timer()
	self.inject 	= nil
	self.cInjection	= nil
	self.gameObject = nil
	self.transform  = nil
end

return Injection