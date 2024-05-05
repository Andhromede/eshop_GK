import './App.css';
import Routes from "./routes/routes";
import { BrowserRouter, Route } from "react-router-dom";
import Navbar from "./components/layouts/Navbar";
import "./assets/styles/base/generalCss.css";
// import Footer from "./components/layouts/Footer";
import { Provider } from 'react-redux';
import { store } from './redux-store/store';

function App() {

   return (
      <Provider store={store}>
         <BrowserRouter>
            {/* <div className="background  bg-image flex h-full min-h-screen cursor-default relative flex-col bg-zinc-800"> */}
            <div className="background bg-fixed bg-image flex h-full min-h-screen cursor-default relative flex-col bg-zinc-800">
               <Navbar />
               <Routes />
            </div>
         </BrowserRouter>
      </Provider>
   );
}

export default App;
