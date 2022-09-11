local EntityDefine = {} _G.GEntityDefine = EntityDefine

EntityDefine.AvatarPartType =
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

EntityDefine.EntityType = 
{
    localPlayer = 0,      --本机玩家
    otherPlayer = 1,      --其他玩家
    monster     = 2,      --怪物
    robot       = 3,      --机器人
    npc         = 4,      --npc
    portal      = 4,      --传送门
}

EntityDefine.Status = 
{
    peace       = 0,      --和平
    fight       = 1,      --战斗
}

EntityDefine.AIStateType = 
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

EntityDefine.AIStateClass = 
{
    [EntityDefine.AIStateType.idle] = "game.bigWorld.actors.fsm.FSM_Idle",
    [EntityDefine.AIStateType.move] = "game.bigWorld.actors.fsm.FSM_Move",
    [EntityDefine.AIStateType.aciton] = "game.bigWorld.actors.fsm.FSM_Action",
    [EntityDefine.AIStateType.jump] = "game.bigWorld.actors.fsm.FSM_Jump",
    [EntityDefine.AIStateType.hurt] = "game.bigWorld.actors.fsm.FSM_Hurt",
    [EntityDefine.AIStateType.combat] = "game.bigWorld.actors.fsm.FSM_Combat",
    [EntityDefine.AIStateType.death] = "game.bigWorld.actors.fsm.FSM_Death",
    [EntityDefine.AIStateType.sleep] = "game.bigWorld.actors.fsm.FSM_Sleep",
    [EntityDefine.AIStateType.born] = "game.bigWorld.actors.fsm.FSM_Born"
}


return EntityDefine