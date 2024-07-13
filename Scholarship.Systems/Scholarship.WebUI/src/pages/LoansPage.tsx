import { createRef, useEffect, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../hooks/redux';
import style from './css/LoansPage.module.css'
import { addLoan, closeLoan, fetchClosedLoans, fetchLoans } from '../store/reducers/ActionCreators';
import { Form, Tab, Tabs } from 'react-bootstrap';
import TableDisplay, { HeaderInfo } from '../components/TableDisplay';
import ModalWindow from '../components/ModalWindow';
import { Button } from 'react-bootstrap';
import ToastError from '../components/ToastError';
import ProgressLoading from '../components/ProgressLoading';
import TextInput from '../components/TextInput';

const loansTableHeader: HeaderInfo[] = [
    { key: 'creditor', text: 'ФИО кредитора', transform: (value) => {
        return `${value.surname} ${value.name} ${value.patronymic}`
    } },
    { key: 'moneyAmount', text: 'Сумма', transform: (value) => `${value} руб` },
    { key: 'openTime', text: 'От даты' },
    { key: 'beforeTime', text: 'До даты ' },
]

export default function LoansPage(): React.JSX.Element {
    const { list, error, isLoading } = useAppSelector(item => item.loan);
    const dispatcher = useAppDispatch();

    const [tabKey, setTabKey] = useState('opened');
    useEffect(() => {
        dispatcher(fetchLoans());
        setTabKey('opened');
    }, [])
    

    const tabSelectHandler = (key: string | null) => {
        switch (key) {
            case 'opened': dispatcher(fetchLoans()); break;
            case 'closed': dispatcher(fetchClosedLoans()); break;
            default: return;
        }
        setTabKey(key!)
    }
    const tableSelectHandler = (index: number) => {
        setCurrent(index);
    }
    const closeLoanHandler = (uuid: string) => {
        setCurrent(null);
        dispatcher(closeLoan({
            closeTime: new Date().toISOString().split('T')[0],
            loanUuid: uuid
        }))
    }
    const summaryRef = createRef<HTMLInputElement>();
    const startDate = createRef<HTMLInputElement>();
    const beforeDate = createRef<HTMLInputElement>();

    const surnameRef = createRef<HTMLInputElement>();
    const nameRef = createRef<HTMLInputElement>();
    const patronymicRef = createRef<HTMLInputElement>();
    const addLoanHandler = (): boolean => {
        if(startDate.current!.value == '' || beforeDate.current!.value == '') return false;
        dispatcher(addLoan({
            beforeTime: beforeDate.current!.value,
            openTime: startDate.current!.value,
            moneyAmount: Number.parseInt(summaryRef.current!.value),
            creditorSurname: surnameRef.current!.value,
            creditorPatronymic: patronymicRef.current!.value,
            creditorName: nameRef.current!.value
        }))
        return true;
    }
    const [current, setCurrent] = useState<number | null>(null)
    const [adding, setAdding] = useState(false);
    useEffect(() => {
        summaryRef.current!.value = summaryRef.current!.defaultValue
        startDate.current!.value = startDate.current!.defaultValue
        beforeDate.current!.value = beforeDate.current!.defaultValue
        surnameRef.current!.value = surnameRef.current!.defaultValue
        nameRef.current!.value = nameRef.current!.defaultValue
        patronymicRef.current!.value = patronymicRef.current!.defaultValue
    }, [adding])
    return (
    <div className={style['loans-content']}>
        <ProgressLoading loading={isLoading}/>
        { error != null ? <ToastError errorMessage={error}/> : null }
        <ModalWindow isOpen={current != null} onClose={() => setCurrent(null)}>
        {
        current == null ? null : 
        <div style={{textAlign: 'start'}}>
            <p style={{marginBottom: '10px'}}>
                ФИО кредитора: &nbsp;
                <span>
                {list[current!].creditor.surname}&nbsp;
                {list[current!].creditor.name}&nbsp;
                {list[current!].creditor.patronymic}
                </span>
            </p>
            <p>Заим был оформлен: {list[current].openTime}</p>
            <p style={{marginBottom: '10px'}}>Заим оформлен до: {list[current].beforeTime}</p>
            <p>На сумму: {list[current].moneyAmount} руб</p>

            <div style={{marginTop: '30px'}}>
                <Button variant='outline-light' onClick={() => {
                    closeLoanHandler(list[current!].uuid)
                    setCurrent(null)
                }}>Закрыть заим</Button>
            </div>
        </div>
        }
        </ModalWindow>
        <ModalWindow isOpen={adding} onClose={() => setAdding(false)}>
            <div style={{marginBottom: '10px'}}>
                <Form.Label style={{width: '100%', textAlign: 'start'}}>Сумма: (руб)</Form.Label>
                <Form.Control ref={summaryRef} type='number' defaultValue={100} min={0} max={10000}/>
            </div>
            <div style={{marginBottom: '10px', display: 'flex', flexFlow: 'row'}}>
                <div style={{margin: '0px 5px'}}>
                    <Form.Label style={{width: '100%', textAlign: 'start'}}>От: </Form.Label>
                    <Form.Control ref={startDate} type='date' defaultValue={1} min={0} max={10000}/>
                </div>
                <div style={{margin: '0px 5px'}}>
                    <Form.Label style={{width: '100%', textAlign: 'start'}}>До: </Form.Label>
                    <Form.Control ref={beforeDate} type='date' defaultValue={1} min={0} max={10000}/>
                </div>
            </div>
            <TextInput title='Фамилия кредитора:' type='text' placeholder='Введите фамилию кредитора'
                ref={surnameRef}/>
            <TextInput title='Имя кредитора:' type='text' placeholder='Введите имя кредитора'
                ref={nameRef}/>
            <TextInput title='Отчество кредитора:' type='text' placeholder='Введите отчество кредитора'
                ref={patronymicRef}/>
            <Button variant='outline-light' onClick={() => {
                if(addLoanHandler()) setAdding(false)
            }}>Отправить</Button>
        </ModalWindow>
        <Tabs activeKey={tabKey} onSelect={tabSelectHandler} className={style['loans-tabs']}>
            <Tab eventKey="opened" title="Открытые" className={style['loans-panel']}>
                <div>
                    <TableDisplay header={loansTableHeader} content={list.map(i => {
                        let currentColor = 'transparent';
                        const currentDate = new Date();
                        currentDate.setDate(currentDate.getDate() + 7);
                        const daysUntilNextWeek = 7 - currentDate.getDay();
                        const nextWeek = new Date(currentDate);
                        nextWeek.setDate(currentDate.getDate() + daysUntilNextWeek);
                        nextWeek.setHours(0, 0, 0, 0);
                        if(Date.parse(i.beforeTime) < Date.now()) currentColor = '#ff3c19'
                        else if(Date.parse(i.beforeTime) < nextWeek.getTime() && Date.parse(i.beforeTime) >= Date.now()) {
                            currentColor = '#ffbd59'
                        }
                        return {
                            color: currentColor,
                            data: i
                        }
                    })} onSelect={tableSelectHandler}/>
                    
                </div>
                <Button variant='outline-light' onClick={() => setAdding(true)}>Открыть займ</Button>
            </Tab>
            <Tab eventKey="closed" title="Закрытые" className={style['loans-panel']}>
                <div>
                    <TableDisplay header={loansTableHeader} content={list.map(i => ({
                        color: 'transparent',
                        data: i
                    }))}/>
                </div>
                <Button variant='outline-light' onClick={() => setAdding(true)}>Открыть займ</Button>
            </Tab>
        </Tabs>
    </div>
    );
}