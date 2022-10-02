local EntitySkillData = class()

function EntitySkillData:ctor()
    self.code = nil
    self.level = nil
    self.cfg = nil
    self.lastRelease = 0
end

function EntitySkillData:set_data(data)
    self.code = data.code
    self.level = data.level
    self.cfg = MDefine.cfg.skill.getSkillCfgById(data.code)
end

function EntitySkillData:record_release_time(time)
    self.lastRelease = time
end

function EntitySkillData:is_ready()
    if not self.cfg then return false end
    return Time.time - self.lastRelease > self.cfg.cd
end

return EntitySkillData