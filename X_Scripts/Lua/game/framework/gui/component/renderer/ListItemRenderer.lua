local base              = require("game.framework.Injection")
local ListItemRenderer  = class(base)

function ListItemRenderer:ctor(citem, listView)
	self.citem 			= citem
	self.button			= nil
	self.listView 		= listView
    self.gameObject     = citem.gameObject
    self.rectTransform  = citem.rectTransform

	base.ctor(self, self.citem.gameObject)
end

function ListItemRenderer:get_index()
	return self.citem.index + 1
end

function ListItemRenderer:get_data()
	return self.listView:get_data_by_index(self:get_index())
end

function ListItemRenderer:get_button()
	return self.button
end

function ListItemRenderer:on_create()
    
end

function ListItemRenderer:on_data()
    
end

function ListItemRenderer:on_release()

end

return ListItemRenderer