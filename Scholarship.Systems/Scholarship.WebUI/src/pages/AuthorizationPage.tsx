import { Button } from 'react-bootstrap'
import TextInput from '../components/TextInput'
import style from './css/Authorization.module.css'
import React from 'react';

export default function AuthorizationPage(): React.JSX.Element {
    const emailRef = React.createRef<HTMLInputElement>();
    const passwordRef = React.createRef<HTMLInputElement>();
    
    return (
    <div className={style['main-content']}>
        <div className={style['auth-panel']}>
            <h1>Авторизация</h1>
            <TextInput title='Почта:' type='text' placeholder='Введите вашу почту'
                ref={emailRef}/>
            <TextInput title='Пароль:' type='password' placeholder='Введите пароль'
                ref={passwordRef}/>
            <span style={{fontSize: '14px'}}>
                Нету аккаунта? &nbsp;
                <a href='/registration' style={{textDecoration: 'underline'}}>Зарегистрируйтесь</a>
            </span>
            <div style={{margin: '20px 0px 0px', width: '100%'}}>
                <Button>Войти в профиль</Button>
            </div>
        </div>
    </div>
    )
} 