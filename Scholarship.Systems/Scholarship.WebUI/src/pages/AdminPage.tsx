import { Button, Form } from 'react-bootstrap'
import style from './css/AdminPage.module.css'
import { adminService } from '../services/AdminService'
import { createRef } from 'react'

export default function AdminPage(): React.JSX.Element {
    const fileRef = createRef<HTMLInputElement>()
    const saveBackupHandler = async () => {
        try {
            const link = document.createElement("a");
            const data = (await adminService.getBackup()).data;
            const url = window.URL.createObjectURL(data);

            link.href = url;
            link.download = 'backup.xml';
            link.click();
            window.URL.revokeObjectURL(url);
        }
        catch (error) {
            console.log(error)
        }
    }
    const loadBackupHandler = async () => {
        try {
            if(!fileRef.current!.value) return;
            await adminService.loadBackup(fileRef.current!.files![0])
            window.alert('Backup успешно загружен')
        }
        catch (error) {
            console.log(error)
        }
    } 
    return (
    <div className={style['main-content']}>
        <div className={style['admin-panel']}>
            <h1 style={{margin: '0px 0px 40px'}}>Панель админа:</h1>
            <div style={{marginBottom: '10px', display: 'flex', flexFlow: 'row'}}>
                <Form.Control ref={fileRef} type='file' accept="text/xml" style={{width: '60%', marginRight: '10px'}}/>
                <Button variant='outline-light' onClick={loadBackupHandler}>Загрузить Backup</Button>
            </div>
            <div style={{marginBottom: '10px'}}>
                <Button style={{width: '60%'}} variant='outline-light' 
                    onClick={saveBackupHandler}>Скачать Backup</Button>
            </div>
        </div>
    </div>
    )
}