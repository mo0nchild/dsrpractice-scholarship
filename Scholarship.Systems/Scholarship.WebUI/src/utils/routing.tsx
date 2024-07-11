import RegistrationPage from "../pages/RegistrationPage";
import AuthorizationPage from "../pages/AuthorizationPage";
import UserPage from "../pages/UserPage";

export interface RouterInfo {
	path: string;
	element: React.ReactNode;
	secure?: boolean;
}

export const loginPath = '/'

export const routers: RouterInfo[] = [
	{
		path: '/registration',
		element: <RegistrationPage />,
	},
	{
		path: loginPath,
		element: <AuthorizationPage />,
	},
	{
		path: '/user',
		element: <UserPage />,
		secure: true
	}
];