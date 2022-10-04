--技能实体
local EntitySkinUtility = require("game.bigWorld.utility.EntitySkinUtility")
local avatarPartType = GEntityDefine.avatarPartType

local base = require("game.bigWorld.actors.entityClass.SkinEntity")
local SkillEntity = class(base)

function SkillEntity:on_avatar_loadComplete()
    base.on_avatar_loadComplete(self)
    local masterFlip = self.entityData.totalInfo.master:get_flip()
    self:set_flip(masterFlip ~= 1)
    local pos = self.entityData.dbcfg.bornPos
    local offset = self.entityData.dbcfg.offsetPos
    self:set_avatarPosition(Vector3(pos.x + (offset.x * masterFlip), pos.y + offset.y, pos.z))
end

function SkillEntity:play_default_animation()
    local skillAniName = self.entityData.dbcfg.animation
    self:play_sprite_animation(skillAniName, nil, function ()
        GEntityManager.release_entity(self.entityData.entityId)
    end)
end

function SkillEntity:set_sort()
    
end

function SkillEntity:refresh_skin_weapon()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_effectPart_Cfg(self.entityData, avatarPartType.weapon)
    self:set_avatarPart(avatarPartType.weapon, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.weapon, scale)
    self:set_avatarPartSort(avatarPartType.weapon, sort)

    assetName, boneName, scale, sort = EntitySkinUtility.get_effectPart_Cfg(self.entityData, avatarPartType.weaponEx)
    self:set_avatarPart(avatarPartType.weaponEx, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.weaponEx, scale)
    self:set_avatarPartSort(avatarPartType.weaponEx, sort)
end

return SkillEntity