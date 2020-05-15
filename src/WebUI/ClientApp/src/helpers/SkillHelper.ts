import { SkillEnum } from './enums/SkillEnum';
import { ISkill } from '../types/ISkill';
export class SkillHelper {
    static mapSkills(skills: ISkill[]): ISkill[] {
        var mappedSkills: ISkill[] = [];
        if (skills.length === 0) {
            return mappedSkills;
        }
        else {
            skills.map(s => {
                var tempSkill = SkillEnum.find(l => l.id === s.id);
                mappedSkills.push(tempSkill!);
            })
            return mappedSkills;
        }
    }
}