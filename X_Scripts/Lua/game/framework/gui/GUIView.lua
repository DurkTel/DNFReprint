local base              = require("game.framework.Injection")
local GUIDefine         = require('game.framework.gui.GUIDefine')
local viewfunc 		    = require('game.framework.gui.func.Viewfunc')

local GUIView           = class(base)
GUIView.prefab 		    = 'GUI_Default_View'
GUIView.layer           = GUIDefine.UILayer_2D.ViewLayer
GUIView.tween_func	    = nil

local function view_life_log(self, content)
    print(string.format('GUIView:viewlog.  name = %s  %s  ',self.name,content))
    
end  

function GUIView:ctor(gameObject)
    view_life_log(self, "GUIView:ctor")
    self.is_opening     = nil
    self.is_closeing    = nil
    self.is_enabled     = nil
    base.ctor(self, gameObject)
end

function GUIView:open()
    view_life_log(self, "GUIView:open")
    if self.is_opening and not self.is_enabled then return end
    self.is_opening = true

    viewfunc.load_main_ui(self)
end

function GUIView:on_initView()
    view_life_log(self, "GUIView:on_initView")

end

function GUIView:on_open_refresh()
    view_life_log(self, "GUIView:on_open_refresh")

end

function GUIView:on_open_effect()
    view_life_log(self, "GUIView:on_open_effect")
    if self.tween_func then
        self.tween_func(true, function ()
            self:on_enabled()
        end)
    else
        self:on_enabled()
    end
end

function GUIView:on_enabled()
    view_life_log(self, "GUIView:on_enabled")
    self.is_enabled = true
end

function GUIView:on_disable()
    view_life_log(self, "GUIView:on_disable")
    self.is_closeing = true
    if self.tween_func then
        self.tween_func(false, function ()
            
        end)
    end
end

function GUIView:on_dispose()
    view_life_log(self, "GUIView:on_dispose")

end

return GUIView