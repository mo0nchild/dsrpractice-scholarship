import { useState } from "react";
import { Toast, ToastContainer } from "react-bootstrap";

import style from './css/ToastError.module.css'

export interface ToastErrorProps {
    errorMessage: string;
}

export default function ToastError(props: ToastErrorProps): React.JSX.Element {
    const [show, setShow] = useState(true);
    return (
    <ToastContainer className='p-3' position={'bottom-end'} style={{ zIndex: 1 }}>
        <Toast className={style['toast']} bg={'dark'} show={show} onClose={() => setShow(false)}>
            <Toast.Header className={style['toast-header']}>
                <strong className='me-auto'>Произошла ошибка</strong>
            </Toast.Header>
            <Toast.Body className={style['toast-body']}>{props.errorMessage}</Toast.Body>
        </Toast>
    </ToastContainer>
    )
}