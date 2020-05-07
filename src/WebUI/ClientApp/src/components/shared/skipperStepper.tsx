import React, { ReactNode } from 'react';
import { Grid } from '@material-ui/core';
import styles from './styles.module.scss';
import { StepperHelper } from '../../helpers/StepperHelper';

interface IProps {
    children: ReactNode
}

const SkipperStepper: React.FC<IProps> = (props: IProps) => {

    return (
        <div className={styles.stepper}>
            <Grid container justify="center">
                <div className={styles.stepperContainer}>
                    <Grid container direction="row" item xs={12} justify="center">
                        <Grid item xs={3}>
                            <div className={styles.step1} />
                        </Grid>
                        <Grid item xs={3}>
                            <div className={StepperHelper.getStep() > 1 ? styles.step2 + " " + styles.step2Active : styles.step2} />
                        </Grid>
                        <Grid item xs={3}>
                            <div className={StepperHelper.getStep() > 2 ? styles.step3 + " " + styles.step3Active : styles.step3} />
                        </Grid>
                        <Grid item xs={3}>
                            <div className={StepperHelper.getStep() > 3 ? styles.step4 + " " + styles.step4Active : styles.step4} />
                        </Grid>
                    </Grid>
                    {props.children}
                </div>
            </Grid>
        </div>
    );
}

export default SkipperStepper;