import { Link } from 'react-router-dom';
import style from './css/Header.module.css'
import { useAppSelector } from '../hooks/redux';

export default function Header() : React.JSX.Element {
    const userInfo = useAppSelector(item => item.user.info);
    return (
        <div className={style['header-content']}>
            <p>Учет займов</p>
            <div>
            { 
                userInfo != null 
                    ? <Link to={'/user'}>{userInfo.name}</Link>
                    : <Link to={'/'}>Войти в профиль</Link>
            }
            </div>
        </div>
    );
}