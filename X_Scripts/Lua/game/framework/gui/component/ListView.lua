local base              = require("game.framework.Injection")
local ListItemRenderer  = require("game.framework.gui.component.renderer.ListItemRenderer")
local ListView          = class(base)

function ListView:ctor(gameObject)
    self.items              = {}
    self.cListView          = nil
    self.dataSource         = nil
    self.clickFunction      = nil
    self.selectFunction     = nil
    self.onCompleteFunction = nil
    self.itemRenderer       = nil
    self.totalCount         = 0
    self.selectIndex        = -1
    base.ctor(self, gameObject)
end

function ListView:on_init()
    self.cListView = self:get_inject()
    self.cListView.onItemCreate = function (citem) self:on_create_item_renderer(citem) end
    self.cListView.onItemUpdate = function (citem) self:on_update_item_renderer(citem) end
    self.cListView.onItemRelease = function (citem) self:on_release_item_renderer(citem) end
    self.cListView.onUpdateComplete = function () self:on_update_complete() end

end

function ListView:set_item_renderer(renderer)
    self.itemRenderer = renderer
end

function ListView:on_create_item_renderer(citem)
    local item = self.itemRenderer and self.itemRenderer(citem, self) or ListItemRenderer(citem, self)
    self.items[citem.index + 1] = item
    item:on_create()

end

function ListView:on_update_item_renderer(citem)
    local item = self.items[citem.index + 1]
    if not item then
        print_err("listView没有该下标的item"..citem.index)
        return
    end

    item:on_data()

end

function ListView:on_release_item_renderer(citem)
    local item = self.items[citem.index + 1]
    if not item then
        print_err("listView没有该下标的item"..citem.index)
        return
    end

    item:on_release()
end

function ListView:on_update_complete()
    if self.onCompleteFunction then
        self.onCompleteFunction()
    end
end

function ListView:set_data(data)
    if self.dataSource and self.dataSource == data then
        return
    end

    self.dataSource = data
    self.totalCount = #data

    self.cListView.dataCount = self.totalCount
end

function ListView:get_data_by_index(index)
    return self.dataSource[index]
end

return ListView