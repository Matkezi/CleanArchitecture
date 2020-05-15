import React from "react";
import styles from './styles.module.scss';
import BackButtonImage from '../../../assets/img/icons/back-button.png';

export interface IProps {
}

const navigateBack = () => {
  window.history.back()
}

const BackButton: React.FC<IProps> = (props: IProps) => {
  return (
    <img alt="back-button" src={BackButtonImage} className={styles.plusIcon + " " + styles.rotate} onClick={navigateBack} >
    </img>
  );
};

export default BackButton;
