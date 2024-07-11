import { Button } from "react-bootstrap";
import { useAppDispatch, useAppSelector } from "../hooks/redux";
import { fetchUser, logoutUser } from "../store/reducers/ActionCreators";
import { loanSlice } from "../store/reducers/LoanSlice";
import { useEffect } from "react";

export default function UserPage(): React.JSX.Element {
    const user = useAppSelector(item => item.user.info);
    const dispatcher = useAppDispatch();

    useEffect(() => {
        dispatcher(fetchUser())
    }, [])

    const logoutHandler = () => dispatcher(logoutUser());
    return (
    <div>
        <p>email: {user?.email}</p>
        <Button onClick={logoutHandler}>Выйти</Button>
        <Button onClick={() => {
            dispatcher(loanSlice.actions.loansFetchingSuccess([
                {
                    beforeTime: 'asdasd',
                    clientUuid: 'asdasdas1231231d',
                    creditor: {
                        name: 'asdasd',
                        patronymic: 'asdasdas',
                        surname: 'asdasdas'
                    },
                    moneyAmount: 123123,
                    openTime: 'asdasdasd',
                    uuid: 'asddsfsdf'
                }
            ]))
        }}>Займ</Button>
    </div>
    );
}