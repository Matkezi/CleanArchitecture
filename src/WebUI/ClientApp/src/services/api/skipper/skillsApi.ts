import { api } from '../../api';
import { API } from '../../../constants/apiRoutes';

export default
    {
        getSkills(): Promise<any> {
            return api.get(API.SKILLS.GET_ALL)
        }
    };