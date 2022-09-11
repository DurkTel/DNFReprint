
/// <summary>
///事件枚举 
/// </summary>
///事件名称统一用大写规范 

public enum EventDefine
{
    NONE,
    EVENT_ADD_JUMP_FORCE,                                                          //添加跳跃力
    EVENT_AIR_ATTACK_COMBO,                                                        //空中连击次数
    EVENT_SKILL_COMBO,                                                             //技能连击数（暂无用）
    EVENT_MOVE_COEFFICIENT_CHANGE,                                                 //改变移动速度系数
    EVENT_ADD_MOVEX_FORCE,                                                         //添加X轴移动力
    EVENT_ADD_MOVEY_FORCE,                                                         //添加Y轴移动力
    EVENT_REST_MOVE_SPEED,                                                         //重置移动速度
    EVENT_PLAY_SOUND,                                                              //播放音效
    EVENT_RESET_MOVEPHASE,                                                         //重置移动阶段
}
