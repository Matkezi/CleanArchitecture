import { IBooatsContext, Boat } from "../../types/Boat";
import React, { useState } from "react";
import boatApi from "../../services/api/charter/boats";
import { BoatSorter } from '../../helpers/sorters/boatSorter';

export const BoatContext = React.createContext<IBooatsContext>({
  charterBoats: [],
  showForm: false,
  setCharterBoats: () => null,
  deleteBoat: (id: number) => null,
  saveBoat: (boat: Boat) => null,
  updateBoat: (boat: Boat) => null,
  setShowForm: (show: boolean) => null
});

export const BoatContextProvider: React.ComponentType<React.ReactNode> = props => {
  const [charterBoats, setCharterBoatState] = useState<Boat[]>([]);
  const [showForm, setShowFormF] = useState<boolean>(false);

  const setCharterBoats = async (type?: string) => {
    let boats = await boatApi.getCharterBoats();
    if (type !== undefined) {
      boats = boats.filter(boat => (boat.type as string) === type)
    }
    setCharterBoatState(BoatSorter.sortBoats(boats));
  };

  const saveBoat = async (boat: Boat) => {
    var response = await boatApi.saveBoat(boat);
  }

  const updateBoat = async (id: number, boat: Boat) => {
    var response = await boatApi.updateBoat(id, boat);
  }

  const deleteBoat = async (id: number) => {
    var response = await boatApi.deleteBoat(id);
  }

  const setShowForm = (show: boolean) => {
    setShowFormF(show);
  }

  return (
    <BoatContext.Provider
      value={{
        charterBoats,
        showForm,
        setShowForm,
        saveBoat,
        deleteBoat,
        updateBoat,
        setCharterBoats
      }}
    >
      {props.children}
    </BoatContext.Provider>
  );

}

export default BoatContextProvider