local Timerfunc = {}
_G.Timerfunc = Timerfunc

local timerId = 0
local timerMap = {}
local removeMap = {}

local function get_timerId()
    timerId = timerId + 1
    return timerId
end

function Timerfunc.add_timer(self, func, interval, duration, delay)
    local timer = {}
    timer.guid = get_timerId()
    local timerId = timer.guid

    timer.self = self
    timer.func = func
    timer.interval = interval or 1
    timer.duration = duration or 1
    timer.times = 0
    timer.delay = delay or 0
    timer.nextTime = timer.delay > 0 and Time.time + timer.delay  or Time.time + timer.interval

    if self then
        if not self.__timers then
            self.__timers = {}
        end
        self.__timers[timerId] = timerId
    end

    timerMap[timerId] = timer

    return timerId
end

function Timerfunc.add_framer(self, func, interval, duration, delay)
    --一帧0.033s
    return Timerfunc.add_timer(self, func, interval and interval * 0.033 or 0.033 , duration, delay)
end

function Timerfunc.wait_timer(self, func, interval, flag)
    flag = flag or func

    interval = interval or 0.02

    if self.__wait_timer and self.__wait_timer[flag] then
        return
    end

    if not self.__wait_timer then
        self.__wait_timer = {}
    end

    if not self.__wait_timer[flag] then
        self.__wait_timer[flag] = Timerfunc.add_timer(self, function ()
            func()
            self.__wait_timer[flag] = nil
        end, interval)
        return self.__wait_timer[flag]
    end
end

function Timerfunc.reset_timer(self, timerId)
    local timer = timerMap[timerId]
    if timer then
        timer.nextTime = timer.delay > 0 and timer.delay + Time.time or timer.interval + Time.time
    end
end

function Timerfunc.del_timer(self, timerId)
    if not timerId or not self.__timers[timerId] then return end
    local timer = timerMap[timerId]
    if not timer then return end
    removeMap[timerId] = timer
    self.__timers[timerId] = nil
end

function Timerfunc.del_all_timer(self)
    if not self.__timers then return end
    for id, v in pairs(self.__timers) do
        Timerfunc.del_timer(self, id)
    end
end

local function release(timer)
    timer.guid = nil
    timer.self = nil
    timer.func = nil
    timer.interval = nil
    timer.duration = nil
    timer.delay = nil
    timer.nextTime = nil
end

local function remove()
    local num = table.nums(removeMap)
    if num > 0 then
        for id, v in pairs(removeMap) do
            local timer = timerMap[id]
            if timer then
                if timer.self and timer.self.__timers and timer.self.__timers[id] then
                    timer.self.__timers[id] = nil
                end

                if timer.self and timer.self.__wait_timer and timer.self.__wait_timer[id] then
                    timer.self.__wait_timer[id] = nil
                end

                timerMap[id] = nil
                release(timer)
            end
            removeMap[id] = nil
        end
    end
end

local function update()
    local now = Time.time
    for k,timer in pairs(timerMap) do
        if timer and now > timer.nextTime then
            timer.times = timer.times + 1
            if timer.duration ~= -1 and timer.times >= timer.duration then
                removeMap[timer.guid] = timer
            end

            timer.nextTime = now + timer.interval
            timer.func(timer.self)
        end
    end
    remove()
end


CS.TimerManager.Instance:AddFrame(update, 0, 1, -1)

return Timerfunc