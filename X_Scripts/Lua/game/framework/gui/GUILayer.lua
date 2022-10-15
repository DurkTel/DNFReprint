local GUILayer = class()
GUILayer.layer_views = {}

function GUILayer:ctor(gameObject)
	self.gameObject = gameObject
	self.transform  = gameObject.transform
end

function GUILayer:add_view(view)
	view.layer_class = self
	view.transform:SetParentZero(self.transform)
	local ts = view.gameObject:TryAddComponent(typeof(RectTransform))
	view.transform = ts
	ts.anchorMin = Vector2.zero
	ts.anchorMax = Vector2.one
	ts.offsetMin = Vector2.zero
	ts.offsetMax = Vector2.zero
	
	table.insert( self.layer_views, view )
end

return GUILayer