import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router';
import { getAllUrls, createUrl, deleteUrl } from './Services';
import { getCurrentUser } from './Services';

const UrlTable = () => {
  const [urls, setUrls] = useState([]);
  const [newUrl, setNewUrl] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const user = getCurrentUser();
  
  useEffect(() => {
    fetchUrls();
  }, []);
  
  const fetchUrls = async () => {
    try {
      const data = await getAllUrls();
      setUrls(data);
    } catch (err) {
      console.error('Error fetching URLs:', err);
    }
  };
  
  const handleCreateUrl = async (e) => {
    e.preventDefault();
    setError('');
    
    try {
      const createdUrl = await createUrl(newUrl);
      setUrls([...urls, createdUrl]);
      setNewUrl('');
    } catch (err) {
      if (err.response && err.response.data) {
        setError(err.response.data);
      } else {
        setError('An error occurred while creating the URL');
      }
    }
  };
  
  const handleDeleteUrl = async (id) => {
    try {
      await deleteUrl(id);
      setUrls(urls.filter(url => url.id !== id));
    } catch (err) {
      console.error('Error deleting URL:', err);
    }
  };
  
  const handleViewDetails = (id) => {
    navigate(`/url/${id}`);
  };
  
  const canDeleteUrl = (url) => {
    return user && (user.isAdmin || url.createdById === user.id);
  };
  
  return (
    <div>
      <h2>Short URLs</h2>
      
      {user && (
        <div style={{ marginBottom: '20px' }}>
          <h3>Add New URL</h3>
          {error && <p style={{ color: 'red' }}>{error}</p>}
          <form onSubmit={handleCreateUrl}>
            <div style={{ display: 'flex' }}>
              <input
                type="url"
                value={newUrl}
                onChange={(e) => setNewUrl(e.target.value)}
                placeholder="Enter URL to shorten"
                required
                style={{ flex: 1, marginRight: '10px' }}
              />
              <button type="submit">Shorten</button>
            </div>
          </form>
        </div>
      )}
      
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            <th style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>Original URL</th>
            <th style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>Short URL</th>
            <th style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {urls.map(url => (
            <tr key={url.id}>
              <td style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>
                {url.url}
              </td>
              <td style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>
                <a href={`https://localhost:7198/api/url/redirect/${url.shortCode}`} target="_blank" rel="noopener noreferrer">
                  {url.shortCode}
                </a>
              </td>
              <td style={{ textAlign: 'left', padding: '8px', borderBottom: '1px solid #ddd' }}>
                {user && (
                  <button onClick={() => handleViewDetails(url.id)} style={{ marginRight: '5px' }}>
                    View
                  </button>
                )}
                {canDeleteUrl(url) && (
                  <button onClick={() => handleDeleteUrl(url.id)}>Delete</button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UrlTable;