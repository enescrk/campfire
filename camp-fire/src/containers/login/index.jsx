import React, {useEffect, useState} from 'react';
import InputBox from "../../components/inputBox";
import CustomBtn from "../../components/customBtn";
import "../../components/style.css";
import "./style.css";
import star from "../../assets/gifs/stars.gif";
import {useHistory} from "react-router-dom";
import {campFireService} from "../../services/loginService";

const Login = () => {
    const [mail, setMail] = useState("");
    const [isDisabled, setIsDesibled] = useState(true);
    const history = useHistory();

    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const eventCode = urlParams.get('code');

    useEffect(() => {
        setIsDesibled(!validateEmail());
    }, [mail])

    function validateEmail() {
        let re = /\S+@\S+\.\S+/;
        return re.test(mail);
    }

    const openWaitingRoom = () => {
        campFireService.joinMeeting(mail, eventCode).then((res) => {
            console.log(res);
        }).catch((err) => {

        });

        history.push("/waiting")
    }

    return (<div>
            <div className={'card inputContainer mx-auto'}>
                <div className={'w-50'}>
                    <InputBox type={'email'} title={'Hoş geldiniz'}
                              description={'Aşağıya mail adresini girerek sıcacık etkinliğimize hemen katıl.'}
                              placeholder={'Mail adresin  (@companyName.com)'} value={e => setMail(e)}/>
                </div>

                <div className={'m-4'}>
                    <CustomBtn text={'Katıl'} size={'xs'} background={!isDisabled ? 'p1' : 'disabled'} width={'20%'}
                               selectedIcon={star} disabled={isDisabled} clicked={() => openWaitingRoom()}/>
                </div>

            </div>
        </div>
    )
}

export default Login;
