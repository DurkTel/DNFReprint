local EntityDefine = {}

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


return EntityDefine