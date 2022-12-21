import axiosLib from "axios";

const axios = axiosLib.create({
  baseURL: "https://rickandmortyapi.com/api"
});

export { axios };
