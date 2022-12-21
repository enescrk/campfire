import React from 'react';
import "./style.css";

const InputBox = ({title, description, placeholder, type, value}) => {

   function sendMail(m) {
       value(m)
   }

    return (
        <div className={'p-4 w-100'}>
            <div>
                <h3 className={'inputTitle'}>{title}</h3>
                <h5 className={'description'}>{description}</h5>
                <input type={type} placeholder={placeholder} className={'inputBox my-3 w-100'} onChange={(e) => sendMail(e.target.value)}/>
            </div>
        </div>
    )
}

export default InputBox;
