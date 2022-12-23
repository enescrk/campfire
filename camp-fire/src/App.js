import React, { useEffect, useState } from 'react';
import './App.css';
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import * as signalR from "@microsoft/signalr";

function App() {
    const [text, setText] = useState("");
    const [name, setName] = useState("");
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
            hubConnection.invoke("SendEvent", "3").then((res) => { })
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
                    {name}
                </ul>
            </div>
        </div>
    );
}
export default App;
