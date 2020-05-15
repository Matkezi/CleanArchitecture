import React, { useState } from "react";
import { SkipperStatus } from "../../types/SkipperStatus";
import styles from './skippersHeader.module.scss'
import Grid from '@material-ui/core/Grid';

interface IProps {
  showDeclined?: boolean,
  color?: string,
  updateSkipperStatus: (updatedSkipperStatus: SkipperStatus) => void;
}

const SkippersHeader: React.FC<IProps> = (props: IProps) => {

  const [selected, setSelected] = useState(SkipperStatus.Pending)

  return (
    <React.Fragment>
      <Grid container>
        <Grid item xs={4} sm={2} md={2}>
          <span onClick={() => {
            setSelected(SkipperStatus.Pending);
            props.updateSkipperStatus(SkipperStatus.Pending);
          }}
            className={selected === SkipperStatus.Pending ? styles.headerRowSelected : styles.headerRow}
            style={{ color: props.color, borderBottomColor: props.color }}
          >
            Pending
        </span>
        </Grid>
        <Grid item xs={4} sm={2} md={2}>
          <div onClick={() => {
            setSelected(SkipperStatus.Approved);
            props.updateSkipperStatus(SkipperStatus.Approved);
          }}
            className={selected === SkipperStatus.Approved ? styles.headerRowSelected : styles.headerRow}
            style={{ color: props.color, borderBottomColor: props.color }}
          >
            Accepted
          </div>
        </Grid>
        {props.showDeclined && <Grid item xs={4} sm={2} md={2}>
          <div onClick={() => {
            setSelected(SkipperStatus.Declined);
            props.updateSkipperStatus(SkipperStatus.Declined);
          }}
            className={selected === SkipperStatus.Declined ? styles.headerRowSelected : styles.headerRow}
            style={{ color: props.color, borderBottomColor: props.color }}
          >
            Declined
          </div>
        </Grid>}
      </Grid>
    </React.Fragment>
  );
};

export default SkippersHeader;
