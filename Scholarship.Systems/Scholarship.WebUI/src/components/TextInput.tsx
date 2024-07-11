import style from './css/TextInput.module.css'
import { Form } from "react-bootstrap";
import React from 'react';

export interface InputProps {
    title: string;
    type: 'text' | 'password',
    placeholder?: string
}
const TextInput = React.forwardRef<HTMLInputElement,InputProps>((props, ref) => {
    const { title, type, placeholder } = props
    return (
    <div className={style['input-content']}>
        <Form.Label style={{margin: '0px'}}>{title}</Form.Label>
        <Form.Control className={style['input-control']} 
            placeholder={placeholder} type={type} ref={ref}/>
    </div>
    )
});
export default TextInput;
