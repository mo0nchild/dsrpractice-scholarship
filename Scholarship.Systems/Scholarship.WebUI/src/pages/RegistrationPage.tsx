import { Button } from "react-bootstrap";
import TextInput from "../components/TextInput";
import React from "react";
import style from './css/Registration.module.css'

export default function RegistrationPage(): React.JSX.Element {
    const emailRef = React.createRef<HTMLInputElement>();
    const passwordRef = React.createRef<HTMLInputElement>();
    const nameRef = React.createRef<HTMLInputElement>();
    return (
    <div className={style['main-content']}>
        <div className={style['auth-panel']}>
            <h1>Регистрация</h1>
            <TextInput title='Почта:' type='text' placeholder='Введите вашу почту'
                ref={emailRef}/>
            <TextInput title='Пароль:' type='password' placeholder='Введите пароль'
                ref={passwordRef}/>
            <div className={style['panel-divider']}></div>
            <TextInput title='Имя пользователя:' type='password' placeholder='Введите ваше имя'
                ref={nameRef}/>
            <div style={{margin: '20px 0px 0px', width: '100%'}}>
                <Button>Зарегистрироваться</Button>
            </div>
        </div>
    </div>
    )
}