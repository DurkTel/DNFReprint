local EventDispatcher = class()

function EventDispatcher:ctor(args)
	self.__eventMap__ = {}
end

function EventDispatcher:clear()
	self.__eventMap__ = {}
end

function EventDispatcher:add_event_listener(name, listener, listenerCaller)
    if type(name) ~= "string" and type(name) ~= "number" then
        print_err("事件名不能是非字符串和number的类型")
        return
    end

    if not self.__eventMap__[name] then
        self.__eventMap__[name] = {}
    end

    if next(self.__eventMap__[name]) then
        for i,v in ipairs(self.__eventMap__[name]) do
            if v.listener == listener and v.listenerCaller == listenerCaller then
                return
            end 
        end
    end

    local newTab = 
    {
        listener = listener,
        listenerCaller = listenerCaller
    }
    
    table.insert(self.__eventMap__[name], newTab)
end

function EventDispatcher:remove_event_listener(name, listener, listenerCaller)
    if type(name) ~= "string" and type(name) ~= "number" then
        print_err("事件名不能是非字符串和number的类型")
        return
    end

    if not self.__eventMap__[name] then return end

    for i = #self.__eventMap__[name], 1, -1 do
        if self.__eventMap__[i].listener == listener and self.__eventMap__[i].listenerCaller == listenerCaller then
            table.remove(self.__eventMap__, i)
        end
    end
end

function EventDispatcher:dispatch_event(name, ...)
    if type(name) ~= "string" and type(name) ~= "number" then
        print_err("事件名不能是非字符串和number的类型")
        return
    end
    
    if not self.__eventMap__[name] or not next(self.__eventMap__[name]) then return end

    for i,v in ipairs(self.__eventMap__[name]) do
        v.listener(v.listenerCaller, name, ...)
    end

end

_G.Dispatcher = {}
local _dispatcher = EventDispatcher()

function Dispatcher.addEventListener(name, listener, listenerCaller)
    _dispatcher:add_event_listener(name, listener, listenerCaller)
end

function Dispatcher.removeEventListener(name, listener, listenerCaller)
    _dispatcher:remove_event_listener(name, listener, listenerCaller)
end

function Dispatcher.dispatchEvent(name, ...)
    _dispatcher:dispatch_event(name, ...)
end


return EventDispatcher