import { Link, useNavigate} from 'react-router';
import { getCurrentUser, logout } from './Services';

const Layout = ({children}) => {
    const navigate = useNavigate();
    const user = getCurrentUser();

    const handleLogout = () =>{
        logout();
        navigate('/login')
    };

    return (
        <div>
        <nav>
          <ul className='list'>
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/about">About</Link>
            </li>
            {user ? (
              <>
                <li>
                  <span>{user.username}</span>
                </li>
                <li>
                  <button onClick={handleLogout}>Logout</button>
                </li>
              </>
            ) : (
              <li>
                <Link to="/login">Login</Link>
              </li>
            )}
          </ul>
        </nav>
        <main>{children}</main>
      </div>
    )
}

export default Layout;