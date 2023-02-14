import React, {createContext, useContext, useEffect, useState} from 'react';
import logo from "../../assets/icons/camperBear.svg";
import "./style.css";

const Navbar = () => {
    const [bearText, setBearText] = useState(null);
    const pageText = [
        {path: "waiting", text: "Tüm katılımcıların hazır olmasını bekliyoruz"},
        {
            path: "truthordare", text: "Selam Ahmet Enes,\n" +
                "Doğruyu söylemeye hazır mısın yoksa cesaretini kanıtlamaya razı mısın ?"
        },
        {path: "smores", text: "Ateşi harlama vakti!"},
        {path: "story", text: "Korku hikayesi bir kamp geleneğidir. Takıldığınız yerde size yardımcı olacağım."}
    ];
    setInterval(() => {
        if (window.location.pathname !== '/')
            setBearText(pageText.filter((page) => window.location.pathname.includes(page.path))[0].text);
    }, 2000)


    return (<div className={'navbar'}>
            <div className={'mx-auto'}>
                <img src={logo} alt="fire" style={{width: '50px'}}/>
                {bearText && (
                    <div className={'shadow comment'}>
                        <div className="triangle"></div>
                        <div className={'bearTalk p-2 rounded'}>
                            “{bearText}"
                        </div>
                    </div>
                )
                }

            </div>
        </div>
    )
}

export default Navbar;
