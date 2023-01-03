import React, { useEffect, useState } from 'react';
import './App.css';
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import * as signalR from "@microsoft/signalr";

function App() {
    const [text, setText] = useState("");
    const [data, setName] = useState(Object);
    const [msgList, setMsgList] = useState(null)
    const [hubConnection, setHubConnection] = useState()
    useEffect(() => {
        createHubConnection();
    }, [])
    const createHubConnection = async () => {
        const hubCn = new HubConnectionBuilder().withUrl("http://localhost:5000/eventHub").build()
        try {
            await hubCn.start();
            console.log(hubCn.connectionId)
            setHubConnection(hubCn)
        } catch (e) {
            console.log("e", e)
        }
    }
    const sendMsg = () => {
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
            }).then((res) => { })
        }
    }
    useEffect(() => {
        if (hubConnection) {
            // sendMsg()
            hubConnection.on("GetEvent", (data) => {
                console.log("EVENTDATASI HABURADAADIR =>" + data)
                setName(data)
            })
        }
    }, [hubConnection])
    return (
        <div className="App">
            <header className="App-header">
                <input value={text} onChange={(e) => { setText(e.target.value) }} />
                <button onClick={sendMsg}>Mesaj Gönder </button>
            </header>
            <div>
                <h2>Mesajlar</h2>
                <ul>
                    {data.name}
                </ul>
            </div>
        </div>
    );
}
export default App;
