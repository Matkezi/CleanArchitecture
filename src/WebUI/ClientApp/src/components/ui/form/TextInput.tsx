import React, { ChangeEvent } from "react";
import styles from './styles.module.scss';

interface IProps {
    handleChange?: (e: ChangeEvent<HTMLInputElement>) => void,
    handleBlur?: (e: ChangeEvent<HTMLInputElement>) => void,
    value: string,
    errors?: any,
    id: string,
    touched?: any,
    label?: string,
    placeholder: string,
    type?: string
    disabled?: boolean
}

export const TextInput: React.SFC<IProps> = (props) => (
    <React.Fragment>
        <label htmlFor="firstName" style={{ display: "block" }}>
            {props.label}
        </label>
        <input
            id={props.id}
            placeholder={props.placeholder}
            value={props.value}
            onChange={props.handleChange}
            onBlur={props.handleBlur}
            type={props.type ? props.type : 'text'}
            disabled={props.disabled === true}
            className={
                props.errors[props.id] && props.touched[props.id]
                    ? "text-input error"
                    : "text-input"
                    + ' ' + styles.textInput
            }
        />
        {props.errors[props.id] && props.touched[props.id] && (
            <div className="input-feedback">{props.errors[props.id]}</div>
        )}
    </React.Fragment>
)

export default TextInput;