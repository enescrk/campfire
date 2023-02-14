import {axios} from "./axios";
import {HubConnectionBuilder} from "@microsoft/signalr";
import {useEffect, useState} from "react";

const apiUrl = "https://cabinfitapi.cabin.com.tr";
let hubConnection = "";
const authorization = `Bearer 1a0e12a3-147d-4f5a-bdae-dc588293b91e`;

export const campFireService = {
    joinMeeting(mail, code) {
        return axios.get(`${apiUrl}/api/Product/${code}/IsAvailable`, {
            headers: {
                "Authorization": authorization
            }
        });
    },
    async createHubConnection() {
        const hubCn = new HubConnectionBuilder().withUrl("http://localhost:5000/eventHub").build()
        try {
            await hubCn.start();
            console.log(hubCn.connectionId)
            hubConnection = hubCn;
            this.sendMsg();
        } catch (e) {
            console.log("e", e)
        }
    },

    sendMsg() {
        if (hubConnection) {
            hubConnection.invoke("SendEvent", {
                "id": 3,
                "name": "123132",
                "hashedKey": "testHash",
                "date": "2022-12-19T19:20:36.472772Z",
                "user": {
                    "id": 1,
                    "name": "test",
                    "surname": "kılıç",
                    "authorizedCompanies": null,
                    "gender": false,
                    "eMail": null,
                    "userType": 0,
                    "phoneNumber": null
                },
                "scoreboards": [],
                "pageIds": [],
                "participiantIds": null
            }).then((res) => {
            })
        }
    },

    getEvent() {
        let data = {};
        hubConnection.on("GetEvent", (res) => {
            console.log(res)
            data = res;
        })
        return data
    }

};
