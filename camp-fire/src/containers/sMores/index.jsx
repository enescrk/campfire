import React, {useEffect, useState} from 'react';
import "./style.css";
import "../../components/style.css";
import UserLine from "../../components/userLine";
import VideoPlayer from "../../components/videoPlayer";
import CustomBtn from "../../components/customBtn";

const SMores = () => {
    const videoList = [
        {
            title: "Kutu açılımı",
            url: "https://www.youtube.com/embed/qsOUv9EzKsg",
            id: 0
        },
        {
            title: "Ateşin Yakımı",
            url: "https://www.youtube.com/embed/9nBFKH3qhGE",
            id: 1
        },
        {
            title: "Ateşin Yakımı",
            url: "https://www.youtube.com/embed/9nBFKH3qhGE",
            id: 2
        },
        {
            title: "Ateşin Yakımı",
            url: "https://www.youtube.com/embed/9nBFKH3qhGE",
            id: 3
        },
    ];

    const [videoInformation, setVideoInformation] = useState(videoList[0]);

    const nextStage = () => {
        let currentVideoId = videoInformation.id;
        if (videoList.length !== currentVideoId + 1) {
            setVideoInformation(videoList[currentVideoId + 1]);
        }
    }


    return (<div>
            <UserLine/>
            <div className={'mt-4'}>
                <VideoPlayer information={videoInformation}
                             percent={((videoInformation.id + 1) / videoList.length) * 100}/>
                <div className={'mx-auto w-75 mt-2'}>
                    <CustomBtn text={'Sonraki Aşama'} size={'xs'} background={'p2'} width={'40%'}
                               clicked={() => nextStage()}/>
                </div>
            </div>
        </div>
    )
}

export default SMores;
