import { Button } from 'react-bootstrap'
import TextInput from '../components/TextInput'
import style from './css/Authorization.module.css'
import { Link, useNavigate } from 'react-router-dom';
import { createRef, useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../hooks/redux';
import { loginUser } from '../store/reducers/ActionCreators';
import ToastError from '../components/ToastError';
import { userSlice } from '../store/reducers/UserSlice';
import ProgressLoading from '../components/ProgressLoading';

export default function AuthorizationPage(): React.JSX.Element {
    const emailRef = createRef<HTMLInputElement>();
    const passwordRef = createRef<HTMLInputElement>();

    const {info, error, isLoading} = useAppSelector(item => item.user);
    const dispatcher = useAppDispatch();
    const navigate = useNavigate();
    useEffect(() => {
        if(info != null) navigate('/user')
    }, [info])
    const loginHandler = async () => {
        await dispatcher(loginUser(emailRef.current!.value, passwordRef.current!.value));
    }
    const hideErrorHandler = async () => await dispatcher(userSlice.actions.clearError());
    return (
    <div className={style['main-content']}>
        <ProgressLoading loading={isLoading}/>
        { error != null ? <ToastError errorMessage={error} onClose={hideErrorHandler}/> : null }
        <div className={style['auth-panel']}>
            <h1>Авторизация</h1>
            <TextInput title='Почта:' type='text' placeholder='Введите вашу почту'
                ref={emailRef}/>
            <TextInput title='Пароль:' type='password' placeholder='Введите пароль'
                ref={passwordRef}/>
            <span style={{fontSize: '14px'}}>
                Нету аккаунта? &nbsp;
                <Link to='/registration' style={{textDecoration: 'underline'}}>Зарегистрируйтесь</Link>
            </span>
            <div style={{margin: '20px 0px 0px', width: '100%'}}>
                <Button onClick={loginHandler}>Войти в профиль</Button>
            </div>
        </div>
    </div>
    )
} 