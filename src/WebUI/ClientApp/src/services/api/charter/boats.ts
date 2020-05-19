import { api } from '../../api';
import { Boat } from "../../../types/Boat"
import { IBoatType } from '../../../types/BoatType';
import { ILicenceType } from '../../../types/LicenceType';

export default
  {
    getCharterBoats(): Promise<Boat[]> {
      return api.get('Boat/charter')
    },
    //čita se s fronta - ne valja, treba povući s baceknda, ali za sad ostaje ovako
    getBoatTypes(): Promise<IBoatType[]> {
      return api.get('Boat/boat-types')
    },
    //isto ka ogore
    getLicenceTypes(): Promise<ILicenceType[]> {
      return api.get('Boat/licence-types')
    },
    saveBoat(boat: Boat): Promise<any> {
      return api.post('Boat/', boat);
    },
    updateBoat(id: number, boat: Boat): Promise<any> {
      return api.put('Boat/' + id, boat);
    },
    deleteBoat(id: number): Promise<any> {
      return api.delete('Boat/' + id);
    }
  };