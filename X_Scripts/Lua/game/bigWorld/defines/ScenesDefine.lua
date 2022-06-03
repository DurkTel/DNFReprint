local ScenesDefine = {}

ScenesDefine.mapType = 
{
    unique = 0,
    test = 1,
}


ScenesDefine.copyLogin = 
{
    [ScenesDefine.mapType.test] = "game.bigWorld.modules.copy.copyLogin.TestCopy"
}


return ScenesDefine