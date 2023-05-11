import React, { useState } from 'react'
import axios from "axios"
import { useNavigate } from 'react-router-dom'
import "../login/Loginstyle.css"
const Login = () => {
  const[Email,setEmail]=useState("")
  const[Password,setPassword]=useState("")
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");
  const navigate=useNavigate();
  const handleChangeEmail=(e)=>{
    setEmail(e.target.value)
  }
  const HandleChangePassword=(e)=>{
    setPassword(e.target.value);
  }
  const HandleSubmitLogin=(e)=>{
    e.preventDefault();
    axios.post("https://localhost:44318/api/Auths/Login",{
    Email:Email,
    Password:Password
  }).then(()=>{
    setSuccessMessage("You Are Logged Successfully .... :)")
    navigate("/home")



  }).catch(() => {
         setErrorMessage("Your Email or Password Is Not Correct");
  });

  }

  return (
    <div className='container'>
      <h1>Login</h1>

      <form onSubmit={HandleSubmitLogin}> 
        <div className='col'>
          <label className='emaillbl'>Email : </label>
          <input type='text' value={Email} onChange={handleChangeEmail}/>
        </div>
        <div className='col'>
          <label>Password : </label>
          <input type='password' value={Password} onChange={HandleChangePassword}/>
        </div>
        <div className='btn'>
        <button className='btn_submit' type='submit'>Login</button>
        </div>

      </form>
      {errorMessage && <div className='error'>{errorMessage}</div>}
     {successMessage && <div className='success'>{successMessage}</div>}


    </div>
  )
}

export default Login