import React from 'react'
import "../src/App.css"
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from '../src/component/login/Login';
import Regsiter from '../src/component/regsiter/Regsiter';
import Home from './component/home/Home';
const App = () => {
  
  return (
    <div className='App'>
      <Router>
        <Routes>
          <Route path='/' element={<Regsiter/>}></Route>
          <Route path='/login' element={<Login/>}></Route>
          <Route path='/home' element={<Home/>}></Route>

        </Routes>
      </Router>
      
      

      
    </div>
  )
}

export default App
