local GUIDefine             = require('game.framework.gui.GUIDefine')
local ModuleRegister        = require("game.framework.ModuleRegister")
local GUIManager            = {} _G.GUIManager               = GUIManager

local cgui                  = CS.GMGUIManager.Instance

GUIManager.ui_camera        = cgui.uiCamera     --ui相机
GUIManager.offset_order     = 1000              --每个层级相隔
GUIManager.ui_layers        = {}                --所有的层级
GUIManager.ui_views         = {}                --所有的视图

--初始化层级
function GUIManager.init_layer()
    for i, v in ipairs(GUIDefine.UILayer_2D_Layer) do
        local lastOrder = (i - 1) * GUIManager.offset_order
        local layerName = string.format("%s [%s-%s]", v.name, lastOrder, lastOrder + GUIManager.offset_order)
        local canvas = cgui:InitLayer(layerName, lastOrder)
        GUIManager.ui_layers[v.name] = require(v.class)(canvas.gameObject)
    end
end

--创建视图
local function create_view(viewName)
    local viewPath = ModuleRegister.get_view_by_name(viewName)
    if not viewPath then
        print_err("该视图未在模块中定义         "..viewName)
        return
    end

    local viewClass = require(viewPath)

    local layer = GUIManager.ui_layers[viewClass.layer]
    if not layer then
        print_err("该视图定义的层不存在         "..viewClass.layer)
        return
    end

    --创建容器
    local go = GameObject(viewName..' Container')
    local view = viewClass(go)
    view.name = viewName
    layer:add_view(view)

    return view
end

function GUIManager.open_view(viewName, viewData)
	local view = GUIManager.get_view(viewName)
    if not view then --销毁后首次打开
        view = create_view(viewName)
        GUIManager.ui_views[viewName] = view
    else

    end

    view.viewData = viewData
    view:open()

    return view
end

function GUIManager.get_view(viewName)
    return GUIManager.ui_views[viewName]
end


return GUIManager
