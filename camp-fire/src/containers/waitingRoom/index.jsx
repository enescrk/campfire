import React, {useEffect, useState} from 'react';
import "./style.css";
import UserLine from "../../components/userLine";
import camera from "../../assets/icons/camera.svg";
import peaceHand from "../../assets/icons/peaceHand.svg";
import fire from "../../assets/icons/fire.svg";
import moon from "../../assets/gifs/moon.gif";
import CustomBtn from "../../components/customBtn";
import {useHistory} from "react-router-dom";
import {useContext, UserContext} from "../../utils/contextApi/userContext";
const WaitingRoom = () => {
    const {users, setUsers} = useContext(UserContext);
    const history = useHistory();
    const openMeetingTab = () => {
        window.open("https://meet.google.com/dno-hjhm-kdx", "_blank");
        history.push("/truthordare");
        let tempUsers = users;
        tempUsers.map((person) => person.onWaitingPage = false);
        setUsers(tempUsers);
    }

    return (<div>
            <UserLine/>

            <div className="card w-50 p-4 mx-auto campBg">
                <div className={'mx-auto my-auto'}>
                    <div>
                        <img src={camera} alt="" width={'32px'}/>
                        Görüşmede kameranın açık olduğundan emin ol.
                    </div>
                    <div>
                        <img src={peaceHand} alt="" width={'32px'}/>
                        Görüşme başında selam vermek güzel bir başlangıç olabilir.
                    </div>
                    <div>
                        <img src={fire} alt="" width={'32px'}/>
                        Kendini kamp ateşi heycanına bırakmayı ihmal etme
                    </div>
                </div>
                <div className={'mx-auto w-75'}>
                    <CustomBtn text={'Görüşmeye Katıl'} background={'p3'} size={'xs'} width={'100%'} clicked={() => openMeetingTab()} selectedIcon={moon}/>
                </div>
            </div>
        </div>
    )
}

export default WaitingRoom;
