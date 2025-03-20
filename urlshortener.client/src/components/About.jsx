import React, { useState, useEffect } from 'react';
import { getAboutContent, updateAboutContent } from './Services';
import { getCurrentUser } from './Services';

const AboutView = () => {
  const [content, setContent] = useState('');
  const [editableContent, setEditableContent] = useState('');
  const [isEditing, setIsEditing] = useState(false);
  const [loading, setLoading] = useState(true);
  const user = getCurrentUser();
  
  useEffect(() => {
    fetchAboutContent();
  }, []);
  
  const fetchAboutContent = async () => {
    try {
      const data = await getAboutContent();
      setContent(data.content);
      setEditableContent(data.content);
      setLoading(false);
    } catch (err) {
      console.error('Error fetching about content:', err);
      setLoading(false);
    }
  };
  
  const handleSave = async () => {
    try {
      await updateAboutContent(editableContent);
      setContent(editableContent);
      setIsEditing(false);
    } catch (err) {
      console.error('Error updating about content:', err);
    }
  };
  
  if (loading) {
    return <div>Loading...</div>;
  }
  
  return (
    <div>
      <h2>About Our URL Shortener</h2>
      
      {user && user.isAdmin && !isEditing && (
        <button onClick={() => setIsEditing(true)} style={{ marginBottom: '10px' }}>
          Edit
        </button>
      )}
      
      {isEditing ? (
        <div>
          <textarea
            value={editableContent}
            onChange={(e) => setEditableContent(e.target.value)}
            style={{ width: '100%', height: '200px', marginBottom: '10px' }}
          />
          <div>
            <button onClick={handleSave} style={{ marginRight: '10px' }}>Save</button>
            <button onClick={() => {
              setEditableContent(content);
              setIsEditing(false);
            }}>Cancel</button>
          </div>
        </div>
      ) : (
        <div dangerouslySetInnerHTML={{ __html: content.replace(/\n/g, '<br>') }} />
      )}
    </div>
  );
};

export default AboutView;