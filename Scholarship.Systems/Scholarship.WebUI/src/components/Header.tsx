import { Link } from 'react-router-dom';
import style from './css/Header.module.css'
import { useAppSelector } from '../hooks/redux';

export default function Header() : React.JSX.Element {
    const userInfo = useAppSelector(item => item.user.info);
    return (
        <div className={style['header-content']}>
            <div className={style['header-navigation']}>
                <div>Учет займов</div>
                <div>
                {
                    userInfo != null 
                        ? <Link to={'/loans'}>Займы</Link>
                        : null
                }
                </div>
                <div>
                {
                    userInfo != null && userInfo.roleName.toLowerCase() == 'admin'
                        ? <Link to={'/admin'}>Админ</Link>
                        : null
                }
                </div>
            </div>
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