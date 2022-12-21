import React from 'react';
import "./style.css";
import coolHand from "../../assets/icons/coolHand.svg";
import waiting from "../../assets/icons/waiting.svg";
import ReactTooltip from 'react-tooltip';
import {useContext, UserContext} from "../../utils/contextApi/userContext";

const UserLine = () => {
    const {users} = useContext(UserContext);
    return (<div>
            <ReactTooltip effect={'float'}/>
            {users.map((person) => (
                <div className="rounded-circle bg-light shadow circle mx-2" style={{border: person.isTurn ? '2px solid red' : 'none'}}>
                <span className="text">
                    <b>
                        {person.name}
                    </b>
                </span>
                    <div className={'statusIcon'}>
                        {
                            person.onWaitingPage && (
                                <img data-tip={person.isReady ? `${person.name} burada` : `${person.name} henüz katılmadı`}
                                     src={person.isReady ? coolHand : waiting} alt="" width={'40px'}/>
                            )
                        }
                    </div>
                </div>
            ))}

        </div>
    )
}

export default UserLine;
