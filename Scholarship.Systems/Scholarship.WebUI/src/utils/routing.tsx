import { createBrowserRouter } from "react-router-dom";
import RegistrationPage from "../pages/RegistrationPage";
import AuthorizationPage from "../pages/AuthorizationPage";

export const router = createBrowserRouter([
	{
		path: '/registration',
		element: <RegistrationPage />,
	},
	{
		path: '/',
		element: <AuthorizationPage />
	}
]);