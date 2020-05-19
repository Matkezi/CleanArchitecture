import React, { useState } from "react";
import styles from './styles.module.scss';
import { Grid, Chip, TextField, Divider, Input, MenuItem, FormControl, ListItemText, Select as MaterialSelect, Checkbox, InputLabel } from '@material-ui/core/';
import LanguageComponent from './../../ui/LanguageComponent';
import { ISkill } from "../../../types/ISkill";
import { ILanguage } from "../../../types/ILanguage";
import { ISkipper } from "../../../types/ISkipper";
import Select from 'react-select';
import DayPicker, { RangeModifier } from 'react-day-picker';
import { isSkipper } from '../../../services/appService/authorizationService';
import 'react-day-picker';
import 'react-day-picker/lib/style.css';
import BackButton from '../../shared/backButton/BackButton'
import { CLIENT } from "../../../constants/clientRoutes";


interface IProps {
    skipperProfile: ISkipper["skipperData"] | undefined,
    updateProfile: (skipper: ISkipper) => void,
    skills: ISkill[],
    languages: ILanguage[],
    history: any,
    setActiveTab: (tab: number) => void,
    available: RangeModifier[],
    booked: RangeModifier[],
    bookedStartDays: Date[],
    bookedEndDays: Date[],
    availableStartDays: Date[],
    availableEndDays: Date[]
}

const SkipperProfileComponent: React.FC<IProps> = (props: IProps) => {

    const [editAbout, setEditAbout] = useState(false);
    const [editPrice, setEditPrice] = useState(false);
    const [editLanguage, setEditLanguage] = useState(false);
    const [editSkills, setEditSkills] = useState(false);
    const [editContact, setEditContact] = useState(false);
    var fileInputRef: any;

    const [data, setData] = useState<ISkipper["skipperData"]>(props.skipperProfile!);



    const showEditBtns = isSkipper();

    const ITEM_HEIGHT = 48;
    const ITEM_PADDING_TOP = 8;
    const MenuProps = {
        style: {
            maxHeight: ITEM_HEIGHT * 7.5 + ITEM_PADDING_TOP,
            width: 250,
        }
    };

    const handleLanguageAuto = (event: any) => {
        var tempArray = data.listOfLanguages!.slice();
        var tempLanguage: ILanguage = {
            label: event[0].label,
            id: event[0].id,
            levelOfKnowledge: event[0].levelOfKnowledge,
            skipperId: props.skipperProfile?.id
        }
        if (tempArray.some(l => l.id === tempLanguage.id)) {
            var index = tempArray.findIndex(i => i.id === tempLanguage.id);
            tempArray.splice(index, 1);
        }
        else {
            tempArray.push(tempLanguage);
        }
        setData({ ...data, listOfLanguages: tempArray });
    }

    const handleLevelChange = (languageID: number, levelOfKnowledge: number) => {
        var tempArray = data.listOfLanguages!.slice();
        var index = tempArray.findIndex(l => l.id === languageID);
        var tempLanguage = tempArray[index];
        tempLanguage.levelOfKnowledge = levelOfKnowledge;
        tempArray.splice(index, 1, tempLanguage);
        setData({ ...data, listOfLanguages: tempArray });
    }

    const handleSkillChange = (event: any) => {
        var tempArray = data.listOfSkills!.slice();
        var tempSkill: ISkill = {
            id: parseInt(event.target.value[0].split(":")[1], 10),
            name: event.target.value[0].split(":")[0],
            icon: event.target.value[0].split(":")[2],
            skipperId: props.skipperProfile?.id!
        }
        if (tempArray.some(s => s.name === tempSkill.name)) {
            var index = tempArray.findIndex(i => i.name === tempSkill.name);
            tempArray.splice(index, 1);
        }
        else {
            tempArray.push(tempSkill);
        }
        setData({ ...data, listOfSkills: tempArray });
    }

    const handleChange = (event: any) => {
        event.persist();
        const reader = new FileReader();
        if (event.target.files[0] instanceof Blob) {
            reader.readAsDataURL(event.target.files[0]);
            reader.onloadend = () => {
                const userData = {
                    skipperData: {
                        id: props.skipperProfile?.id!,
                        ...props.skipperProfile,
                        UserPhoto: {
                            Name: event.target.files[0].name,
                            Data: reader.result as string
                        },
                        newEmail: props.skipperProfile?.email!
                    }
                }
                props.updateProfile(userData);
            }
        }
    }

    const changeData = (property: string, value: any) => {
        setData({ ...data, [property]: value });
    }


    const updateProperty = (property: string, value: any) => {
        const userData = {
            skipperData: {
                id: props.skipperProfile?.id!,
                ...props.skipperProfile,
                [property]: value
            }
        }
        props.updateProfile(userData);
    }

    const updateContact = (save: boolean) => {
        if (save) {
            const userData = {
                skipperData: {
                    id: props.skipperProfile?.id!,
                    ...props.skipperProfile,
                    city: data.city,
                    country: data.country,
                    phoneNumber: data.phoneNumber
                }
            }
            props.updateProfile(userData);
        }
        else {
            setData({
                ...data,
                phoneNumber: props.skipperProfile!.phoneNumber!,
                city: props.skipperProfile!.city!,
                country: props.skipperProfile!.country!
            })
        }
    }

    const { skipperProfile, skills, available, booked, bookedStartDays, bookedEndDays, availableStartDays, availableEndDays } = props;

    const modifiers = {
        booked: booked,
        bookedStartDays: bookedStartDays,
        bookedEndDays: bookedEndDays,
        availableStartDays: availableStartDays,
        availableEndDays: availableEndDays,
        available: available,
    };

    const calendarStyle = `.DayPicker-Day--highlighted:not(.DayPicker-Day--outside) {
        background-color: #274675;
        color: white;
        border-radius: 0px !important;
        outline: none;
    }
    
    .DayPicker-Day--first:not(.DayPicker-Day--outside) {
        background-color: #274574 !important;
        color: white;
        border-radius: 0px !important;
        border-top-left-radius: 50% !important;
        border-bottom-left-radius: 50% !important;
        outline: none;
    }
    
    .DayPicker-Day {
      padding-left: 0px !important;
      padding-right: 0px !important;
      padding-bottom: 0px;
      padding-top: 0px;
      height: 43px;
      outline: none !important;
    }
    
    .DayPicker-Caption:not(.DayPicker-Day--outside) {
      font-size: 18px;
      font-weight: normal;
      font-stretch: normal;
      font-style: normal;
      line-height: 1.36;
      letter-spacing: normal;
      text-align: center;
      color: #000000;
    }
    
    .DayPicker-Weekdays:not(.DayPicker-Day--outside) {
      font-weight: normal;
      font-stretch: normal;
      font-style: normal;
      line-height: 1.36;
      letter-spacing: normal;
      text-align: center;
      color: #000000;
    }
    
    .DayPicker-Day--booked:not(.DayPicker-Day--outside) {
      color: black;
      border-radius: 0px !important;
      vertical-align: middle;
      text-align: center;
    }
    
    .DayPicker-Day--booked div {  
      background-color: #919191 !important;
      height: fit-content !important;
      padding: 8px;
      vertical-align: middle;
      text-align: center;
    }
    
    .DayPicker-Day--bookedStartDays div:not(.DayPicker-Day--outside) {
      border-top-left-radius: 50% !important;
      border-bottom-left-radius: 50% !important;
    }
    
    .DayPicker-Day--bookedEndDays div:not(.DayPicker-Day--outside) {
      border-top-right-radius: 50% !important;
      border-bottom-right-radius: 50% !important;
    }
    
    .DayPicker-Day--avaliable div {  
      background-color: #23395b !important;
      height: fit-content !important;
      padding: 8px;
      vertical-align: middle;
      text-align: center;
    }
    
    .DayPicker-Day--avaliable {
      color: black;
      background-color: white !important;
      border-radius: 0px !important;
      vertical-align: middle;
      text-align: center;
    }
    
    .DayPicker-Day--last:not(.DayPicker-Day--outside) {
      background-color: #274574 !important;  
      border-radius: 0px !important;
      color: white;
      border-top-right-radius: 50% !important;
      border-bottom-right-radius: 50% !important;
      outline: none;
    }
    
    .DayPicker-Day--availableStartDays:not(.DayPicker-Day--booked) div:not(.DayPicker-Day--outside) {
      border-top-left-radius: 50%;
      border-bottom-left-radius: 50%;
    }
    
    .DayPicker-Day--availableEndDays:not(.DayPicker-Day--booked) div:not(.DayPicker-Day--outside) {
      border-top-right-radius: 50%;
      border-bottom-right-radius: 50%;
    }

    .DayPicker-Day--selected div {  
        background-color: #23395b !important;
        height: fit-content !important;
        padding: 8px;
        vertical-align: middle;
        text-align: center;
      }
      
      .DayPicker-Day--selected {
        color: black;
        background-color: white !important;
        border-radius: 0px !important;
        vertical-align: middle;
        text-align: center;
      }
    
    // this covers when avalible comes right after booking, we do not need it now
    //.DayPicker-Day--booked + .DayPicker-Day--avaliable:not(.DayPicker-Day--booked) > div:not(.DayPicker-Day--outside){
    //  border-top-left-radius: 50%;
    //  border-bottom-left-radius: 50%;
    //}
    
    // this covers when avalible comes right before booking, and is not supported in most browsers
    //.DayPicker-Week:has(.DayPicker-Day--avaliable + .DayPicker-Day--booked > div:not(.DayPicker-Day--outside)) .DayPicker-Day--avaliable > div:not(.DayPicker-Day--outside){
    //  border-top-right-radius: 50%;
     // border-bottom-right-radius: 50%;
    }
    `;

    function renderDay(day: Date) {
        const date = day.getDate();
        return (
            <div>{date}</div>
        );
    }



    return (
        <div className={styles.wrapper}>
            {!isSkipper() ? <div className={styles.backButton}><BackButton></BackButton></div> : null}
            <div className={styles.headline}>
                <Grid container direction="row" spacing={4}>
                    <Grid container className={styles.container} item xs={12} sm={4} direction="column">
                        <Grid item container justify="center">
                            <img className={styles.photoHolder} alt="" src={skipperProfile?.userPhotoUrl} />
                        </Grid>
                        {showEditBtns && <Grid item container direction="column">
                            <Grid item container xs={12} justify="center">
                                <input type="file" id='photoURL' style={{ display: 'none' }} ref={fileInput => fileInputRef = fileInput} onChange={(event) => handleChange(event)} />
                                <button type="button" style={{ marginTop: 15 }} className={styles.uploadButton} onClick={() => fileInputRef.click()}>
                                    Change
                                </button>
                            </Grid>
                        </Grid>
                        }
                    </Grid>
                    <Grid container item xs={12} sm={8}>
                        <Grid item xs={12}>
                            <span className={styles.skipperName}>{skipperProfile?.firstName}</span>
                        </Grid>
                        <Grid item xs={12} className={styles.about}>
                            <span >Pricing:</span>
                            {editPrice ? <TextField
                                id="standard-number"
                                type="number"
                                value={data.price!}
                                variant="outlined"
                                fullWidth={false}
                                className={styles.number}
                                onChange={(e) => changeData("price", (e.target.value as unknown as number) > 150 ? 150 : e.target.value as unknown as number)}
                            />
                                :
                                <span className={styles.notEditableNumber + " " + styles.bolded}>{props.skipperProfile?.price!}</span>}
                            <span> €/day
                              {showEditBtns &&
                                    <>  {!editPrice && <i onClick={() => setEditPrice(!editPrice)} className={"fas fa-pen " + styles.icon} title="Edit data"></i>}
                                        <>
                                            {editPrice &&
                                                <><i onClick={() => {
                                                    updateProperty("price", data.price!);
                                                    setEditPrice(!editPrice);
                                                }}
                                                    className={"fas fa-save " + styles.icon} title="Save changes"></i>
                                                    <i onClick={() => {
                                                        changeData("price", props.skipperProfile?.price!);
                                                        setEditPrice(!editPrice);
                                                    }}
                                                        className={"fas fa-times " + styles.icon} title="Cancel"></i>
                                                </>
                                            }
                                        </>
                                    </>}
                            </span>
                        </Grid>
                        <Grid item xs={12} className={styles.divider}>
                            <Divider />
                        </Grid>
                        <Grid item xs={12}>
                            <span className={styles.about}>Contact
                            {showEditBtns && <>  {!editContact && <i onClick={() => setEditContact(!editContact)} className={"fas fa-pen " + styles.icon}></i>}
                                    {editContact &&
                                        <> <i onClick={() => {
                                            updateContact(true);
                                            setEditContact(!editContact);
                                        }}
                                            className={"fas fa-save " + styles.icon} title="Save changes"></i>
                                            <i onClick={() => {
                                                updateContact(false);
                                                setEditContact(!editContact);
                                            }}
                                                className={"fas fa-times " + styles.icon} title="Cancel"></i>
                                        </>
                                    }
                                </>}
                            </span>
                            {!editContact &&
                                <>
                                    <Grid container item xs={12} direction="row">
                                        <Grid item xs={12} sm={5}>
                                            <span className={styles.info}><i className={"fas fa-phone " + styles.icon}></i>{props.skipperProfile!.phoneNumber!} </span>
                                        </Grid>
                                        <Grid item xs={12} sm={5}>
                                            <span className={styles.info}><i className={"fas fa-envelope " + styles.icon}></i>{props.skipperProfile!.email!} </span>
                                        </Grid>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Grid item xs={12} sm={5}>
                                            <span className={styles.info}><i className={"fas fa-globe-europe " + styles.icon}></i>{props.skipperProfile!.city! + ", " + props.skipperProfile!.country!}</span>
                                        </Grid>
                                    </Grid>
                                </>}
                            {editContact &&
                                <>
                                    <Grid container item xs={12} direction="row">
                                        <Grid item xs={12} sm={5}>
                                            <TextField
                                                id="standard-text1"
                                                value={data.phoneNumber}
                                                variant="outlined"
                                                fullWidth={false}
                                                className={styles.contact}
                                                onChange={(e) => changeData("phoneNumber", e.target.value)}
                                                onBlur={(e) => changeData("phoneNumber", e.target.value)}
                                                spellCheck={false} />
                                        </Grid>
                                        <Grid item xs={12} sm={5}>
                                            <span className={styles.info}><i className={"fas fa-envelope " + styles.icon}></i>{props.skipperProfile!.email!} </span>
                                        </Grid>
                                    </Grid>
                                    <Grid item xs={12} container direction="row">
                                        <Grid item xs={12} sm={5}>
                                            <TextField
                                                id="city"
                                                value={data.city}
                                                variant="outlined"
                                                fullWidth={false}
                                                className={styles.contact}
                                                onChange={(e) => changeData("city", e.target.value)}
                                                onBlur={(e) => changeData("city", e.target.value)}
                                                spellCheck={false} />
                                        </Grid>
                                        <Grid item xs={12} sm={5}>
                                            <TextField
                                                id="country"
                                                value={data.country}
                                                variant="outlined"
                                                fullWidth={false}
                                                className={styles.contact}
                                                onChange={(e) => changeData("country", e.target.value)}
                                                onBlur={(e) => changeData("country", e.target.value)}
                                                spellCheck={false} />
                                        </Grid>
                                    </Grid>
                                </>}
                        </Grid>
                        <Grid item xs={12} className={styles.divider}>
                            <Divider />
                        </Grid>
                        <Grid item xs={12}>
                            <span className={styles.about}>About
                            {showEditBtns &&
                                    <> {!editAbout ?
                                        <i onClick={() => setEditAbout(!editAbout)} className={"fas fa-pen " + styles.icon}></i>
                                        :
                                        <>
                                            <i onClick={() => { updateProperty("PersonalSummary", data.personalSummary); setEditAbout(!editAbout); }} className={"fas fa-save " + styles.icon}></i>
                                            <i onClick={() => { changeData("personalSummary", props.skipperProfile?.personalSummary); setEditAbout(!editAbout); }} className={"fas fa-times " + styles.icon}></i>
                                        </>
                                    }
                                    </>}
                            </span>
                            <TextField
                                id="outlined-multiline-flexible"
                                multiline
                                value={editAbout ? data.personalSummary : props.skipperProfile!.personalSummary}
                                onChange={(e) => changeData("personalSummary", e.target.value)}
                                onBlur={(e) => changeData("personalSummary", e.target.value)}
                                className={editAbout ? styles.textField : styles.textFieldDisabled}
                                margin="normal"
                                placeholder="Tell us something about yourself..."
                                disabled={!editAbout}
                                variant='outlined'
                                spellCheck={false}
                            />
                        </Grid>
                        <Grid item xs={12} className={styles.divider}>
                            <Divider />
                        </Grid>
                        <Grid item xs={12}>
                            <span className={styles.about}>Skills
                            {showEditBtns &&
                                    <>     {!editSkills && <i onClick={() => setEditSkills(!editSkills)} className={"fas fa-pen " + styles.icon}></i>}
                                        {editSkills && <i onClick={() => {
                                            updateProperty("listOfSkills", data.listOfSkills);
                                            setEditSkills(!editSkills);
                                        }}
                                            className={"fas fa-save " + styles.icon} title="Save changes"></i>
                                        }
                                    </>}
                            </span>
                            {editSkills && <>{data.listOfSkills!.length === 0 ?
                                <span className={styles.notChosen}><i>No skills to show.</i></span> :
                                <Grid item xs={12}>
                                    {data.listOfSkills!.map(skill => (
                                        <Chip icon={<i className={skill.icon}></i>} className={styles.chip} style={{ backgroundColor: "#406e8e" }} key={skill.name} label={skill.name} />
                                    ))}
                                </Grid>
                            }
                            </>
                            }
                            {!editSkills && <>{props.skipperProfile!.listOfSkills!.length === 0 ?
                                <span className={styles.notChosen}><i>No skills to show.</i></span> :
                                <Grid item xs={12}>
                                    {props.skipperProfile!.listOfSkills!.map(skill => (
                                        <Chip icon={<i className={skill.icon}></i>} className={styles.chip} style={{ backgroundColor: "#406e8e" }} key={skill.name} label={skill.name} />
                                    ))}
                                </Grid>}
                            </>
                            }
                        </Grid>
                        {editSkills && <Grid item xs={10} sm={6}>
                            <FormControl className={styles.formSelect} style={{ marginLeft: 25, marginTop: 20 }}>
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
                                    {skills.map(skill => (
                                        <MenuItem key={skill.name} value={skill.name + ":" + skill.id + ":" + skill.icon}>
                                            <Checkbox style={{ color: "#406e8e" }} checked={data.listOfSkills!.some(s => s.name === skill.name)} />
                                            <ListItemText primary={skill.name} />
                                        </MenuItem>
                                    ))}
                                </MaterialSelect>
                            </FormControl>
                        </Grid>}
                        <Grid item xs={12} className={styles.divider}>
                            <Divider />
                        </Grid>
                        <Grid item xs={12}>
                            <span className={styles.about}>Languages
                            {showEditBtns &&
                                    <>  {!editLanguage && <i onClick={() => setEditLanguage(!editLanguage)} className={"fas fa-pen " + styles.icon}></i>}
                                        {editLanguage && <i onClick={() => {
                                            updateProperty("listOfLanguages", data.listOfLanguages!);
                                            setEditLanguage(!editLanguage);
                                        }}
                                            className={"fas fa-save " + styles.icon} title="Save changes"></i>
                                        }
                                    </>}
                            </span>
                            {editLanguage && <>
                                {data.listOfLanguages!.length === 0 ?
                                    <span className={styles.notChosen} style={{ display: "block" }}><i>No languages to show.</i></span> :
                                    <Grid item xs={12}>
                                        <div className={styles.levelDiv}><span className={styles.levelOfKnowledge}>Level of knowledge</span></div>
                                        {data.listOfLanguages!.map(language => (
                                            <div key={language.id} className={styles.languageHolder}>
                                                <LanguageComponent disabled={false} className={styles.marginTop} handleLevelChange={handleLevelChange} key={language.label} language={language} />
                                            </div>
                                        ))}
                                    </Grid>
                                }
                            </>
                            }
                            {!editLanguage && <>
                                {props.skipperProfile!.listOfLanguages!.length === 0 ?
                                    <span className={styles.notChosen} style={{ display: "block" }}><i>No languages to show.</i></span> :
                                    <Grid item xs={12}>
                                        <div className={styles.levelDiv}><span className={styles.levelOfKnowledge}>Level of knowledge</span></div>
                                        {props.skipperProfile!.listOfLanguages!.map(language => (
                                            <div key={language.id} className={styles.languageHolder}>
                                                <LanguageComponent disabled={true} className={styles.marginTop} handleLevelChange={handleLevelChange} key={language.label} language={language} />
                                            </div>
                                        ))}
                                    </Grid>
                                }
                            </>
                            }
                        </Grid>

                        {editLanguage && <Grid item xs={12} sm={6}>
                            <FormControl className={styles.formSelect} style={{ marginLeft: 25, marginTop: 20 }}>
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
                        </Grid>}
                        <Grid item xs={12} className={styles.divider}>
                            <Divider />
                        </Grid>
                        <Grid container item xs={12}>
                            <Grid item xs={12}>{showEditBtns && <span className={styles.about}>Availability <i className={"fas fa-pen " + styles.icon} onClick={() => { props.setActiveTab(2); props.history.push(CLIENT.SKIPPER.AVAILABILITY); }}></i></span>}</Grid>
                            <Grid item xs={12}>
                                <style>{calendarStyle}</style>
                                <DayPicker
                                    numberOfMonths={2}
                                    selectedDays={available}
                                    disabledDays={[...booked]}
                                    modifiers={modifiers}
                                    renderDay={renderDay}
                                />
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            </div>
        </div >
    );
}

export default SkipperProfileComponent;