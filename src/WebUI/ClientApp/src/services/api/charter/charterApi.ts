import { api } from '../../api';


export default
    {
        getCharterById(id: string): Promise<any> {
            return api.get('Charter/' + id)
        },
    };

