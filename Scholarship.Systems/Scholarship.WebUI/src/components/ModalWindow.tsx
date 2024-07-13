import { createRef, ReactNode, useEffect } from 'react'
import style from './css/ModalWindow.module.css'
import { Button } from 'react-bootstrap';

export interface ModalWindowProps {
    isOpen: boolean,
    children: ReactNode,
    onClose?: () => void,
}

export default function ModalWindow(props: ModalWindowProps): React.JSX.Element {
    const { isOpen, children, onClose } = props
    const dialogRef = createRef<HTMLDialogElement>();
    useEffect(() => {
        if(isOpen) dialogRef.current?.showModal();
        else dialogRef.current?.close();
    }, [isOpen])
    return (
    <dialog ref={dialogRef} className={style['modal-window']}>
        <div className={style['modal-window-header']}>
            <Button variant='outline-light' onClick={onClose}>Закрыть</Button>

        </div>
        <div style={{width: '100%', height: '1px', backgroundColor: '#FFF'}}></div>
        <div style={{marginTop: '20px'}}>{ children }</div>
    </dialog>
    )
}