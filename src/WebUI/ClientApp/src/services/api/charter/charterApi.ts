import { api } from '../../api';
import { API } from '../../../constants/apiRoutes';


export default
    {
        getCharterById(id: string): Promise<any> {
            return api.get(API.CHARTER.GET_CHARTER_BY_ID(id))
        },
    };

