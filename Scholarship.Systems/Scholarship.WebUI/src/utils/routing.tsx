import RegistrationPage from "../pages/RegistrationPage";
import AuthorizationPage from "../pages/AuthorizationPage";
import UserPage from "../pages/UserPage";
import LoansPage from "../pages/LoansPage";
import AdminPage from "../pages/AdminPage";

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
	},
	{
		path: '/loans',
		element: <LoansPage />,
		secure: true
	},
	{
		path: '/admin',
		element: <AdminPage />,
		secure: true
	}
];