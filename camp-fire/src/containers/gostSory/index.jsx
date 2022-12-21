import React, {useEffect, useState} from 'react';
import "./style.css";
import "../../components/style.css";
import UserLine from "../../components/userLine";
import StoryMaker from "../../components/storyMaker";
import PageTitle from "../../components/pageTitle";
import Counter from "../../components/counter";

const GostStory = () => {
const [isOver, setIsOver] = useState(false);

    return (<div>
            <UserLine />
            <div className={'mt-4 w-50 mx-auto'}>
                <PageTitle title={'Horror Story'} />
            </div>
            <div className={'mt-4'}>
                <span>
                    Sıra sana geldiğinde verilen süre içinde hikayeyi devam ettirecek yaratıcı bir cümle eklemelisin.
                </span>
                <Counter limit={60} isOver={e => setIsOver(e)} />
            </div>
            <div className={'mt-4 w-75 mx-auto'}>
            <StoryMaker isOver={isOver} />
            </div>
        </div>
    )
}

export default GostStory;
