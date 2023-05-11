import React from 'react'
import { useState } from 'react';
import axios from "axios"
import { useNavigate } from 'react-router-dom';
import"../regsiter/Regsiterstyle.css"

const Regsiter = () => {
    const [firstname,setfirstname]=useState("");
    const [lastname,setlastname]=useState("");
    const[username,setuserame]=useState("");
    const[email,setemail]=useState("");
    const[password,setpassword]=useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [successMessage, setSuccessMessage] = useState("");
    const navigate=useNavigate();
    const handlechangefirstname=(e)=>{
     setfirstname(e.target.value)
   
    };
    const handlechangelastname=(e)=>{
      setlastname(e.target.value)
    };
    const handlechangeusername=(e)=>{
     setuserame(e.target.value)
   
    };
    const handlechangeemail=(e)=>{
     setemail(e.target.value)
   
    };
    const handlechangepassword=(e)=>{
      setpassword(e.target.value)
    };
    const handleRegister=(e)=>{
     e.preventDefault();
     axios.post('https://localhost:44318/api/Auths/Register', {
       firstname:firstname,
       lastname:lastname,
       username: username,
       password: password,
       email: email
     })
       .then(() => {
         setSuccessMessage('Registration successful!');
         navigate("/login");
       })
       .catch(() => {
         setErrorMessage('Registration failed. Please try again.');
       });
   
      }
  return (
    <div className="container">
    <h1>Register</h1>

      <form onSubmit={handleRegister} className='register'>
        <div className='col'>
        <label>First name : </label>
        <input type='text' value={firstname} onChange={handlechangefirstname}/>
        </div>
        <div className='col'>
        <label>Last name : </label>
        <input type='text' value={lastname} onChange={handlechangelastname}/>
        </div>
        <div className='col'>
        <label>Username : </label>
        <input type='text' value={username} onChange={handlechangeusername}/>
        </div>
        <div className='col'>
        <label className='emaillbl'>Email : </label>
        <input type='text' value={email} onChange={handlechangeemail}/>
        </div>
        <div className='col'>
        <label>Password : </label>
        <input type='password' value={password} onChange={handlechangepassword}/>
        </div>
        <div className='btn'>
        <button className='btn_submit' type='submit'>Register</button>
        </div>
      </form>
      {errorMessage && <div className='error'>{errorMessage}</div>}
      {successMessage && <div className='success'>{successMessage}</div>}


   
    </div>
  )

}

export default Regsiter