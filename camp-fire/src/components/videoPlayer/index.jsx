
import React from 'react';
import "./style.css";
const CustomVideoPlayer = ({information,percent}) => {
    const url = information?.url;
    return(
        <div className={'mx-auto w-75 shadow p-2 rounded'}>
            <div className={'videoTitle'}>
                <b>
                {information?.title}
                </b>
            </div>
            <div className="progress">
                <div className="progress-bar" role="progressbar" style= {{width: `${percent}%`}} aria-valuenow="25"
                     aria-valuemin="0" aria-valuemax="100"><b> %{percent}</b>
                </div>
            </div>
            <hr/>
            <iframe id="ytplayer"  width="100%" height="500"
                    src={url} title={information?.title}/>
        </div>
    )
}
export default CustomVideoPlayer;
