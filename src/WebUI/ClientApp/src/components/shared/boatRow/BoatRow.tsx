import React from "react";
import { Grid, Divider, Button } from "@material-ui/core";
import styles from "./styles.module.scss";
import { Boat } from "../../../types/Boat";
import BoatIcon from '../../../assets/img/icons/boat-icon-green-28.png';
import EditIcon from '../../../assets/img/icons/edit-icon-green-28.png'

export interface IProps {
  boat: Boat;
  choseBoat?: (boat: Boat) => void;
  deselectBoat?: () => void;
  boatSelected: boolean;
}

const BoatRow: React.FC<IProps> = (props: IProps) => {
  return (
    <Grid
      container
      className={styles.boatRow}
      justify="space-around"
      alignItems="center"
      item
    >
      <Grid item xs={1} container justify="center" alignItems="center">
        <img className={styles.boatPic} src={props.boat.boathPhotoUrl} alt="boat_pic" />
      </Grid>
      <Divider style={{ height: 60 }} orientation="vertical" />
      <Grid item xs={2}>
        {props.boat.name}
      </Grid>
      <Divider style={{ height: 60 }} orientation="vertical" />
      <Grid item xs={3} container>
        <Grid item xs={2} container alignItems="center">
          <img
            className={styles.icon}
            src={BoatIcon}
            alt="icon"
          />
        </Grid>
        <Grid item xs={10} container alignItems="center">
          {props.boat.model}, {props.boat.length} m
        </Grid>
      </Grid>
      <Divider style={{ height: 60 }} orientation="vertical" />
      <Grid item container xs={3} justify="flex-end" alignItems="center">
        {!props.boatSelected ? (
          <Button
            className={styles.choseBoatButton}
            onClick={() => props.choseBoat!(props.boat)}
          >
            <span className={styles.choseBoatButtonText}>Choose the Boat</span>
          </Button>
        ) : (
            <Button onClick={() => props.deselectBoat!()}>
              <img
                className={styles.icon}
                src={EditIcon}
                alt="edit"
              ></img>
            </Button>
          )}
      </Grid>
    </Grid>
  );
};

export default BoatRow;
