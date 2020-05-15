import React, { useState } from "react";
import { SkipperRegistrationStepData } from "../../../types/SkipperRegistrationContextProps";
import styles from './styles.module.scss';
import mainStyles from '../../ui/main.module.scss';
import { Grid, Chip, Input, InputLabel, FormControl, MenuItem, ListItemText, Select as MaterialSelect, Checkbox } from "@material-ui/core";
import { ISkill } from './../../../types/ISkill';
import { ILanguage } from './../../../types/ILanguage';
import LanguageComponent from './../../ui/LanguageComponent';
import Select from 'react-select';
import SkipperStepper from '../../shared/skipperStepper';

interface IProps {
  saveStep4: (values: any) => void,
  values: SkipperRegistrationStepData,
  goBack: () => void,
  skills: ISkill[],
  languages?: ILanguage[]
}

export const SkipperRegistrationFormStep4: React.FC<IProps> = (props) => {

  const [listOfSkills, setlistOfSkills] = useState<ISkill[]>([]);
  const [listOfLanguages, setlistOfLanguages] = useState<ILanguage[]>([]);

  const showSkillsChips = !(listOfSkills.length === 0);
  const showLanguagessChips = !(listOfLanguages.length === 0);

  const handleSubmit = (event: any) => {
    event.preventDefault();
    props.saveStep4({ listOfSkills: listOfSkills, listOfLanguages: listOfLanguages });
  }

  const handleLevelChange = (languageID: number, levelOfKnowledge: number) => {
    var tempArray = listOfLanguages.slice();
    var index = tempArray.findIndex(l => l.id === languageID);
    var tempLanguage = tempArray[index];
    tempLanguage.levelOfKnowledge = levelOfKnowledge;
    tempArray.splice(index, 1, tempLanguage);
    setlistOfLanguages(tempArray);
  }

  const handleSkillChange = (event: any) => {
    var tempArray = listOfSkills.slice();
    var tempSkill = {
      name: event.target.value[0].split(":")[0],
      id: event.target.value[0].split(":")[1],
      icon: event.target.value[0].split(":")[2],
    }
    if (tempArray.some(s => s.id === tempSkill.id)) {
      var index = tempArray.findIndex(i => i.id === tempSkill.id);
      tempArray.splice(index, 1);
    }
    else {
      tempArray.push(tempSkill);
    }
    setlistOfSkills(tempArray);
  }

  const handleLanguageAuto = (event: any) => {
    var tempArray = listOfLanguages.slice();
    var tempLanguage = {
      label: event[0].label,
      id: event[0].id,
      levelOfKnowledge: 0
    }
    if (tempArray.some(s => s.id === tempLanguage.id)) {
      var index = tempArray.findIndex(i => i.id === tempLanguage.id);
      tempArray.splice(index, 1);
    }
    else {
      tempArray.push(tempLanguage);
    }
    setlistOfLanguages(tempArray);
  }

  const ITEM_HEIGHT = 48;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    style: {
      maxHeight: ITEM_HEIGHT * 7.5 + ITEM_PADDING_TOP,
      width: 250,
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <span className={styles.headline}></span>
        <Grid container spacing={4} direction="column">
          <span className={styles.descriptionSmall}>Skills</span>
          <Grid item xs={12} style={{ width: 300 }}>
            <FormControl className={styles.formSelect} >
              <InputLabel id="demo-mutiple-checkbox-label">Select a skill</InputLabel>
              <MaterialSelect
                labelId="demo-mutiple-checkbox-label"
                id="demo-mutiple-checkbox"
                multiple
                value={[]}
                onChange={handleSkillChange}
                input={<Input />}
                MenuProps={MenuProps}
              >
                {props.skills.map(skill => (
                  <MenuItem key={skill.name} value={skill.name + ":" + skill.id + ":" + skill.icon}>
                    <Checkbox style={{ color: "#406e8e" }} checked={listOfSkills.some(s => s.name === skill.name)} />
                    <ListItemText primary={skill.name} />
                  </MenuItem>
                ))}
              </MaterialSelect>
            </FormControl>
          </Grid>
          <Grid item xs={12} style={{ width: 600 }}>
            {showSkillsChips && <div>
              {listOfSkills.map(skill => (
                <Chip style={{ backgroundColor: "#406e8e" }} icon={<i className={skill.icon}></i>} className={styles.chip} key={skill.name} label={skill.name} />
              ))}
            </div>}
          </Grid>
          <span className={styles.descriptionSmall}>Languages</span>
          <Grid item xs={12} style={{ width: 300 }}>
            <FormControl className={styles.formSelect}>
              <Select
                className={styles.autoCompleteSelect_reg}
                value={[]}
                id="react-select-multi"
                isMulti
                placeholder="Type a language..."
                options={props.languages}
                onChange={e => handleLanguageAuto(e)}
              >
              </Select>
            </FormControl>
          </Grid>
          <Grid item xs={12} sm={6}>
            {showLanguagessChips && <div>
              <div className={styles.languageSpan}>
                Level of knowledge
              </div>
              {listOfLanguages.map(language => (
                <LanguageComponent disabled={false} key={language.label} handleLevelChange={handleLevelChange} language={language} />
              ))}
            </div>}
          </Grid>
          <SkipperStepper>
            <Grid container item xs={12} justify="flex-end" style={{ marginTop: 13 }}>
              <Grid item xs={6}>
                <button
                  type="button" onClick={props.goBack} className={mainStyles.backButton} style={{ float: "left" }}>
                  Back
              </button>
              </Grid>
              <Grid item xs={6}>
                <button type="submit" className={mainStyles.submitButton}>
                  Finish
              </button>
              </Grid>
            </Grid>
          </SkipperStepper>
        </Grid>
      </form>
    </div >)
};

export default SkipperRegistrationFormStep4;