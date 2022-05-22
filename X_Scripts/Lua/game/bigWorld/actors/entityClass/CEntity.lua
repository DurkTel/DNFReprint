--主要实现与C#的交互接口

local CEntity = class()
CEntity.cgmentity = nil
CEntity.cguid = nil


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

function CEntity:onCreateEvent() end

function CEntity:onAvatarLoadComplete() end


local extensions = 
{
    "game.bigWorld.manager.func.CEntityAvatarfunc",
    "game.bigWorld.manager.func.CEntityfunc"
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