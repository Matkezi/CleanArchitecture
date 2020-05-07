import { api } from '../api';
import {Boat} from "../../types/Boat"

export default 
{
  getCharterBoats(): Promise<Boat[]>{
    return api.get('Boat/charter')
  },
};