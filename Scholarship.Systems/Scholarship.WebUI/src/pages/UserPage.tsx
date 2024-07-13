import { Button } from "react-bootstrap";
import { useAppDispatch, useAppSelector } from "../hooks/redux";
import { fetchUser, logoutUser } from "../store/reducers/ActionCreators";
import { useEffect } from "react";
import ProgressLoading from "../components/ProgressLoading";

export default function UserPage(): React.JSX.Element {
    const { info, isLoading } = useAppSelector(item => item.user);
    const dispatcher = useAppDispatch();

    useEffect(() => {
        dispatcher(fetchUser())
    }, [])

    const logoutHandler = () => dispatcher(logoutUser());
    return (
    <div>
        <ProgressLoading loading={isLoading}/>
        <p>Электронная почта: {info?.email}</p>
        <p>Имя пользователя: {info?.name}</p>
        <Button onClick={logoutHandler}>Выйти</Button>
    </div>
    );
}