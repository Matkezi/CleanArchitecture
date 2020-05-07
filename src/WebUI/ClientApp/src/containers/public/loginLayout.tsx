import React, { ReactNode } from 'react';
import { Grid } from '@material-ui/core';
import styles from './styles.module.scss';

interface IProps {
    children: ReactNode,
}

const LoginLayout: React.FC<IProps> = (props: IProps) => {

    return (
        <div>
            <Grid container xs={12} item justify="center" alignItems="center">
                <div className={styles.loginWrapper}>
                    {props.children}
                </div>
            </Grid>
        </div>
    );
}

export default LoginLayout;