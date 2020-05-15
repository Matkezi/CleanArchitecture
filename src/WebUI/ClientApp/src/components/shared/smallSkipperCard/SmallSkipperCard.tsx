import { Skipper } from "../../../types/Skipper";
import React from "react";
import { Grid, Button } from "@material-ui/core";
import styles from "./styles.module.scss";

export interface IProps {
  skipper?: Skipper;
}

const SmallSkipperCard: React.FC<IProps> = (props: IProps) => {
  return (
    <React.Fragment>
      <Grid container direction="row" className={styles.skipperCard}>
        <Grid item xs={3}>
          {/* <img src="https://celebxxx.pw/wp-content/uploads/2019/05/abigail_spencer3.jpg"></img> */}
        </Grid>
        <Grid item container xs={9} direction="column">
          <Grid item>Roko</Grid>
          <Grid item xs={6}>
            broj nemamo u bazi
          </Grid>
          <Grid item xs={6}>
            @skipper
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default SmallSkipperCard;
