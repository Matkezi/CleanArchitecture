import { IBooatsContext, Boat } from "../../types/Boat";
import React, { useState } from "react";
import boatApi from "../../services/shared/boats";

export const BoatContext = React.createContext<IBooatsContext>({
  charterBoats: [],
  setCharterBoats: () => null
});

export const BoatContextProvider: React.ComponentType<React.ReactNode> = props => {
  const [charterBoats, setCharterBoatState] = useState<Boat[]>([]);

  const setCharterBoats = async (type?: string) => {
    let boats = await boatApi.getCharterBoats();
    if (type !== undefined) {
      boats = boats.filter(boat => boat.type === type)
    }
    setCharterBoatState(boats);
  };

  return (
    <BoatContext.Provider
      value={{
        charterBoats,
        setCharterBoats
      }}
    >
      {props.children}
    </BoatContext.Provider>
  );

}

export default BoatContextProvider