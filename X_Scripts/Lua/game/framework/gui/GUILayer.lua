local GUILayer = class()
GUILayer.layer_views = {}

local order_spacing = 50

function GUILayer:ctor(gameObject)
	self.gameObject = gameObject
	self.transform  = gameObject.transform
end

function GUILayer:set_order_range(min, max)
	self.min_sorting_order = min
	self.max_sorting_order = max
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

	self:init_sorting_order(view)
	table.insert( self.layer_views, view )
end

function GUILayer:remove_view(view)
	for i = #self.layer_views, 1, -1 do
		if self.layer_views[i] == view then
			view:on_dispose()
			table.remove(self.layer_views, i)
			GUIManager.remove_view(view.name)
			CS.UnityEngine.Object.Destroy(view.gameObject)
			return
		end
	end
end

function GUILayer:init_sorting_order(view)
	local lastOrder = self:get_last_view_order()
	local newOrder = lastOrder + order_spacing
	if newOrder > self.max_sorting_order then
		for i, v in ipairs(self.layer_views) do
			newOrder = (i - 1) * order_spacing + self.min_sorting_order
			v.gameObject:AddCanvas(newOrder)
			v.sorting_order = newOrder
			v.transform:SetAsLastSibling()
		end
		newOrder = newOrder + order_spacing
	end

	view.gameObject:AddCanvas(newOrder)
	view.sorting_order = newOrder
	view.transform:SetAsLastSibling()
end

function GUILayer:get_last_view_order()
	local allNum = #self.layer_views
	if allNum > 0 then
		return self.layer_views[allNum].sorting_order
	end

	return self.min_sorting_order + 1
end

return GUILayer