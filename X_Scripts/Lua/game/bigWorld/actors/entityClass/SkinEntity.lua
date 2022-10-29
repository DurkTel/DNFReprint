--场景上所有有皮肤外观的实体 都由此派生

local EntitySkinUtility = require("game.bigWorld.utility.EntitySkinUtility")
local EntityAnimatorUtility = require("game.bigWorld.utility.EntityAnimatorUtility")
local avatarPartType = GEntityDefine.avatarPartType

local base = require("game.bigWorld.actors.entityClass.GameEntity")
local SkinEntity = class(base)


function SkinEntity:on_init()
    base.on_init(self)
    self.skinInitialized = nil
    self.skinIsComplete = nil
end

function SkinEntity:dispose()
    base.dispose(self)
    self.skinInitialized = nil
    self.skinIsComplete = nil
end

function SkinEntity:on_create()
    self:init_skin()
    self.skinInitialized = true
end

function SkinEntity:on_avatar_loadComplete()
    base.on_avatar_loadComplete(self)
    self.skinIsComplete = true
    self:set_sort()
    self:init_animator()
    self:play_default_animation()
end

function SkinEntity:set_sort()
    self:enable_sort_sprite(0.5)
end

function SkinEntity:play_default_animation()
    self:play_sprite_animation("IDLE_TOWN_ANIM")
end

function SkinEntity:init_animator()
    local cfgAssetName = EntityAnimatorUtility.get_animatorCfg(self.entityData.etype)
    self:add_entityAnimator(cfgAssetName)
end

function SkinEntity:init_skin()
    self:refresh_allskin()
end

function SkinEntity:refresh_allskin()
    self:refresh_skeleton()
    self:refresh_skin_body()
    self:refresh_skin_shirt()
    self:refresh_skin_weapon()
    self:refresh_skin_pants()
    self:refresh_skin_shoes()
    self:refresh_skin_hair()
end

--刷新骨骼
function SkinEntity:refresh_skeleton()
    local entityType = self.entityData:get_entityType()
    if not entityType then
        -- print_err("")
        return
    end
    local skeletonAssetName = EntitySkinUtility.get_skeleton_assetName(entityType) or "common_single_bone.prefab"
    self:set_avatarSkeleton(skeletonAssetName)
end


--刷新身体皮肤
function SkinEntity:refresh_skin_body()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.body)
    self:set_avatarPart(avatarPartType.body, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.body, scale)
    self:set_avatarPartSort(avatarPartType.body, sort)
end

--刷新上衣皮肤
function SkinEntity:refresh_skin_shirt()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.shirt)
    self:set_avatarPart(avatarPartType.shirt, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.shirt, scale)
    self:set_avatarPartSort(avatarPartType.shirt, sort)
end

--刷新武器皮肤
function SkinEntity:refresh_skin_weapon()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.weapon)
    self:set_avatarPart(avatarPartType.weapon, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.weapon, scale)
    self:set_avatarPartSort(avatarPartType.weapon, sort)

    assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.weaponEx)
    self:set_avatarPart(avatarPartType.weaponEx, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.weaponEx, scale)
    self:set_avatarPartSort(avatarPartType.weaponEx, sort)
end

--刷新裤子皮肤
function SkinEntity:refresh_skin_pants()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.pants)
    self:set_avatarPart(avatarPartType.pants, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.pants, scale)
    self:set_avatarPartSort(avatarPartType.pants, sort)

    assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.pantsEx)
    self:set_avatarPart(avatarPartType.pantsEx, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.pantsEx, scale)
    self:set_avatarPartSort(avatarPartType.pantsEx, sort)
end

--刷新鞋子皮肤
function SkinEntity:refresh_skin_shoes()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.shoes)
    self:set_avatarPart(avatarPartType.shoes, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.shoes, scale)
    self:set_avatarPartSort(avatarPartType.shoes, sort)

    assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.shoesEx)
    self:set_avatarPart(avatarPartType.shoesEx, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.shoesEx, scale)
    self:set_avatarPartSort(avatarPartType.shoesEx, sort)
end

--刷新头发皮肤
function SkinEntity:refresh_skin_hair()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, avatarPartType.hair)
    self:set_avatarPart(avatarPartType.hair, assetName, boneName)
    self:set_avatarPartScale(avatarPartType.hair, scale)
    self:set_avatarPartSort(avatarPartType.hair, sort)
end


return SkinEntity