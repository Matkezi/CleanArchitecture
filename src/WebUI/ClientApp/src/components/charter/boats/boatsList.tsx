import React from 'react';
import { Boat } from '../../../types/Boat';
import { Grid } from '@material-ui/core';
import BoatRow from '../../shared/boatRow/BoatRow';
import styles from './styles.module.scss';

interface IProps {
    boats: Boat[];
    saveBoat: (boat: Boat) => void,
    updateBoat: (id: number, boat: Boat) => void,
    deleteBoat: (id: number) => void
}

const BoatsList: React.FC<IProps> = (props: IProps) => {
    return (
        <Grid container className={styles.boatContainer}>
            {props.boats.map((boat, i) => (
                <div className={styles.boatRow} key={i}>
                    <BoatRow deleteBoat={props.deleteBoat} key={i} boat={boat} boatSelected={false} editable={true} saveBoat={props.saveBoat} updateBoat={props.updateBoat} />
                </div>
            ))}
        </Grid>
    );
}

export default BoatsList;