import React, { useState, useContext } from 'react';
import { Grid, Divider } from '@material-ui/core';
import styles from './styles.module.scss';
import PlusIcon from '../../../assets/img/icons/plus-icon-green-37.png';
import NewBoatForm from './newBoatForm';
import { Boat } from '../../../types/Boat';
import { BoatContext } from '../../../providers/boats/boatsContext';

interface IProps {
    saveBoat: (boat: Boat) => void
}

const AddBoatComponent: React.FC<IProps> = (props: IProps) => {

    const boatContext = useContext(BoatContext);
    const [addingNewBoat, setAddingNewBoat] = useState(boatContext.showForm);
    const [photo, setPhoto] = useState({
        photoData: { name: "" },
        photoURL: "",
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
        <div className={styles.container}>
            <Grid container>
                <Grid item container xs={12}>
                    <Grid item container sm={9} xs={6} className={styles.TitleContainer + " " + styles.titleCont}>
                        <div>
                            <span>Boats</span>
                        </div>
                    </Grid>
                    <Grid item container sm={3} xs={6} alignItems="flex-end" justify="flex-end" className={styles.newBoatCont}>
                        {!boatContext.showForm && <div className={styles.newBoat} onClick={() => { setAddingNewBoat(true); boatContext.setShowForm(true); }}>
                            <span>Add New Boat</span>
                            <img alt="" src={PlusIcon} className={styles.plusIcon} />
                        </div>}
                    </Grid>
                </Grid>
                <Divider className={styles.divider} />
                {addingNewBoat && boatContext.showForm &&
                    <Grid item container xs={12}>
                        <NewBoatForm showIcon={true} title="Adding New Boat" closeForm={() => { setAddingNewBoat(false); boatContext.setShowForm(false); }} handleChange={handleChange} photoURL={photo.photoURL} saveBoat={props.saveBoat} />
                    </Grid>
                }
            </Grid>
        </div>
    );
}

export default AddBoatComponent;