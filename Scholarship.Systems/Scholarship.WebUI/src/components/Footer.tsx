import style from './css/Footer.module.css'

export default function Footer(): React.JSX.Element {
    return (
    <footer className={style['footer-content']}>
        <div className="text-center p-3">
          © 2024 Copyright:&nbsp;
          <a className="text-light" href="">Scholarship.com</a>
        </div>
    </footer>
    )
}