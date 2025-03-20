import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router';
import { getUrlById } from './Services';

const ShortUrlInfo = () => {
  const [url, setUrl] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const { id } = useParams();
  const navigate = useNavigate();
  
  useEffect(() => {
    fetchUrlDetails();
  }, [id]);
  
  const fetchUrlDetails = async () => {
    try {
      const data = await getUrlById(id);
      setUrl(data);
      setLoading(false);
    } catch (err) {
      setError('Error fetching URL details');
      setLoading(false);
    }
  };
  
  if (loading) {
    return <div>Loading...</div>;
  }
  
  if (error) {
    return <div>{error}</div>;
  }
  
  if (!url) {
    return <div>URL not found</div>;
  }
  
  return (
    <div>
      <h2>URL Details</h2>
      <div style={{ marginBottom: '20px' }}>
        <button onClick={() => navigate('/')}>Back to List</button>
      </div>
      <div style={{ border: '1px solid #ddd', padding: '20px', borderRadius: '5px' }}>
        <p><strong>Original URL:</strong> {url.url}</p>
        <p><strong>Short Code:</strong> {url.shortCode}</p>
        <p><strong>Created By:</strong> {url.createdBy.username}</p>
        <p><strong>Created Date:</strong> {new Date(url.createdAt).toLocaleString()}</p>
        <p>
          <strong>Full Short URL:</strong> 
          <a href={`https://localhost:7198/api/url/redirect/${url.shortCode}`} target="_blank" rel="noopener noreferrer">
            {`https://localhost:7198/api/url/redirect/${url.shortCode}`}
          </a>
        </p>
      </div>
    </div>
  );
};

export default ShortUrlInfo;