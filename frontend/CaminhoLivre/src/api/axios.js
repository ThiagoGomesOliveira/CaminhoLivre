import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7076', // Replace with your API base URL
  timeout: 10000, // Set a timeout for requests (optional)
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;