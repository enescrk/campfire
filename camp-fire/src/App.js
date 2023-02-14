import './App.css';
import Login from "./containers/login";
import Navbar from "./containers/navbar";
import React, {Suspense, useEffect, useState} from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route
} from "react-router-dom";
import {useHistory} from "react-router-dom";
import WaitingRoom from "./containers/waitingRoom";
import TruthOrDare from "./containers/truthOrDare";
import SMores from "./containers/sMores";
import GostStory from "./containers/gostSory";
import {EventContext} from "./utils/contextApi/eventContext";
import {HubConnectionBuilder} from "@microsoft/signalr";
import {pageIndex} from "./utils/enum";

function App() {
    const [hubConnection, setHubConnection] = useState();
    const [connectionStarted, setConnectionStarted] = useState(false);
    const [event, setEvent] = useState(null);
    const history = useHistory();

    useEffect(() => {
        createHubConnection();
    }, []);

    const redirectUserToRelatingPage = () => {
        let pathName = window.location.pathname;
        if(pathName !== pageIndex[event?.currentPageId])
        window.location.pathname = pageIndex[event?.currentPageId];
    }
    const createHubConnection = async () => {
        const hubCn = new HubConnectionBuilder().withUrl("http://localhost:5000/eventHub").build()
        try {
            await hubCn.start();
            setHubConnection(hubCn);
        } catch (e) {
            console.log("e", e)
        }
    }
    const sendMsg = () => {
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
            setConnectionStarted(true);
        })

    }
    useEffect(() => {
        if (hubConnection) {
            if (!connectionStarted) {
                sendMsg();
            }

            hubConnection.on("GetEvent", (data) => {
                setEvent(data);
            })
        }
    }, [hubConnection])

    useEffect(() => {
        console.log(event)
        if (event)
            redirectUserToRelatingPage();
    }, [event])


    return (
        <EventContext.Provider value={{event, setEvent}}>
            <div className="App">
                <Navbar/>
                <Suspense fallback={<span>Loading</span>}>
                    <Router>
                        <Switch>
                            <Route exact path="/">
                                <Login/>
                            </Route>
                            <Route path="/waiting">
                                <WaitingRoom/>
                            </Route>
                            <Route path="/truthordare">
                                <TruthOrDare/>
                            </Route>
                            <Route path="/smores">
                                <SMores/>
                            </Route>
                            <Route path="/story">
                                <GostStory/>
                            </Route>
                        </Switch>
                    </Router>

                </Suspense>
            </div>
        </EventContext.Provider>
    );
}
export default App;
