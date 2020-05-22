import React, { useState, useContext } from "react";
import { Grid, Divider, Button, Modal, Paper } from "@material-ui/core";
import styles from "./styles.module.scss";
import { Boat } from "../../../types/Boat";
import BoatIcon from '../../../assets/img/icons/boat-icon-green-28.png';
import EditIcon from '../../../assets/img/icons/edit-icon-green-28.png'
import DeleteIcon from '../../../assets/img/icons/delete-icon-31.png';
import NewBoatForm from '../../charter/boats/newBoatForm';
import { LicenceEnum } from '../../../helpers/enums/LicenceEnum'
import { BoatTypeEnum } from '../../../helpers/enums/BoatEnum';
import { BoatContext } from "../../../providers/boats/boatsContext";

export interface IProps {
  boat: Boat;
  choseBoat?: (boat: Boat) => void;
  deselectBoat?: () => void;
  boatSelected: boolean;
  editable: boolean;
  saveBoat?: (boat: Boat) => void,
  updateBoat?: (id: number, boat: Boat) => void,
  deleteBoat?: (id: number) => void
}

const BoatRow: React.FC<IProps> = (props: IProps) => {

  const [showEdit, setShowEdit] = useState(false);
  const [deleteData, setDeleteData] = useState({ showDelete: false, id: -1, boatName: "" });
  const boatContext = useContext(BoatContext);

  const boat: Boat = {
    ...props.boat,
    minimalRequiredLicense: LicenceEnum.filter(l => l.id === (props.boat.minimalRequiredLicense as unknown as number))[0],
    type: BoatTypeEnum.filter(b => b.id === (props.boat.type as unknown as number))[0]
  }

  const preformDelete = (id: number, boatName: string) => {
    setDeleteData({ showDelete: true, id, boatName });
  }

  const [photo, setPhoto] = useState({
    photoData: { name: "" },
    photoURL: boat.boatPhotoUrl,
    readerData: ""
  });

  const handleChange = (event: any) => {
    event.persist();
    const reader = new FileReader();
    if (event.target.files[0] instanceof Blob) {
      reader.readAsDataURL(event.target.files[0]);
      reader.onloadend = () => {
        setPhoto({ photoData: event.target.files[0], photoURL: URL.createObjectURL(event.target.files[0]), readerData: reader.result as string });
      }
    }
  }

  return (
    <Grid
      container
      className={styles.boatRow}
      justify="space-around"
      alignItems="center"
      item
    >
      <Grid item xs={1} container justify="center" alignItems="center">
        <img className={styles.boatPic} src={props.boat.boatPhotoUrl} alt="boat_pic" />
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
      {!props.editable ?
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
        :
        <Grid item container xs={3} justify="flex-end" alignItems="center" className={styles.iconContainer}>
          <img
            className={styles.icon}
            src={EditIcon}
            alt="edit"
            onClick={() => { setShowEdit(true); }}
          ></img>
          <img
            onClick={() => preformDelete(props.boat.id, props.boat.name)}
            className={styles.icon}
            src={DeleteIcon}
            alt="delete"
          ></img>
        </Grid>
      }
      {showEdit &&
        <NewBoatForm updateBoat={props.updateBoat} data={boat} showIcon={!showEdit} title="" closeForm={() => { setShowEdit(false) }} handleChange={handleChange} photoURL={photo.photoURL} saveBoat={props.saveBoat!} />}
      {deleteData.showDelete &&
        <Grid container item xs={12}>
          <Modal open={deleteData.showDelete} className={styles.Modal} onClose={() => setDeleteData({ ...deleteData, showDelete: false })}>
            <Paper className={styles.Paper}>
              <Grid container>
                <Grid item container xs={12} justify="center">Are you sure you want to delete boat &nbsp; <b>{deleteData.boatName}</b>?</Grid>
                <Grid item container xs={12} justify="center" className={styles.buttonsCont}>
                  <button className={styles.cancelDeleteBtn} onClick={() => { setDeleteData({ ...deleteData, showDelete: false }) }}><span>Cancel</span></button>
                  <button className={styles.deleteBoatBtn} onClick={() => { setDeleteData({ ...deleteData, showDelete: false }); props.deleteBoat!(deleteData.id); }}><span>Yes</span></button>
                </Grid>
              </Grid>
            </Paper>
          </Modal>
        </Grid>
      }
    </Grid >
  );
};

export default BoatRow;
