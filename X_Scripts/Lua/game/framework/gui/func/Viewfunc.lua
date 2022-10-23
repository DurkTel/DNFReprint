
local function on_main_ui_load_complete(self, go)
    self.mainObject = go
    self.mainTransform = self.mainObject.transform
    self.mainTransform:SetParentZero(self.transform)

    self:on_initView()

    self:on_open_refresh()

    self:on_open_effect()
end

local function load_main_ui(self)
    if not self.mainObject then
        
        GLoaderfunc.load_ui_prefab(self.prefab, function (go)
            on_main_ui_load_complete(self, go)
        end)
    else
        self:on_open_refresh()

        if not self.is_enabled or self.is_closeing then
            self:on_open_effect()
        else
            self:on_enabled()
        end
    end

end


return 
{
    load_main_ui = load_main_ui
}