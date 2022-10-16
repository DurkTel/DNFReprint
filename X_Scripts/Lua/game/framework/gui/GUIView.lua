local base              = require("game.framework.Injection")
local GUIDefine         = require('game.framework.gui.GUIDefine')
local viewfunc 		    = require('game.framework.gui.func.Viewfunc')

local GUIView           = class(base)
GUIView.prefab 		    = 'GUI_Default_View'
GUIView.layer           = GUIDefine.UILayer_2D.ViewLayer
GUIView.tween_func	    = nil
GUIView.delay_destroy	= 5

local function view_life_log(self, content)
    -- print(string.format('GUIView:viewlog.  name = %s  %s  ',self.name,content))
    
end  

function GUIView:ctor(gameObject)
    view_life_log(self, "GUIView:ctor")
    self.is_open        = nil
    self.is_closeing    = nil
    self.is_enabled     = nil
    base.ctor(self, gameObject)
end

function GUIView:open()
    view_life_log(self, "GUIView:open")
    if self.delayDestroyTimer then self:del_timer(self.delayDestroyTimer) self.delayDestroyTimer = nil end
    if self.is_open and not self.is_enabled then return end
    self.is_open = true

    self:set_visible(true)

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

function GUIView:on_close_effect()
    view_life_log(self, "GUIView:on_close_effect")
    if self.tween_func then
        self.tween_func(false, function ()
            self:internal_close()
        end)
    else
        self:internal_close()
    end
end

function GUIView:on_disable()
    view_life_log(self, "GUIView:on_disable")
    
end

function GUIView:on_dispose()
    view_life_log(self, "GUIView:on_dispose")

end

function GUIView:close()
    view_life_log(self, "GUIView:close")
    if not self.is_open then return end

    self.is_open = false
    self.is_closeing = true

    if self.is_enabled then --视图是加载完成的
        self:on_close_effect()
    else
        self:internal_close()
    end
end

function GUIView:internal_close()
    view_life_log(self, "GUIView:internal_close")
    self.is_closeing = nil
    if self.is_enabled then
        self.is_enabled = nil
        self:on_disable()

        self:set_visible(false)

        self.delayDestroyTimer = self:add_timer(function ()
            self:on_destroy()
            if self.delayDestroyTimer then self:del_timer(self.delayDestroyTimer) self.delayDestroyTimer = nil end
        end, self.delay_destroy)
    end
end

function GUIView:on_destroy()
    view_life_log(self, "GUIView:on_destroy")
    self.layer_class:remove_view(self)
end

function GUIView:set_visible(bool)
    view_life_log(self, "GUIView:set_visible")
    if self.transform then 
        local pos = bool and Vector3(0,0,0) or Vector3(0,-5000,0)
        self.transform.localPosition = pos
	end
end

return GUIView