import style from './css/Header.module.css'

export default function Header() : React.JSX.Element {

    return (
        <div className={style['header-content']}>
            <p>Учет займов</p>
            <div>
                <a href={'/'}>Войти в профиль</a>
            </div>
        </div>
    );
}