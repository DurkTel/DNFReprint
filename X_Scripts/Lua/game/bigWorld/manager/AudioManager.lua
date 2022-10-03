local AudioManager = {} _G.GAudioManager = AudioManager

local CGMAudioManager = CS.GMAudioManager

local audioRegister = 
{
    ["TalkAudio"] = {isLoop = false, playMode = 0},
    ["CityLoopAudio"] = {isLoop = true, playMode = 0},
    ["CopyLoopAudio"] = {isLoop = true, playMode = 0},
    ["HitAudio"] = {isLoop = false, playMode = 2},
    ["HurtAudio"] = {isLoop = false, playMode = 2},
    ["OtherAudio"] = {isLoop = false, playMode = 2},
    ["UiLoopAudio"] = {isLoop = true, playMode = 0},
    ["UiEffectAudio"] = {isLoop = false, playMode = 2},
}

local audioGroupMap = {}

local function Init()
    local audioGroups = CGMAudioManager.instance.audioMixerGroups

    for k, v in pairs(audioGroups) do
        if audioRegister[k] then
            local go = GameObject(string.format("[%s]", v.name))
            go.transform:SetParent(CGMAudioManager.instance.transform)
            local audioGroup = go:AddComponent(typeof(CS.AudioGroup));
            audioGroup.isLoop = audioRegister[k].isLoop
            audioGroup.playMode = audioRegister[k].playMode
            audioGroup.audioMixerGroup = v
            audioGroupMap[k] = audioGroup
            CGMAudioManager.instance.audioGroups:Add(v.name, audioGroup)
        end
    end
end

Init()

function AudioManager.play(groupName, assetName)
    local group = audioGroupMap[groupName]
    if not group then return end
    group:Play(assetName)
end

function AudioManager.play_bg_loop_city(assetName)
    if string.isEmptyOrNull(assetName) then return end
    AudioManager.play("CityLoopAudio", assetName)
end

function AudioManager.play_hit(assetName)
    if string.isEmptyOrNull(assetName) then return end
    AudioManager.play("HitAudio", assetName)
end

function AudioManager.play_hurt(assetName)
    if string.isEmptyOrNull(assetName) then return end
    AudioManager.play("HurtAudio", assetName)
end

return AudioManager