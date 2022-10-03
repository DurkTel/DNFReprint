--主要实现与C#的交互接口

local CEntity = class()
CEntity.gmentity = nil
CEntity.cguid = nil

--[[
    ctor
        ⬇
    init_data
        ⬇
    on_init
        ⬇
    set_hotRadius
        ⬇
    on_create
        ⬇
    init_skin
        ⬇
    on_avatar_loadComplete
]]


function CEntity:ctor() end

--与C#实体绑定
function CEntity:set_gmentity(gmentity)
    self.gmentity = gmentity
    self.cguid = gmentity.entityId
end

function CEntity:dispose()
    self.gmentity = nil
    self.cguid = nil
end

function CEntity:get_gameObject()
    return self.gmentity and self.gmentity.gameObject or nil
end

function CEntity:get_transform()
    return self.gmentity and self.gmentity.transform or nil
end

function CEntity:get_position()
    return self.gmentity and self.gmentity.transform and self.gmentity.transform.position or nil
end

function CEntity:get_local_position()
    return self.gmentity and self.gmentity.transform and self.gmentity.transform.localPosition or nil
end

function CEntity:get_flip()
    return self.gmentity and self.gmentity.curFlip or nil
end


function CEntity:on_create() end

function CEntity:on_avatar_loadComplete() end


local extensions = 
{
    "game.bigWorld.manager.func.CEntityAvatarfunc",
    "game.bigWorld.manager.func.CEntityfunc",
    "game.bigWorld.manager.func.CEntityFightfunc"
}

local function initExtensionsfuns()
    local tab = nil
    for _, v in pairs(extensions) do
        tab = require(v)
        for fname, func in pairs(tab) do
            if not CEntity[fname] then
                CEntity[fname] = func
            else
                print_err(string.format('CEntity.initExtensionsfuns 在CEntity中已经存在方法  %s',fname))
            end
        end
    end
end

initExtensionsfuns()

return CEntity