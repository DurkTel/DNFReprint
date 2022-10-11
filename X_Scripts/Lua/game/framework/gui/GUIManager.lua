local GUIManager = {}
_G.GUIManager = GUIManager

function GUIManager.openView(viewName, viewData)
	local viewClass = require( "game.test" )
    GLoaderfunc.load_object_fromPool("prefabs/gui/"..viewClass.prefab, GLoaderfunc.game_poolType.viewPrefab, function (go)
        local view = viewClass(go)
        view:on_initView()
    end)
end


return GUIManager
