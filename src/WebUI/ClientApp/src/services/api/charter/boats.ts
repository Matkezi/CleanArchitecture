import { api } from '../../api';
import { Boat } from "../../../types/Boat"
import { IBoatType } from '../../../types/BoatType';
import { ILicenceType } from '../../../types/LicenceType';
import { API } from '../../../constants/apiRoutes';

export default
  {
    getCharterBoats(): Promise<Boat[]> {
      return api.get(API.CHARTER.BOATS.GET_CHARTER_BOATS)
    },
    saveBoat(boat: Boat): Promise<any> {
      return api.post(API.CHARTER.BOATS.SAVE_BOAT, boat);
    },
    updateBoat(id: number, boat: Boat): Promise<any> {
      return api.put(API.CHARTER.BOATS.UPDATE_BOAT(id), boat);
    },
    deleteBoat(id: number): Promise<any> {
      return api.delete(API.CHARTER.BOATS.DELETE_BOAT(id));
    }
  };