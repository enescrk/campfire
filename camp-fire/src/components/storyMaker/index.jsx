import React, {useEffect, useState} from 'react';
import "./style.css";
import "../style.css";
import * as stories from "../../utils/stories.json";
import InputBox from "../inputBox";
import CustomBtn from "../customBtn";
import ReactTooltip from 'react-tooltip';

const StoryMaker = ({isOver}) => {
    const storyList = stories.default;
    const [storyObj, setStoryObj] = useState(storyList[0]);
    const [addedSentence, setAddedSentence] = useState("");
    const [personsSentences, setPersonSentences] = useState([]);

    const addSentences = () => {
        let sentenceObj = {
            sentence: addedSentence,
            person: "Ahmet Enes Çırak",
            color: "red"
        }
        let tempArr = personsSentences;
        tempArr.push(sentenceObj)
        setPersonSentences(tempArr)
        setAddedSentence("");
    }

    useEffect(() => {
        ReactTooltip.rebuild();
    });

    return (<div className={'card storyBox'}>
            <ReactTooltip effect={'float'}/>
            <span className={'mx-3 mt-3'}>
                {storyObj.story}
            </span>

            {personsSentences.map(sentenceObj => (
                <span data-tip={`${sentenceObj.person} tarafından eklendi.`} style={{
                    borderBottom: `1px solid ${sentenceObj.color}`,
                    width: "max-content",
                    display: 'inline-block',
                    fontFamily: 'Maitree'
                }} className={'addedSentence mx-3 my-1'} key={sentenceObj.sentence}>
                    {sentenceObj.sentence}
                </span>
            ))}

            {
                !isOver && (
                    <div className={'storyInput mx-auto w-100 rounded'}>
                        <InputBox placeholder={'Hikayeyi devam ettirecek yaratıcı cümleler yaz.'} type={'text'}
                                  value={e => setAddedSentence(e)}/>
                        <CustomBtn text={"Ekle"} width={'10%'} size={'xs'} background={'p1'}
                                   clicked={() => addSentences()}/>
                    </div>
                )
            }

        </div>
    )
}

export default StoryMaker;
