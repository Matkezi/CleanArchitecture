import React from "react";
import { ILanguage } from './../../types/ILanguage';
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import Rating from "@material-ui/lab/Rating";
import FiberManualRecordIcon from "@material-ui/icons/FiberManualRecord";
import Box from "@material-ui/core/Box";
import { Grid } from '@material-ui/core'
import mainStyles from './main.module.scss'

interface IProps {
    language: ILanguage,
    handleLevelChange?: (languageID: number, level: number) => void,
    className?: string,
    disabled: boolean
}

const StyledRating = withStyles({
    iconFilled: {
        color: "rgb(64,110,142)"
    },
    iconHover: {
        color: "rgb(58, 102, 133)"
    }
})(Rating);

const LanguageComponent: React.FC<IProps> = (props: IProps) => {

    const customIcons: any = {
        1: {
            icon: <FiberManualRecordIcon />,
            label: "1"
        },
        2: {
            icon: <FiberManualRecordIcon />,
            label: "2"
        },
        3: {
            icon: <FiberManualRecordIcon />,
            label: "3"
        },
        4: {
            icon: <FiberManualRecordIcon />,
            label: "4"
        },
        5: {
            icon: <FiberManualRecordIcon />,
            label: "5"
        }
    };

    function handleChange(event: any, value: number | null): void {
        if (value !== null) {
            props.handleLevelChange!(props.language.id, value)
        }
    }

    function IconContainer(props: any) {
        const { value, ...other } = props;
        return <span {...other}>{customIcons[value].icon}</span>;
    }

    IconContainer.propTypes = {
        value: PropTypes.number.isRequired
    };

    return (
        <Grid container direction="row">
            <Grid item xs={5} sm={3} >
                <Box component="fieldset" borderColor="transparent" >
                    <span className={props.className ? mainStyles.languageName + " " + props.className : mainStyles.languageName}>{props.language.label}</span >
                </Box >
            </Grid>
            <Grid item xs={7} sm={5}>
                <Box component="fieldset" borderColor="transparent" >
                    <StyledRating
                        name={props.language.label}
                        defaultValue={1}
                        disabled={props.disabled}
                        value={props.language.levelOfKnowledge}
                        getLabelText={value => customIcons[value].label}
                        IconContainerComponent={IconContainer}
                        onChange={handleChange}
                    />
                </Box >
            </Grid>
        </Grid>)
}

export default LanguageComponent;