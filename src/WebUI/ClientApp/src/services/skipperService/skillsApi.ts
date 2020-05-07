import { api } from '../api';

export default
    {
        getSkills(): Promise<any> {
            return api.get('Skill')
        }
    };