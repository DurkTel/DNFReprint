local EntityDefine = {} _G.GEntityDefine = EntityDefine

EntityDefine.avatarPartType =
{
    body		= 0,      --身体
    shirt		= 1,      --上衣
    weapon		= 2,      --武器
    weaponEx    = 3,      --武器额外
    hair	 	= 4,      --头发
    pants	 	= 5,      --裤子
    pantsEx 	= 6,      --裤子额外
    shoes	 	= 7,      --鞋子
    shoesEx 	= 8,      --鞋子额外
}

EntityDefine.entityType = 
{
    localPlayer = 0,      --本机玩家
    otherPlayer = 1,      --其他玩家
    monster     = 2,      --怪物
    robot       = 3,      --机器人
    npc         = 4,      --npc
    portal      = 4,      --传送门
    effect      = 5,      --特效
}

EntityDefine.career = 
{
    swordMan = 0,      --男剑
}

EntityDefine.status = 
{
    peace       = 0,      --和平
    fight       = 1,      --战斗
}

EntityDefine.ai_stateType = 
{
    idle = 1,
    move = 2,
    aciton = 3,
    jump = 4,
    hurt = 5,
    combat = 6,
    death = 7,
    sleep = 8,
    born = 9
}

EntityDefine.ai_stateClass = 
{
    [EntityDefine.ai_stateType.idle] = "game.bigWorld.actors.fsm.FSM_Idle",
    [EntityDefine.ai_stateType.move] = "game.bigWorld.actors.fsm.FSM_Move",
    [EntityDefine.ai_stateType.aciton] = "game.bigWorld.actors.fsm.FSM_Action",
    [EntityDefine.ai_stateType.jump] = "game.bigWorld.actors.fsm.FSM_Jump",
    [EntityDefine.ai_stateType.hurt] = "game.bigWorld.actors.fsm.FSM_Hurt",
    [EntityDefine.ai_stateType.combat] = "game.bigWorld.actors.fsm.FSM_Combat",
    [EntityDefine.ai_stateType.death] = "game.bigWorld.actors.fsm.FSM_Death",
    [EntityDefine.ai_stateType.sleep] = "game.bigWorld.actors.fsm.FSM_Sleep",
    [EntityDefine.ai_stateType.born] = "game.bigWorld.actors.fsm.FSM_Born"
}

EntityDefine.entity_attribute = 
{
    attribute_life = 1,                         --生命值更新
    attribute_magic = 2,                        --法力值更新
    attribute_move_seed = 3,                    --移动速度更新
    attribute_jump_height = 4,                  --跳跃高度更新
}

return EntityDefine