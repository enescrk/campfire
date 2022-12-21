import { axios } from "./axios";

const apiUrl = "https://cabinfitapi.cabin.com.tr";

const authorization = `Bearer 1a0e12a3-147d-4f5a-bdae-dc588293b91e`;

export const campFireService = {
    joinMeeting(mail, code) {
        return axios.get(`${apiUrl}/api/Product/${code}/IsAvailable`, {
            headers: {
                "Authorization": authorization
            }
        });
    },

};
