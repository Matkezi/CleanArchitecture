import React, { useContext, useState, useEffect } from "react";
import { GuestBookingContext } from "../../providers/booking/guestBooking";
import { Skipper } from "../../types/Skipper";
import BookingApi from "../../services/api/shared/booking"
import SkipperCard from "../../components/shared/skipperCard/SkipperCard";
import styles from './styles.module.scss';
import { Grid, Divider, Chip, FormControl, InputLabel, Input, Select as MaterialSelect, MenuItem, Checkbox, ListItemText } from "@material-ui/core";
import { dateHelper } from './../../helpers/dateHelper';
import { StepperHelper } from './../../helpers/StepperHelper';
import { ISkill } from "../../types/ISkill";
import { ILanguage } from "../../types/ILanguage";
import Select from 'react-select';
import { NotificationContext } from "../../providers/notification";
import { NotificationType } from "../../types/NotificationProps";
import { CLIENT } from "../../constants/clientRoutes";


interface IProps {
    history: any
    skills: ISkill[],
    languages: ILanguage[]
}

const GuestStep2: React.FC<IProps> = (props: IProps) => {
    const [skippers, setSkippers] = useState<Skipper[]>([]);
    const [selectedFilter, setSelectedFilter] = useState<number>(1);
    const [selectedSkills, setSelectedSkills] = useState<ISkill[]>([]);
    const [selectedLanguages, setSelectedLanguages] = useState<ILanguage[]>([]);
    const guestBookingContext = useContext(GuestBookingContext);
    const notificationContext = useContext(NotificationContext);

    useEffect(() => {
        const fetchData = async () => {
            notificationContext.setLoading({ showLoading: true })
            try {
                const skippers = await BookingApi.getAvaliableSkippers({
                    dateFrom: guestBookingContext.booking.bookedFrom!,
                    dateTo: guestBookingContext.booking.bookedTo!,
                    requiredSkills: selectedSkills.map(s => s.name),
                    listOfLanguages: selectedLanguages.map(l => l.label)
                });
                setSkippers(skippers);
            } catch (e) {
                notificationContext.setLoading({ showLoading: false })
                notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
            } finally {
                notificationContext.setLoading({ showLoading: false })
            }
        };
        fetchData();
    }, []);

    const ITEM_HEIGHT = 48;
    const ITEM_PADDING_TOP = 8;
    const MenuProps = {
        style: {
            maxHeight: ITEM_HEIGHT * 7.5 + ITEM_PADDING_TOP,
            width: 250,
        }
    };

    const requestSkipper = async (skipper: Skipper) => {
        guestBookingContext.setBooking({ ...guestBookingContext.booking, skipperId: skipper.id, skipper: skipper });
        props.history.push(StepperHelper.increasedStepUrl())
    }

    const handleSkillChange = (event: any) => {
        var tempArray = selectedSkills!.slice();
        var tempSkill: ISkill = {
            id: parseInt(event.target.value[0].split(":")[1], 10),
            name: event.target.value[0].split(":")[0],
            icon: event.target.value[0].split(":")[2],
        }
        if (tempArray.some(s => s.name === tempSkill.name)) {
            var index = tempArray.findIndex(i => i.name === tempSkill.name);
            tempArray.splice(index, 1);
        }
        else {
            tempArray.push(tempSkill);
        }
        setSelectedSkills(tempArray);
        applyFilters(tempArray.map(s => s.name), selectedLanguages.map(l => l.label));
    }

    const deleteSkill = (skillname: string) => {
        var tempArray = selectedSkills!.slice();
        if (tempArray.some(s => s.name === skillname)) {
            var index = tempArray.findIndex(i => i.name === skillname);
            tempArray.splice(index, 1);
        }
        setSelectedSkills(tempArray);
        applyFilters(tempArray.map(s => s.name), selectedLanguages.map(l => l.label));
    }

    const deleteLanguage = (languageName: string) => {
        var tempArray = selectedLanguages.slice();
        if (tempArray.some(l => l.label === languageName)) {
            var index = tempArray.findIndex(i => i.label === languageName);
            tempArray.splice(index, 1);
        }
        setSelectedLanguages(tempArray);
        applyFilters(selectedSkills.map(s => s.name), tempArray.map(l => l.label));
    }

    const redirectToSkipperProfile = (skipperId: string) => {
        props.history.push(CLIENT.GUEST.SKIPPER_PROFILE(skipperId));
    }

    const handleLanguageAuto = (event: any) => {
        var tempArray = selectedLanguages.slice();
        var tempLanguage: ILanguage = {
            label: event[0].label,
            id: event[0].id,
            levelOfKnowledge: event[0].levelOfKnowledge,
        }
        if (tempArray.some(l => l.id === tempLanguage.id)) {
            var index = tempArray.findIndex(i => i.id === tempLanguage.id);
            tempArray.splice(index, 1);
        }
        else {
            tempArray.push(tempLanguage);
        }
        setSelectedLanguages(tempArray);
        applyFilters(selectedSkills.map(s => s.name), tempArray.map(l => l.label));
    }

    const applyFilters = async (requiredSkills: string[], listOfLanguages: string[]) => {
        const skippers = await BookingApi.getAvaliableSkippers({
            dateFrom: guestBookingContext.booking.bookedFrom!,
            dateTo: guestBookingContext.booking.bookedTo!,
            requiredSkills: requiredSkills,
            listOfLanguages: listOfLanguages
        });
        setSkippers(skippers);
    }

    return (
        <React.Fragment>
            <Grid container direction="row" style={{ marginBottom: 120 }}>
                <Grid item xs={12}>
                    <p className={styles.greeting}>
                        Choose the skipper with the availability and the skills you want.
                    </p>
                </Grid>
                <Grid item xs={12} container >
                    <Grid item xs={6} container><p className={styles.recomendation}>Recommended for you</p></Grid>
                    <Grid item xs={6} container justify="flex-end">
                        <p className={styles.recomendation}>
                            {dateHelper.formatDates(guestBookingContext.booking.bookedFrom!, guestBookingContext.booking.bookedTo!)}
                        </p>
                    </Grid>
                </Grid>
                <Grid item xs={12}>
                    <Divider className={styles.divider} />
                </Grid>
                <Grid item xs={12} container justify="flex-start">
                    <p className={styles.filter}>Filter:</p>
                    <p onClick={() => setSelectedFilter(1)} className={selectedFilter === 1 ? styles.skills + " " + styles.selected : styles.skills}>Skills</p>
                    <p onClick={() => setSelectedFilter(2)} className={selectedFilter === 2 ? styles.languages + " " + styles.selected : styles.languages}>Languages</p>
                </Grid>
                <Grid item xs={12} sm={8} md={3}>
                    {selectedFilter === 1 ?
                        <FormControl className={styles.formSelect}>
                            <InputLabel id="demo-mutiple-checkbox-label">Select a skill</InputLabel>
                            <MaterialSelect
                                labelId="demo-mutiple-checkbox-label"
                                id="demo-mutiple-checkbox"
                                displayEmpty={true}
                                multiple
                                value={[]}
                                onChange={handleSkillChange}
                                input={<Input />}
                                MenuProps={MenuProps}
                            >
                                {props.skills.map(skill => (
                                    <MenuItem key={skill.name} value={skill.name + ":" + skill.id + ":" + skill.icon}>
                                        <Checkbox style={{ color: "#406e8e" }} checked={selectedSkills.some(s => s.name === skill.name)} />
                                        <ListItemText primary={skill.name} />
                                    </MenuItem>
                                ))}
                            </MaterialSelect>
                        </FormControl>
                        :
                        <FormControl className={styles.formSelect}>
                            <Select
                                className={styles.autoCompleteSelect}
                                value={[]}
                                isMulti
                                placeholder="Type a language..."
                                options={props.languages}
                                onChange={e => handleLanguageAuto(e)}
                            >
                            </Select>
                        </FormControl>
                    }
                </Grid>
                <Grid item xs={12} container justify="flex-start">
                    {selectedSkills.map(skill =>
                        <Chip icon={<i className={skill.icon}></i>} deleteIcon={<i className={"fas fa-times " + styles.deleteIcon}></i>} onDelete={() => deleteSkill(skill.name)} className={styles.chip} style={{ backgroundColor: "#4f5b8b" }} key={skill.name} label={skill.name} />)}
                    {selectedLanguages.map(lang =>
                        <Chip deleteIcon={<i className={"fas fa-times " + styles.deleteIcon}></i>} className={styles.chip} onDelete={() => deleteLanguage(lang.label)} style={{ backgroundColor: "#4f5b8b" }} key={lang.id} label={lang.label} />)}

                </Grid>
                {
                    skippers.map((skipper, i) => (
                        <SkipperCard viewSkipperProfile={redirectToSkipperProfile} key={skipper.id} skipper={skipper} requestSkipper={requestSkipper}></SkipperCard>
                    ))
                }
            </Grid>
        </React.Fragment>
    )
}

export default GuestStep2;