import { Boat } from './../../types/Boat';

export class BoatSorter {

    static sortBoats(boats: Boat[]): Boat[] {
        return boats.sort((b1: Boat, b2: Boat) => {
            return b1.id - b2.id;
        });
    }

}