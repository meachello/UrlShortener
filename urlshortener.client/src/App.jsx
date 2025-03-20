import { useEffect, useState } from 'react';
import {
   RouterProvider,
   Route,
   Routes,
   Router,
   BrowserRouter,
   Navigate
} from 'react-router'
import './App.css';
import UrlTable from './components/UrlTable';
import './components/UrlTable.css';
import Layout from './components/Layout';
import './components/Layout.css'
import Login from './components/Login';
import About from './components/About'
import ShortUrlInfo from './components/ShortUrlInfo';
import PrivateRoute from './components/PrivateRoute';

function App() {
    return (
        <BrowserRouter>
        <Layout>
            <Routes>
                <Route path="/" element={<UrlTable />} />
                <Route path="login" element={<Login />} />
                <Route path="about" element={<About />} />
                <Route 
                    path="url/:id" 
                    element={
                        <PrivateRoute>
                            <ShortUrlInfo />
                        </PrivateRoute>
                    } 
                />
                <Route path="*" element={<Navigate to="/" />} />
            </Routes>
        </Layout>    
        </BrowserRouter>
    )
    // const [forecasts, setForecasts] = useState();

    // useEffect(() => {
    //     populateWeatherData();
    // }, []);

    // const contents = forecasts === undefined
    //     ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    //     : <table className="table table-striped" aria-labelledby="tableLabel">
    //         <thead>
    //             <tr>
    //                 <th>Date</th>
    //                 <th>Temp. (C)</th>
    //                 <th>Temp. (F)</th>
    //                 <th>Summary</th>
    //             </tr>
    //         </thead>
    //         <tbody>
    //             {forecasts.map(forecast =>
    //                 <tr key={forecast.date}>
    //                     <td>{forecast.date}</td>
    //                     <td>{forecast.temperatureC}</td>
    //                     <td>{forecast.temperatureF}</td>
    //                     <td>{forecast.summary}</td>
    //                 </tr>
    //             )}
    //         </tbody>
    //     </table>;

    // return (
    //     <div>
    //         <h1 id="tableLabel">Weather forecast</h1>
    //         <p>This component demonstrates fetching data from the server.</p>
    //         {contents}
    //     </div>
    // );
    
    // async function populateWeatherData() {
    //     const response = await fetch('weatherforecast');
    //     if (response.ok) {
    //         const data = await response.json();
    //         setForecasts(data);
    //     }
    // }
}

export default App;