import React, { useContext, useState, useEffect } from 'react'
import { TrustedSkippersAction } from "../../../types/TrustedSkippers";
import { SkipperStatus } from "../../../types/SkipperStatus";
import { Checkbox, withStyles } from '@material-ui/core';
import { TrustedSkippersContext } from '../../../providers/skippers/trustedSkippers'
import styles from './trustedSkippers.module.scss'
import Grid from '@material-ui/core/Grid';
import CheckBoxOutlinedIcon from '@material-ui/icons/CheckBoxOutlined';
import CheckBoxOutlineBlankOutlinedIcon from '@material-ui/icons/CheckBoxOutlineBlankOutlined';

interface IProps {
    updateTrustedSkippersAction: (trustedSkippersAction: TrustedSkippersAction) => void;
}

const TrustedSkippersFooter: React.FC<IProps> = (props: IProps) => {
    const trustedSkippersContext = useContext(TrustedSkippersContext);
    const [currentySelectedSkipperCount, setCurrentySelectedSkipperCount] = useState(0);

    const GreenCheckbox = withStyles({
        root: {
            color: "#26816A",
            '&$checked': {
                color: "#26816A",
            },
        },
        checked: {},
    })((props: any) => <Checkbox color="default" {...props} />);

    useEffect(() => {
        switch (trustedSkippersContext.currentlySelectedSkipperStatus) {
            case SkipperStatus.Pending:
                setCurrentySelectedSkipperCount(trustedSkippersContext.pendingSkippers.length);
                break;
            case SkipperStatus.Approved:
                setCurrentySelectedSkipperCount(trustedSkippersContext.acceptedSkippers.length);
                break;
            case SkipperStatus.Declined:
                setCurrentySelectedSkipperCount(trustedSkippersContext.declinedSkippers.length);
                break;
        }
    });

    return (
        <div className={styles.footerWrapper}>
            <Grid container item xs={12} direction="row" justify="center" alignItems="center" className={styles.footerDiv}>
                <Grid item container xs={12} md={7} lg={7} direction="row" justify="center" alignItems="center">
                    <Grid item xs={4}>
                        {currentySelectedSkipperCount !== 0 ?
                            <span className={styles.numOfSelected}>{currentySelectedSkipperCount} skipper's requests are marked.</span> :
                            <span className={styles.numOfSelected}> Please select a skipper.</span>}
                    </Grid>
                    <Grid item xs={8} container direction="row" justify="center" alignItems="center">
                        <Grid item xs={4}>
                            <GreenCheckbox
                                checked={currentySelectedSkipperCount > 0}
                                onChange={() => props.updateTrustedSkippersAction(TrustedSkippersAction.UnmarkAll)}
                                value="primary"
                                inputProps={{ 'aria-label': 'primary checkbox' }}
                                size="medium"
                                icon={<CheckBoxOutlineBlankOutlinedIcon />}
                                checkedIcon={<CheckBoxOutlinedIcon />}
                                className={styles.checkbox}
                            />
                            <span style={{ padding: 15, cursor: "pointer", fontWeight: 600 }}
                                onClick={() => props.updateTrustedSkippersAction(TrustedSkippersAction.UnmarkAll)}>
                                Unmark All
                            </span>
                        </Grid>
                        <Grid item xs={4}>
                            {(trustedSkippersContext.currentlySelectedSkipperStatus === SkipperStatus.Pending
                                || trustedSkippersContext.currentlySelectedSkipperStatus === SkipperStatus.Approved) &&
                                <button className={styles.decline}
                                    onClick={() => props.updateTrustedSkippersAction(TrustedSkippersAction.Decline)}>
                                    <span>Decline</span>
                                </button>
                            }
                        </Grid>
                        <Grid item xs={4}>
                            {(trustedSkippersContext.currentlySelectedSkipperStatus === SkipperStatus.Pending
                                || trustedSkippersContext.currentlySelectedSkipperStatus === SkipperStatus.Declined) &&

                                <button className={styles.accept}
                                    onClick={() => props.updateTrustedSkippersAction(TrustedSkippersAction.Accept)}>
                                    <span>Accept</span>
                                </button>
                            }
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </div>
    );
};

export default TrustedSkippersFooter;
