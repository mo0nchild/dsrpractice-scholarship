import { Button } from "react-bootstrap";
import TextInput from "../components/TextInput";
import React, { useEffect } from "react";
import style from './css/Registration.module.css'
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../hooks/redux";
import ToastError from "../components/ToastError";
import { registrateUser } from "../store/reducers/ActionCreators";
import { userSlice } from "../store/reducers/UserSlice";
import ProgressLoading from "../components/ProgressLoading";

export default function RegistrationPage(): React.JSX.Element {
    const emailRef = React.createRef<HTMLInputElement>();
    const passwordRef = React.createRef<HTMLInputElement>();
    const nameRef = React.createRef<HTMLInputElement>();
    
    const {info, error, isLoading} = useAppSelector(item => item.user);
    const dispatcher = useAppDispatch();
    const navigate = useNavigate();
    useEffect(() => {
        if(info != null) navigate('/user')
    }, [info])
    const registrationHandler = async () => {
        await dispatcher(registrateUser({
            email: emailRef.current!.value,
            name: nameRef.current!.value,
            password: passwordRef.current!.value
        }))
    }
    const hideErrorHandler = async () => await dispatcher(userSlice.actions.clearError());
    return (
    <div className={style['main-content']}>
        <ProgressLoading loading={isLoading}/>
        { error != null ? <ToastError errorMessage={error} onClose={hideErrorHandler}/> : null }
        <div className={style['auth-panel']}>
            <h1>Регистрация</h1>
            <TextInput title='Почта:' type='text' placeholder='Введите вашу почту'
                ref={emailRef}/>
            <TextInput title='Пароль:' type='password' placeholder='Введите пароль'
                ref={passwordRef}/>
            <div className={style['panel-divider']}></div>
            <TextInput title='Имя пользователя:' type='text' placeholder='Введите ваше имя'
                ref={nameRef}/>
            <div style={{margin: '20px 0px 0px', width: '100%'}}>
                <Button onClick={registrationHandler}>Зарегистрироваться</Button>
            </div>
        </div>
    </div>
    )
}