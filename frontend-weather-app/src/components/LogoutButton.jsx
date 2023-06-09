const LogoutButton = ({ handleLogout }) => {
  return (
    <button
      onClick={handleLogout}
      className="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mt-6 dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800"
    >
      Logout
    </button>
  );
};

export default LogoutButton;