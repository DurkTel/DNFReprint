local base              = require("game.framework.gui.GUIView")
local ListView          = require("game.framework.gui.component.ListView")
local ListItemRenderer  = require("game.framework.gui.component.renderer.ListItemRenderer")
local CreateRoleView    = class(base)

CreateRoleView.prefab 	= 'GUI_CreateRole_View'

local roleRenderer = class(ListItemRenderer)
function roleRenderer:on_create()
    self.button = self.injec.Button
end

function roleRenderer:on_data()
    local data = self:get_data()
    self.inject.Icon:SetSpriteAsync("gui/module/createRole/Images/roleIcon/role_head_portrait_"..data)
end

function CreateRoleView:on_initView()
    self.roleListView = ListView(self.inject.roleListView)
    self.roleListView:set_item_renderer(roleRenderer)

    self.inject.closeBtn.onClick:AddListener(function ()
        self:close()
    end)
end

function CreateRoleView:on_open_refresh()
    local tab = {}
    for i = 1, 6 do
        table.insert(tab, i)
    end
    self.roleListView:set_data(tab)

    self:on_tween_live()
end

function CreateRoleView:on_disable()
    self.del_timer(self.liveTimer)
end

function CreateRoleView:on_tween_live()
    local flag = 0
    self.liveTimer = self:add_timer(function ()
        self.inject.live2d:SetSprite("gui/module/createRole/Images/Single/live2d/role_live2d_1_"..flag)
        flag = flag >= 90 and 0 or flag + 1
    end, 0.1, -1)
end

return CreateRoleView