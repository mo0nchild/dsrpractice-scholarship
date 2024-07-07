import { RouterProvider } from 'react-router-dom';
import { router } from './utils/routing';

import Header from './components/Header';
import Footer from './components/Footer';

import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css'

export default function App(): React.JSX.Element {
	
	return (
		<div className={'page-content'}>
			<Header/>
			<div style={{flexGrow: '1', marginTop: '70px', color: '#FFF'}}>
				<RouterProvider router={router} />
			</div>
			<Footer />
		</div>
	)
}


