import React from 'react';
import { Navigate } from 'react-router';
import { getCurrentUser } from './Services';

const PrivateRoute = ({ children, adminOnly = false }) => {
  const user = getCurrentUser();
  
  if (!user) {
    return <Navigate to="/login" />;
  }
  
  if (adminOnly && !user.isAdmin) {
    return <Navigate to="/" />;
  }
  
  return children;
};

export default PrivateRoute;