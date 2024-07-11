import { Navigate, Route, Routes } from 'react-router-dom';
import { loginPath, routers } from './utils/routing';

import Header from './components/Header';
import Footer from './components/Footer';
import { useAppSelector } from './hooks/redux';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css'

export default function App(): React.JSX.Element {
	// const token = useLocalStorage(accessTokenKey);
	const user = useAppSelector(item => item.user.info);
	return (
	<div className={'page-content'}>
		<Header/>
		<div style={{flexGrow: '1', marginTop: '70px', color: '#FFF'}}>
			<Routes>
			{routers.map((route, index) => {
				if (route.secure) {
					const privateElement = user == null ? <Navigate to={loginPath}/> : route.element;
					return <Route key={index} path={route.path} element={privateElement}/>;
				} 
				return <Route key={index} {...route}/>;
			})}
			</Routes>
		</div>
		<Footer/>
	</div>
	)
}


