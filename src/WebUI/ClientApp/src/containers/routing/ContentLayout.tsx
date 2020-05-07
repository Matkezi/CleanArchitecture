import React, { ReactNode } from 'react';
import { Grid } from '@material-ui/core';
import styles from './routes.module.scss';

interface IProps {
    children: ReactNode,
    document?: boolean
};

const ContentLayout: React.FC<IProps> = (props: IProps) => {
    return (
        <Grid container direction="column" justify="center" alignItems="center">
            <Grid item xs={12} sm={11} className={props.document ? styles.mobileDocument : ""}>{props.children}</Grid>
        </Grid>
    );
};

export default ContentLayout;