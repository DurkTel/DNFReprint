local GUIDefine = {}

GUIDefine.UILayer_2D =
{
    MainUILayer         = "MainUILayer",
    ViewLayer           = "ViewLayer",
    WindowLayer         = "WindowLayer",
    AlertLayer          = "AlertLayer",
}

GUIDefine.UILayer_2D_Layer =
{
    {name = GUIDefine.UILayer_2D.MainUILayer, class = 'game.framework.gui.GUILayer'},
    {name = GUIDefine.UILayer_2D.ViewLayer, class = 'game.framework.gui.GUILayer'},
    {name = GUIDefine.UILayer_2D.WindowLayer, class = 'game.framework.gui.GUILayer'},
    {name = GUIDefine.UILayer_2D.AlertLayer, class = 'game.framework.gui.GUILayer'},
}


return GUIDefine