import { useState, useEffect } from 'react';
import PropTypes from 'prop-types';

const FindUserId = ({ token, displayUserId, displayUsername, className, especialText }) => {
  const [userId, setUserId] = useState(null);
  const [userName, setUserName] = useState(null);

  const URLId = `${import.meta.env.VITE_USER_ID}`;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(URLId, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        if (!response.ok) {
          throw new Error("Error en la solicitud");
        }
        const data = await response.json();
        setUserId(data.userId);
        setUserName(data.username);
      } catch (error) {
        console.log(error);
      }
    };

    fetchData();
  }, [URLId, token]);

  return (
    <>
      {userId !== null && userName !== null && (
        <p className={className}>
          {especialText} {displayUserId ? userId : ''} {displayUsername ? userName : ''}
        </p>
      )}
    </>
  );
};

FindUserId.propTypes = {
  token: PropTypes.string.isRequired,
  displayUserId: PropTypes.bool,
  displayUsername: PropTypes.bool,
  className: PropTypes.string,
  especialText: PropTypes.string,
};

FindUserId.defaultProps = {
  displayUserId: true,
  displayUsername: true,
  className: '',
  especialText: '',
};

export default FindUserId;