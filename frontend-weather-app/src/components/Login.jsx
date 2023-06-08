import { useState, useEffect } from 'react';
import axios from 'axios';
import App from '../App';

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [token, setToken] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    const token = window.localStorage.getItem('token');
    if(token) {
      setToken(token);
    }
  }, []);

  const handleLogin = async (e) => {
    e.preventDefault();

    const data = {
      Username: username,
      Password: password
    };

    try {
      const response = await axios.post(import.meta.env.VITE_LOGIN_API, data);

      if (response.status === 200) {
        setToken(response.data.token);
        localStorage.setItem('token', response.data.token);
      }
    } catch (err) {
      setError("Error al iniciar sesión, por favor intenta de nuevo.");
    }
  };

  const handleLogout = () => {
    setToken(null);
    window.localStorage.removeItem('token');
  };

  return token ? (
    <App token={token} handleLogout={handleLogout} />
  ) : (
    <div className="bg-gray-800 h-screen grid place-items-center">
      <form onSubmit={handleLogin} className='space-y-4 md:space-y-6 card'>
      <div>
        <label htmlFor="" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Username</label>
        <input type="text" name="user" id="user" value={username} onChange={e => setUsername(e.target.value)} placeholder="Username" required className='bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'/>      
      </div>
      <div>
        <label className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
        <input type="password" value={password} onChange={e => setPassword(e.target.value)} placeholder="••••••••" required className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"/>
      </div>
      <button type="submit" className="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">
        Login</button>
      {error && <p className='block mb-2 text-sm font-medium text-gray-900 dark:text-white'>{error}</p>}
    </form>
    </div>
    
  );
};

export default Login;